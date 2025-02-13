namespace App

open Feliz
open Types
open Components
open Fable.SimpleJson
open ARCtrl.Json
open Thoth.Json.Core

        
type View =
    [<ReactComponent>]
    static member Main() =

        let isLocalStorageClear (key:string) () =
            match (Browser.WebStorage.localStorage.getItem key) with
            | null -> true // Local storage is clear if the item doesn't exist
            | _ -> false //if false then something exists and the else case gets started

        let initialInteraction (id: string) =
            if isLocalStorageClear id () = true then []
            else Decode.fromJsonString decoderAnno (Browser.WebStorage.localStorage.getItem id)  

        let (AnnotationState: Annotation list, setAnnotationState) = React.useState (initialInteraction "Annotations")

        let setLocalStorageAnnotation (id: string)(nextAnnos: Annotation list) =
            let JSONstring= 
                // Json.stringify nextAnnos 
                nextAnnos |> List.map encoderAnno |> Encode.list |> Encode.toJsonString 0

            // log JSONstring
            Browser.WebStorage.localStorage.setItem(id, JSONstring)

        let setState(state: Annotation list) =
            setAnnotationState state
            setLocalStorageAnnotation "Annotations" state    

        
          

        let (modalState: ModalInfo, setModal) =
            React.useState(Contextmenu.initialModal)               
                       
        let myModalContext = { //makes setter and state in one record type
            modalState = modalState
            setter = setModal
            }

        let elementID = "Paper"
        
        let currentpage,setpage = React.useState(Types.Page.Builder) 

        React.strictMode [
            React.contextProvider(Contexts.ModalContext.createModalContext, myModalContext, React.fragment [ //makes the context accesable for the whole project
                Html.div [
                    prop.id "mainView"
                    prop.className "flex min-h-screen flex-col"
                    prop.children [
                        Components.Navbar.Main(setpage,currentpage, AnnotationState, setState)
                        Html.div [
                            prop.id "contentView"
                            prop.className "grow"
                            prop.children [
                                match currentpage with
                                |Types.Page.Builder -> 
                                  Components.Builder.Main( 
                                    AnnotationState, 
                                    setState, 
                                    isLocalStorageClear,
                                    elementID,
                                    modalState,
                                    myModalContext
                                  )
                                |Types.Page.Contact -> Components.Contact.Main()
                                |Types.Page.Help -> Components.Help.Main()
                            ]
                        ]
                        Components.Footer.Main
                    ]
                ]
            ])
        ]