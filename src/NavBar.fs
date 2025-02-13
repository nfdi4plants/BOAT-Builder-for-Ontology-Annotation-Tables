namespace Components

open Feliz
open Feliz.DaisyUI


type Navbar =

    static member AnnotationModal (isActive: bool, toggleActive: bool -> unit, annoState, setAnnoState) = 
      Daisy.modal.dialog [
          prop.className [
            if isActive then "modal-open"
          ]
          prop.children [
              Daisy.modalBox.div [
                  Html.form [
                    prop.method "dialog"
                    prop.children [
                      Daisy.button.button [
                        prop.className "btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
                        prop.text "✕"
                        prop.onClick (fun _ -> toggleActive(false))
                      ]
                    ]
                  ]
                  PreviewTable.table(annoState, setAnnoState)
              ]
          ]
      ]

    static member NavbarButton(text: string, onClick: unit -> unit, ?disabled) = 
      let disabled = defaultArg disabled false
      Daisy.button.a [ 
          prop.text text
          prop.className [
            "btn-outline"
          ]
          prop.onClick (fun _ -> onClick())
          prop.disabled disabled
      ]

    static member Main(setPage: Types.Page -> unit, statePage: Types.Page, annoState, setAnnoState) =
        let modalState, toggleState = React.useState(false)

        let logo = StaticFile.import "./img/DataPLANT_logo_bg_transparent.svg"
        Daisy.navbar [
            prop.className "bg-base-300 shadow-lg"
            prop.children [

                Navbar.AnnotationModal(modalState, toggleState, annoState, setAnnoState)

                Daisy.navbarStart [
                  prop.className "gap-2 items-center"
                  prop.children [ 
                    Html.a [
                        prop.href "https://www.nfdi4plants.de/"
                        prop.target.blank 
                        prop.children [
                            Html.img [ prop.src logo; prop.height 70; prop.width 70]
                        ]
                    ] 
                    Html.div [ prop.text "BOAT - Builder for Ontology ARC Templates"; prop.className "font-semibold" ]
                  ]
                ]

                Daisy.navbarCenter [
                  prop.className "gap-2"
                  prop.children [
                    Navbar.NavbarButton(
                      "Builder",
                      fun _ -> setPage(Types.Page.Builder)
                    )

                    Navbar.NavbarButton(
                      "View annotations",
                      (fun _ -> toggleState(true)),
                      disabled = List.isEmpty annoState
                    )
                
                    Navbar.NavbarButton(
                      "Download",
                      (fun _ -> ()),
                      disabled = List.isEmpty annoState
                    )
                  ]

                ]

                Daisy.navbarEnd [

                  prop.className "gap-2"
                  prop.children [
                    Daisy.button.a [ 
                        prop.text "Help"
                        prop.className "hover:bg-[#3f8fae]"
                        prop.onClick (fun _ -> setPage(Types.Page.Help)) 
                        ]
                    Daisy.button.a [ 
                        prop.text "Contact"
                        prop.className "hover:bg-[#3f8fae]"
                        prop.onClick (fun _ -> setPage(Types.Page.Contact)) 
                        // prop.children [
                        //     Html.i [prop.className "fa-brands fa-github"; prop.style [style.fontSize (length.rem 3)]]
                        // ]
                    ]
                    Daisy.button.a [
                      prop.href "https://github.com/nfdi4plants/BOAT-Builder-for-Ontology-ARC-Templates"
                      prop.target.blank 
                      prop.className "btn-square"
                      prop.children [
                          Html.img [ 
                              prop.src "./img/github-mark-white.png"
                              prop.className "p-2"
                          ]
                      ]
                    ]
                  ]
                ]
            ]
        ] 
        
