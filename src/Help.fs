namespace Components

open Feliz
open Feliz.Router
open Feliz.DaisyUI

type Help =
    [<ReactComponent>]
    static member Main() = 
        Html.div [
            prop.className "p-4"
            prop.children [
                Html.h1 [
                    prop.className "text-2xl font-bold mb-4"
                    prop.text "Documentation"
                ]
                Html.div [
                    prop.className "flex flex-row"
                    prop.children [
                        Html.div [
                            prop.className "w-1/5"
                            prop.text "Navigation"
                        ]
                        Html.div [
                            prop.className "card bg-base-100 card-md shadow-sm w-4/5"
                            prop.children [
                                Html.div [
                                    prop.className "card-body"
                                    prop.children [
                                        Html.h2 [
                                            prop.className "card-title"
                                            prop.text "What is BOAT?"
                                            
                                        ]
                                        Html.p [
                                            prop.text "This application is a web-based tool to direct assist in creating metadata annotations out of your free text protocols. These can be connected with ontologies and you get an MS Excel and ARC compatible output containing your annotations describing experimental processes."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/overview.png")
                                            prop.alt "BOAT Logo"
                                            prop.className "w-1/2 mx-auto my-4"
                                        ]
                                        Html.h2 [
                                            prop.className "card-title"
                                            prop.text "Upload your document file"
                                        ]
                                        Html.p [
                                            prop.text "First of all, start by uploading your free text document as a .docx, .pdf, .md or .txt file using the 'Browse' button. The explorer window will open to let you select the file."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/upload.svg")
                                            prop.alt "upload"
                                            prop.className "w-1/3 mx-auto my-4"
                                        ]
                                        Html.h2 [
                                            prop.className "card-title"
                                            prop.text "Adding and customize an annotation"
                                        ]
                                        Html.p [
                                            prop.text "To add a step of the experimental process as an annotation, mark the word and right-click to open the BOAT context menu."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/contextmenu.svg")
                                            prop.alt "context menu"
                                            prop.className "w-1/3 mx-auto my-4"
                                        ]
                                        Html.p [
                                            prop.text "Inside the upper context menu, the marked term can be placed inside a new annotation as a Key, Term or Value. In the lower part, the selected word can be added to the annotation lastly created as the same types."
                                            ]
                                        Html.p [
                                            prop.text "Here the Key refers to the header of the annotation and Term with an optional value is the content."
                                        ]
                                        Html.p [
                                            prop.text "After clicking an action inside the context menu, an annotation note is opened at the edge of the document with the marked word in the respective text field."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/newAnno.svg")
                                            prop.alt "new annotation"
                                            prop.className "w-1/3 mx-auto my-4"
                                        ]
                                        Html.p [
                                            prop.text "Now, you sucessfully added you first annotation! Hooray 🎉"
                                        ]
                                        Html.p [
                                            prop.text "They can be expanded or downsized agin, leading to a small speech bubble icon."
                                        ]
                                        Html.p [
                                            prop.text "But it might need some adjustments. First, you propably want to change the keytype to be more descriptive for the header. By clicking on the blue box, a dropdown opens which lets you determine the keytype such as Characterisics, Factor, Component or Parameter. Under 'more' you can find more descriptives keytypes which are for more general data about the protocol like date, performer, description, type, uri and version."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/keytypeDropdown.png")
                                            prop.alt "keytype dropdown"
                                            prop.className "w-1/3 mx-auto my-4"
                                        ]
                                        Html.p [
                                            prop.text "As a next step, you should consider connecting your added key or terms to ontologies."
                                        ]
                                        Html.p [
                                            prop.text "Implementing ontologies enhances the findability and interoperable aspect of your data by using predefined ontologies with an unique ID."
                                        ]
                                        Html.p [
                                            prop.text "To do this, just click inside the text field and the built-in search compoenent will search in the database for fitting ontologies. You might change the word a bit to get the best results. The search suggestions can be expanded to see the definitions and the ID of the ontology."
                                        ]
                                        Html.p [
                                            prop.text "Clicking on an ontology suggestion replaces the term in the text field and a checkmark appears on the right to ensure the use of an ontology."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/termSearch.png")
                                            prop.alt "term search"
                                            prop.className "w-1/3 mx-auto my-4"
                                        ]
                                        Html.p [
                                            prop.text "In the same way, you can add more annotations or complete the metadata in the last created annotation. Added words as key, terms or value shighlights them in the document in different colors."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/highlight.svg")
                                            prop.alt "highlight"
                                            prop.className "w-1/3 mx-auto my-4"
                                        ]
                                        Html.p [
                                            prop.text "All annotations can be edited or deleted any time with the trash bin icon in the upper right corner."
                                        ]
                                        Html.h2 [
                                            prop.className "card-title"
                                            prop.text "Preview"
                                        ]
                                        Html.p [
                                            prop.text "To have an overview of your already annotation, you can always click on 'View annotations' to open a table which has all the annotations with their keys, terms and values. \n In there, you can also delete individual annotations."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/preview.png")
                                            prop.alt "preview table"
                                            prop.className "w-1/4 mx-auto my-4"
                                        ]
                                        Html.h2 [
                                            prop.className "card-title"
                                            prop.text "Download the finished product"
                                        ]
                                        Html.p [
                                            prop.text "Added enough annotations? Then just click on the download button and decide if the output metadata is converted into a .xlsx or .json file."
                                        ]
                                        Html.img [
                                            prop.src (StaticFile.import "./img/download.png")
                                            prop.alt "download"
                                            prop.className "w-1/3 mx-auto my-4"
                                        ]
                                        Html.p [
                                            prop.className "flex"
                                            prop.children [
                                                Html.span [
                                                    prop.text "It depends on which file format fits to your workflow or enviroment the best for example MS Excel or the ARCitect by"
                                                ]
                                                Html.a [
                                                    prop.text "DataPLANT."
                                                    prop.href "https://www.nfdi4plants.de/"
                                                    prop.target.blank 
                                                    prop.className "underline text-blue-400 pl-2"
                                                ]
                                            ]
                                        ]
                                        Html.p [
                                            prop.text "And that's it!"
                                        ]
                                        Html.p [
                                            prop.text "If you want to annotate more metadata in a new protocol, you can delete it using the red delete button and the left side of the document or just uplaod a new one which overwrites the current document."
                                        ]
                                    ]
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]