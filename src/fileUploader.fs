namespace Components

open Feliz
open Types
open Fable.SimpleJson
open System.Text.RegularExpressions
open ARCtrl


module private FileReaderHelper =
  open Fable.Core
  open Fable.Core.JsInterop

  [<Emit("new FileReader()")>]
  let newFileReader(): Browser.Types.FileReader = jsNative

  let readDocx (file: Browser.Types.File) setState setLocalFile = 
    let reader = newFileReader()
    reader.onload <- fun e ->
      let arrayBuffer = e.target?result
      promise {
        let! r = Mammoth.mammoth.convertToHtml({|arrayBuffer = arrayBuffer|})
        (Docx r.value)
        |> fun t ->
          t |> setState
          t |> setLocalFile "file"
      }
      |> Promise.start

    reader.onerror <- fun e ->
      Browser.Dom.console.error ("Error reading file", e)
    reader.readAsArrayBuffer(file)

  // let readPdf (file: Browser.Types.File) setState =
  //   let src = URL.createObjectURL(file)
  //   log ("Uploaded PDF:", src)
  //   setState (PDF src)

  let readFromFile (file: Browser.Types.File) setState (fileType: UploadFileType) setLocalFile =
    match fileType with
    | UploadFileType.Docx -> readDocx file setState setLocalFile
    // | UploadFileType.PDF -> readPdf file setState

module Highlight =

  let keyList(annoList: Annotation list) = 
    [|
        for a in annoList do
          a.Search.Key.NameText
    |]
  let valuelist(annoList: Annotation list) = 
    [|
        for a in annoList do
          match a.Search.Body with
            | CompositeCell.Term oa -> oa.NameText
            | CompositeCell.Unitized (v,oa) -> oa.NameText
            |_ -> ()
    |]

  // let allList (annoList: Annotation list) =
  //   keyList(annoList) @ valuelist(annoList)

      // List.fold (fun (acc: string) value -> 
      //   acc.Replace(value, $"<mark>{value}</mark>")
      // ) keyHighlighttext values


type FileUpload =
    static member DisplayHtml(htmlString: string, annoList: Annotation list, elementID: string) = 
      // Html.div [    
      //       // prop.innerHtml (Highlight.highlightAnnos (htmlString, Highlight.keyList (annoList)))
      //       prop.dangerouslySetInnerHTML htmlString
      //       prop.className "prose bg-slate-100 p-3 text-black max-w-4xl"  
      //       prop.id elementID        
      // ]
      Html.div [
        Components.PaperWithMarker.Main(htmlString, Highlight.keyList(annoList), Highlight.valuelist(annoList), elementID)
      ]


    /// https://stackoverflow.com/a/60539836/12858021
    static member DisplayPDF(pdfSource: string, modalContext: DropdownModal) =
      Html.div [
        prop.className "prose lg:prose-lg"
        prop.onContextMenu (fun e ->
            let term = Browser.Dom.window.getSelection().ToString().Trim() 
            if term.Length <> 0 then 
                modalContext.setter {
                    isActive = true;
                    location = int e.pageX, int e.pageY
                }
                e.stopPropagation() 
                e.preventDefault()
            else 
                ()
        )
        prop.children [
          Html.embed [
            prop.src pdfSource
            prop.type' "application/pdf"
            prop.style [
              style.minHeight (length.perc 100)
              style.width (length.perc 100)
              style.height 600
            ]
          ]
        ]
      ]

    static member private FileTypeSelect (setUploadFileType) =
      Html.select [
        prop.className "select join-item w-min"
        prop.onChange (fun (e: string) -> 
          match e with
          | "Docx" -> setUploadFileType(UploadFileType.Docx)
          // | "PDF" -> setUploadFileType(UploadFileType.PDF)
          | _ -> ()
        )
        prop.children [
          Html.option [
            prop.value "Docx"
            prop.text "Docx"
          ]
          // Html.option [
          //   prop.value "PDF"
          //   prop.text "PDF"
          // ]
        ]
      ]

    static member private FileInput (ref: IRefValue<Browser.Types.HTMLInputElement option>) filehtml uploadFileType setUploadFileType setFilehtml setLocalFile setState setFileName setLocalFileName=
      Html.input [
        prop.className "file-input join-item"
        prop.ref ref
        prop.type'.file
        prop.onChange (fun (f: Browser.Types.File) -> 
          FileReaderHelper.readFromFile f setFilehtml uploadFileType setLocalFile
          if ref.current.IsSome then
            ref.current.Value.value <- null
          setFileName f.name
          setLocalFileName "fileName" f.name
        )
      ]

    static member private RemoveUploadedFileButton (setFilehtml, setLocalFile, setState, setFileName, setLocalFileName) =
      Html.button [
        prop.className "btn btn-error btn-block"
        prop.onClick (fun e -> 
          setFilehtml Unset
          setLocalFile "file" Unset

          [] |> setState

          setFileName ""
          setLocalFileName "fileName" ""
        )
        prop.children [
          Html.span [
            Html.i [
              prop.className "fa-solid fa-trash-can"
            ]
          ]
        ]
      ]

    /// <summary>
    /// A stateful React component that maintains a counter
    /// </summary>
    [<ReactComponent>]
    static member UploadDisplay(filehtml, setFilehtml, setState, setFileName, setLocalFileName) =
    
        let uploadFileType, setUploadFileType = React.useState(UploadFileType.Docx)

        let setLocalFile (id: string) (nextFile: UploadedFile) =
            let JSONString = Json.stringify nextFile 
            Browser.WebStorage.localStorage.setItem(id, JSONString)

        let ref = React.useInputRef()
        Html.div [
          prop.className "flex flex-col gap-2"
          prop.children [
            Html.div [
              prop.className "join"
              prop.children [
                FileUpload.FileTypeSelect setUploadFileType
                FileUpload.FileInput ref filehtml uploadFileType setUploadFileType setFilehtml setLocalFile setState setFileName setLocalFileName
              ]
            ]
            match filehtml with
            | Unset -> Html.div []
            | _ ->
              FileUpload.RemoveUploadedFileButton(
                setFilehtml, setLocalFile, setState, setFileName, setLocalFileName
              )
          ]
        ]

