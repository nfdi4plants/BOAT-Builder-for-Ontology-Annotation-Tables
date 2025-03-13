namespace Components

open Feliz
open Browser.Dom
open Browser.Types
open Types
open Fable.SimpleJson
open Fable.Core.JS
open System
open ARCtrl
open Fable.Core.JsInterop

module List =
  let rec removeAt index list =
      match index, list with
      | _, [] -> failwith "Index out of bounds"
      | 0, _ :: tail -> tail
      | _, head :: tail -> head :: removeAt (index - 1) tail

// module Helperfunctions =
    
// type BOATelement =
//     static member annoBlock (annoState: Annotation list, setState: Annotation list -> unit, index: int) =

//         let revIndex = annoState.Length - 1 - index
//         let a = annoState.[revIndex]

//         let updateAnnotation (func:Annotation -> Annotation) =
//             let nextA = func a
//             annoState |> List.mapi (fun i a ->
//                 if i = revIndex then nextA else a 
//             ) |> setState

//         let annosAfterCurrent: Annotation list = 
//             let splittedList = List.splitAt revIndex annoState
//             let before, after = splittedList
//             after
        
//         Bulma.block [
//             prop.style [
//                 style.position.absolute
//                 //if a.IsOpen = true then annoState |> List.map (fun e -> style.top (int e.Height + 40)) 
//                 //else style.top (int a.Height)
//                 style.top (int a.Height)
//             ]
//             prop.children [
//                 Bulma.block [
//                     if a.IsOpen = false then 
//                         Html.button [
//                             Html.i [
//                                 prop.className "fa-solid fa-comment-dots"
//                                 prop.style [style.color "#ffe699"]
//                                 prop.onClick (fun e ->
//                                     (annoState |> List.mapi (fun i e ->
//                                         if i = revIndex then e.ToggleOpen() 
//                                         else {e with IsOpen = false}
//                                     )) |> setState 
//                                     // updateAnnotation (fun a -> a.ToggleOpen())                              
//                                 )
//                             ]
//                         ] 
//                     else
//                         Html.div [
//                             prop.className "bg-[#ffe699] p-3 text-black z-50 max-w-96 mb-20"
//                             prop.children [
//                                 Bulma.columns [
//                                     Bulma.column [
//                                         column.is1
//                                         prop.className "hover:bg-[#ffd966] cursor-pointer"
//                                         prop.onClick (fun e -> updateAnnotation (fun a -> a.ToggleOpen())
                                        
//                                         )
//                                         prop.children [
//                                             Html.span [
//                                                 Html.i [
//                                                     prop.className "fa-solid fa-chevron-left"
//                                                 ]
//                                             ]
//                                         ]
//                                     ]
//                                     Bulma.column [
//                                         prop.className "space-y-2"
//                                         prop.children [
//                                             Html.span "Key: "
//                                             Html.span [
//                                                 prop.className "delete float-right mt-0"
//                                                 prop.onClick (fun _ -> 
//                                                     let newAnnoList: Annotation list = annoState |> List.filter (fun x -> x = a |> not)  
//                                                     // List.removeAt (List.filter (fun x -> x = a) state) state
//                                                     setState newAnnoList
//                                                 )
                                                
//                                             ]
//                                             Bulma.input.text [
//                                                 input.isSmall
//                                                 prop.value (a.Search.Key|> Option.map (fun e -> e.Name.Value) |> Option.defaultValue "")
//                                                 prop.className ""
//                                                 prop.onChange (fun (x: string)-> 
//                                                     let updatetedAnno = 
//                                                         {a with Key = OntologyAnnotation(name = x) |> Some}

//                                                     let newAnnoList: Annotation list =
//                                                         annoState
//                                                         |> List.map (fun elem -> if elem = a then updatetedAnno else elem)

//                                                     setState newAnnoList
//                                                 )
//                                             ]
//                                             Html.p "Value: "
//                                             Bulma.input.text [
//                                                 input.isSmall
//                                                 prop.value (a.Value|> Option.map (fun e -> e.ToString()) |> Option.defaultValue "" )
//                                                 prop.className ""
//                                                 prop.onChange (fun (x:string) -> 
//                                                     let updatetedAnno = 
//                                                         {a with Value = CompositeCell.createFreeText(x) |> Some}
                                                        
//                                                     let newAnnoList: Annotation list =
//                                                         annoState
//                                                         |> List.map (fun elem -> if elem = a then updatetedAnno else elem)

//                                                     setState newAnnoList
//                                                 )
//                                             ]
//                                         ]
//                                     ]
//                                 ]
//                             ]  
//                         ]
//                 ]
//             ]
//         ]


type Builder =
    [<ReactComponent>]
    static member Main (annoState: Annotation list, setState: Annotation list -> unit, isLocalStorageClear: string -> unit -> bool, elementID, modalState, modalContext) =

        let initialFile (id: string) =
            if isLocalStorageClear id () = true then Unset
            else Json.parseAs<UploadedFile> (Browser.WebStorage.localStorage.getItem id)  

        let (filehtml: UploadedFile), setFilehtml = React.useState(initialFile "file")

        let setLocalFileName (id: string)(nextNAme: string) =
            let JSONstring= 
                Json.stringify nextNAme 

            Browser.WebStorage.localStorage.setItem(id, JSONstring)

        let initialFileName (id: string) =
            if isLocalStorageClear id () = true then ""
            else Json.parseAs<string> (Browser.WebStorage.localStorage.getItem id)  

        let fileName, setFileName = React.useState(initialFileName "fileName")

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

        // React.useEffect((fun _ ->
        //   // if modalContext.modalState.isActive then 
        //   //   document.body.setAttribute("style", "overflow-y: hidden; scrollbar-gutter: stable")
        //   // else 
        //   //   document.body.setAttribute("style", "overflow: auto; overflow-x:hidden;")
        // ), [|box modalContext.modalState.isActive|])

        Html.div [
            prop.className "flex flex-row py-5 px-5"
            prop.id "main-parent"
            prop.onClick (fun e -> modalContext.setter initialModal)
            prop.children [
                Html.div [
                    prop.className "w-1/5 px-2"
                    prop.children [
                        Html.h1 [
                            prop.className "mb-2"
                            prop.text "Navigation"
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
                          Html.div [
                            prop.className "container mx-auto flex"
                            prop.children [
                              Html.p [prop.text "Upload a file!"; prop.className "text-[#4fb3d9]"]
                            ]
                          ] 
                        | Docx fileString ->
                          Html.div [
                            prop.className "overflow-x-hidden h-[50rem] flex flex-row gap-2 w-full relative"
                            prop.children [
                              match modalState.isActive with
                              |true -> Contextmenu.onContextMenu (modalContext, annoState, setState, elementID)
                              |false -> Html.none
                              Html.div [
                                prop.className "w-2/3"
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
                                      prop.className "mb-2"
                                      prop.style [
                                          style.width.inheritFromParent
                                      ]
                                  ]
                                  FileUpload.DisplayHtml(fileString, annoState, elementID)
                                ]
                              
                              ]
                              Html.div [
                                prop.className "w-1/3"
                                prop.children [
                                  if filehtml = Unset then
                                      Html.none
                                  else
                                      Html.div [
                                          prop.text "Annotations"
                                          prop.className "mb-2"
                                          // prop.className "mb-2 fixed bg-[#183641] z-50 top-20"
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
                ]
              ]
            ]
        ]
        
        
        

        
    
