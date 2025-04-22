namespace Components

open Feliz
open Types
open Fable.SimpleJson
open System.Text.RegularExpressions
open ARCtrl
open Fable.Core.JsInterop
open Browser.Dom

module PDFjs =

  importSideEffects "react-pdf/dist/Page/TextLayer.css"
  importSideEffects "react-pdf/dist/Page/AnnotationLayer.css"
  emitJsStatement () """import { pdfjs } from 'react-pdf';

pdfjs.GlobalWorkerOptions.workerSrc = new URL(
  'pdfjs-dist/build/pdf.worker.min.mjs',
  import.meta.url,
).toString();"""

type ReactElements =
  [<ReactComponent(import="Document", from="react-pdf")>]
  static member Document (file: string, onLoadSuccess: {|numPages: int|} -> unit, children: ReactElement list, ?externalLinkTarget: string) = React.imported()

  [<ReactComponent(import="Page", from="react-pdf")>]
  static member Page (pageNumber: int, width: int, customTextRenderer:'c -> string, ?key: string) = React.imported()

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

  let readPdf (file: Browser.Types.File) setState setLocalFile = //put pdf to html string converter
    let reader = newFileReader()
    reader.onload <- fun e ->
      let base64 = reader.result
      setState (PDF (base64.ToString()))
      setLocalFile "file" (PDF (base64.ToString()))

    reader.readAsDataURL(file); // Converts to base64

  let readFromFile (file: Browser.Types.File) setState (fileType: UploadFileType) setLocalFile =
    match fileType with
    | UploadFileType.Docx -> readDocx file setState setLocalFile
    | UploadFileType.PDF -> readPdf file setState setLocalFile

module Lists =

  let keyList (annoList: Annotation list)= 
    annoList 
    |> List.map (fun a -> a.HighlightKeys) //maps the key names to a list
    |> List.toArray
    |> Array.filter (fun a -> a <> "") //filters out empty strings
    
     //filters out empty strings

  let valuelist (annoList: Annotation list) = 
    annoList 
    |> List.map (fun a -> a.HighlightTerms)
    |> List.toArray
    |> Array.filter (fun a -> a <> "")

type FileUpload =
    static member DisplayHtml(htmlString: string, annoList: Annotation list, elementID: string) = 
      // Html.div [    
      //       // prop.innerHtml (Highlight.highlightAnnos (htmlString, Highlight.keyList (annoList)))
      //       prop.dangerouslySetInnerHTML htmlString
      //       prop.className "prose bg-slate-100 p-3 text-black max-w-4xl"  
      //       prop.id elementID        
      // ]
      Html.div [
        PaperWithMarker.Main(htmlString, Lists.keyList annoList, Lists.valuelist annoList, elementID)
      ]


  //  https://stackoverflow.com/a/60539836/12858021
    static member DisplayPDF filehtml setNumPages (numPages: int option) (elementID: string) annoList  =

      let highlightPattern(text: string, anno: string, colorcode) = 
        text.Replace(anno, sprintf "<mark style='background-color: %s'>%s</mark>" colorcode anno)
      // #ffe699
      // #4fb3d9

      let textRender =
        React.useCallback(
          (fun text -> 
            let mutable txt = text?str
            for a in Lists.keyList annoList do
              txt <- highlightPattern(txt, a, "#ffe699")
              log Lists.keyList
            for a in Lists.valuelist annoList do
              txt <- highlightPattern(txt, a, "#4fb3d9")
            txt
          ),
          [|box annoList|]
        )

      Html.div [
        prop.id elementID
        prop.children [
          ReactElements.Document(
            filehtml, 
            (fun (props: {|numPages: int|}) -> 
              setNumPages (Some props.numPages)), 
            [
              for i in 1 .. numPages |> Option.defaultValue 1 do
                ReactElements.Page(
                  i, 
                  750,
                  textRender,
                  i.ToString())
            ],
            externalLinkTarget = "_blank"
          ) 
          Html.p [
              prop.text (
                  match numPages with
                  | Some np -> ""
                  | None -> "Loading..."
              )
          ]
        ]
      ]

    static member private FileTypeSelect (setUploadFileType) =
      Html.select [
        prop.className "select join-item w-min"
        prop.onChange (fun (e: string) -> 
          match e with
          | "Docx" -> setUploadFileType(UploadFileType.Docx)
          | "PDF" -> setUploadFileType(UploadFileType.PDF)
          | _ -> ()
        )
        prop.children [
          Html.option [
            prop.value "PDF"
            prop.text "PDF"
          ]
          Html.option [
            prop.value "Docx"
            prop.text "Docx"
          ]
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

