namespace Components

open Feliz
open Browser.Dom
open Browser.Types
open Types
open Fable.SimpleJson
open Fable.Core.JS
open Feliz.DaisyUI
open System
open ARCtrl
open Fable.Core.JsInterop

module List =
  let rec removeAt index list =
      match index, list with
      | _, [] -> failwith "Index out of bounds"
      | 0, _ :: tail -> tail
      | _, head :: tail -> head :: removeAt (index - 1) tail



type Builder =
    [<ReactComponent>]
    static member Main (annoState: Annotation list, setState: Annotation list -> unit, isLocalStorageClear: string -> unit -> bool, elementID, modalState,fileName: string, setFileName, setLocalFileName) =

        let initialFile (id: string) =
            if isLocalStorageClear id () = true then Unset
            else Json.parseAs<UploadedFile> (Browser.WebStorage.localStorage.getItem id)  

        let (filehtml: UploadedFile), setFilehtml = React.useState(initialFile "file")

        let (numPages: int option), setNumPages = React.useState(None)

        let initialModal = {
            isActive = false
            location = (0,0)
        }

        let modalContext = React.useContext (Contexts.ModalContext.createModalContext)    

        let turnOffContext (event: Browser.Types.Event) = 
            modalContext.setter initialModal 
            
        React.useEffectOnce(fun () ->
            Browser.Dom.window.addEventListener ("resize", turnOffContext)
            {new IDisposable with member this.Dispose() = window.removeEventListener ("resize", turnOffContext) }    
        )
        
        let pleaceHolder =
          Html.div [
              prop.className "overflow-x-hidden h-full flex flex-row gap-2 w-full relative"
              prop.children [
                Html.div [
                  prop.className "w-2/3 flex items-center justify-center"
                  prop.children [
                    Daisy.badge [
                      prop.className ""
                      badge.outline
                      badge.lg
                      badge.info
                      prop.text "Upload a file!"
                    ]
                    // Html.div [prop.text "Upload a file!"; prop.className "text-[#4fb3d9]"]
                  ]
                ]
                Html.div [
                  prop.className "w-1/3"
                  prop.children [
                    Html.div [
                    ]
                  ]
                ]
              ]
            ]

        let paper (width: string) (display: ReactElement) =
          Html.div [
            prop.className "overflow-x-hidden h-[50rem] flex flex-row gap-2 w-full relative"
            prop.children [
              match modalState.isActive with
              |true -> Contextmenu.onContextMenu (modalContext, annoState, setState, elementID)
              |false -> Html.none
              Html.div [
                prop.className width
                prop.onContextMenu (fun e ->
                    // https://stackoverflow.com/a/2614472/12858021
                    let Selection = window.getSelection()
                    let term = Selection.ToString().Trim()
                    let rect = Selection.getRangeAt(0).getBoundingClientRect()
                    let relativeParent = document.getElementById(elementID).getBoundingClientRect()
                    if term.Length <> 0 then 
                        modalContext.setter {
                            isActive = true;
                            location =rect.right - relativeParent.left , rect.bottom - relativeParent.top + 12.0
                        }
                        e.stopPropagation() 
                        e.preventDefault()
                    else 
                        () 
                )
                prop.children [
                  Html.div [
                      prop.text fileName
                      prop.className "mb-2 pt-5 fixed bg-[#a5e7dc] z-30 top-16 w-2/3"
                  ]
                  display
                ]
              ]
              Html.div [
                prop.className "w-1/3"
                prop.children [
                  
                  Html.div [
                      prop.text "Annotations"
                      prop.className "mb-2 pt-5 fixed bg-[#a5e7dc] z-30 top-16"
                      prop.style [
                        style.width.inheritFromParent
                    ]
                  ]
                  for a in 0 .. annoState.Length - 1 do
                      App.Components.AnnoBlockwithSwate(annoState, setState, a)    
                ]
              ]
            ]
          ]

        Html.div [
          prop.className "flex flex-row p-5"
          prop.id "main-parent"
          prop.onClick (fun e -> modalContext.setter initialModal)
          prop.children [
              Html.div [
                  if filehtml = Unset then prop.className "w-1/4 px-2"
                  else prop.className "w-1/5 px-2"
                  prop.children [
                      Html.h1 [
                          prop.className "my-2"
                          prop.text "Select file here:"
                      ]
                      Html.div [
                          FileUpload.UploadDisplay(filehtml,setFilehtml, setState, setFileName, setLocalFileName)
                      ]
                      
                  ]
              ]
              Html.div [
                prop.className "w-4/5 px-2"
                prop.children [
                  match filehtml with
                    | Unset ->
                      pleaceHolder
                    | Docx fileString ->
                      paper "w-2/3" (FileUpload.DisplayHtml(fileString, annoState, elementID, isLocalStorageClear))
                    | PDF fileString ->
                      paper "" (FileUpload.DisplayPDF fileString setNumPages numPages elementID annoState)
                    | Txt fileString ->
                      paper "w-2/3" (FileUpload.DisplayHtml(fileString, annoState, elementID, isLocalStorageClear))
                ]
              ]
            ]
          ]
    
        
        

        
    
