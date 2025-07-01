namespace Components

open Feliz
open Feliz.DaisyUI

type Navbar =

    static member AnnotationModal (isActive: bool, toggleActive: bool -> unit, annoState, setAnnoState) = 
        // Modal for displaying annotations
      if annoState = [] then
        Html.none
      else
      Daisy.modal.dialog [
          prop.className [
            if isActive then "modal-open"
            // else "modal-close"
          ]
          // if annoState = [] then toggleActive(false)
          prop.children [
            Daisy.modalBox.div [
              prop.className "p-0"
              prop.children [
                Html.form [
                  prop.method "dialog"
                  prop.children [
                    Daisy.button.button [
                      prop.className "btn btn-sm btn-circle absolute right-2 top-2 z-50 h-5 w-5"
                      prop.text "✕"
                      prop.onClick (fun _ -> toggleActive(false))
                    ]
                  ]
                ]
                Html.div [
                  prop.className "p-inherit overflow-auto"
                  prop.children [
                    PreviewTable.table(annoState, setAnnoState, toggleActive)
                  ]
                ]
              ]
            ]
          ]
        ]

      

    static member NavbarButton(text: string, onClick: unit -> unit, annoState,fileName: string, ?disabled) = 
      
      let disabled = defaultArg disabled false
      if text = "Download" then
        Daisy.dropdown [
          Daisy.button.button [
              prop.className "btn-outline"
              prop.text text
              prop.disabled disabled
          ]
          Daisy.dropdownContent [
              prop.className "p-2 shadow menu bg-base-100 rounded-box w-52"
              prop.tabIndex 0
              prop.children [
                  Html.li [Html.a [
                    prop.text "as .xlsx"
                    prop.onClick (fun _ -> DownloadParser.downloadXlsxProm(fileName,annoState) |> Promise.start)
                  ]]
                  Html.li [Html.a [
                    prop.text "as .json"
                    prop.onClick (fun _ -> DownloadParser.downloadJsonProm(fileName,annoState) |> Promise.start)
                  ]]
              ]
          ]
        ]
        else
          Daisy.button.a [ 
              prop.text text
              prop.className [
                "btn-outline"
              ]
              prop.onClick (fun _ -> onClick())
              prop.disabled disabled
          ]

          

    static member Main(setPage: Types.Page -> unit, statePage: Types.Page, annoState, setAnnoState, fileName) =
        let logoDP = StaticFile.import "./img/DataPLANT_logo_bg_transparent.svg"
        let logoGithub = StaticFile.import "./img/github-mark-white.png"
        let modalState, toggleState = React.useState(false)
        Daisy.navbar [
            prop.className "bg-base-300 shadow-lg p-0 sticky top-0 z-50"
            prop.children [
                Navbar.AnnotationModal(modalState, toggleState, annoState, setAnnoState)
                Daisy.navbarStart [
                  prop.className "gap-2 items-center"
                  prop.children [ 
                    Html.a [
                        prop.href "https://www.nfdi4plants.de/"
                        prop.target.blank 
                        prop.children [
                            Html.img [ prop.src logoDP; prop.height 70; prop.width 70]
                        ]
                    ] 
                    Html.div [ prop.text "BOAT - Builder for Ontology Annotation Tables"; prop.className "font-semibold" ]
                  ]
                ]

                Daisy.navbarCenter [
                  prop.className "gap-2"
                  prop.children [
                    Navbar.NavbarButton(
                      "View annotations",
                      (fun _ -> toggleState(true)),
                      annoState,
                      fileName,
                      disabled = List.isEmpty annoState
                    )
                
                    Navbar.NavbarButton(
                      "Download",
                      (fun _ -> ()), //replace with download funcgtion
                      annoState,
                      fileName,
                      disabled = List.isEmpty annoState
                    )
                  ]
                

                ]

                Daisy.navbarEnd [
                  prop.className "gap-2"
                  prop.children [
                    Daisy.button.a [ 
                        prop.text "Builder"
                        prop.className "hover:bg-[#3f8fae]"
                        prop.onClick (fun _ -> setPage(Types.Page.Builder)) 
                    ]
                    Daisy.button.a [ 
                        prop.text "Contact"
                        prop.className "hover:bg-[#3f8fae]"
                        prop.onClick (fun _ -> setPage(Types.Page.Contact)) 
                    ]
                    Daisy.button.a [ 
                        prop.text "Help"
                        prop.className "hover:bg-[#3f8fae]"
                        prop.onClick (fun _ -> setPage(Types.Page.Help)) 
                    ]
                    Html.a [
                      prop.href "https://github.com/nfdi4plants/BOAT-Builder-for-Ontology-ARC-Templates"
                      prop.target.blank 
                      prop.children [
                          Html.img [ 
                              prop.src logoGithub
                              prop.className "p-1"
                              prop.height 40
                              prop.width 40
                          ]
                      ]
                    ]
                  ]
                ]
            ]
        ] 
        
