namespace Components

open Feliz
open Feliz.DaisyUI
open Feliz.Router

type Navbar =
      

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

    static member NavbarNavigationLi(page: Types.Page, setPage: Types.Page -> unit, isActive: bool) =
        Html.li [
            Html.a [
              prop.className [
                if isActive then "menu-active"
              ]
              prop.onClick(fun e ->
                e.preventDefault()
                setPage(page)  
              )
              prop.text (sprintf "%A" page)
            ]
        ]

    static member Main(setPage: Types.Page -> unit, statePage: Types.Page, annoState, setAnnoState, fileName) =
        let logoDP = StaticFile.import "./img/DataPLANT_logo_bg_transparent.svg"
        let logoGithub = StaticFile.import "./img/github-mark-white.png"
        let modalState, toggleState = React.useState(false)
        React.useEffect((fun () -> 
          match statePage with
          | Types.Page.Builder -> Router.navigate ("")
          | Types.Page.Help -> Router.navigate ("Help")
          | Types.Page.Contact -> Router.navigate ("Contact")
        ), [| box statePage |])

        Daisy.navbar [
            prop.className "bg-base-300 border-b-1 border-base-content p-0 sticky top-0 z-50"
            prop.children [
                Daisy.navbarStart [
                  prop.className "gap-2 items-center px-2"
                  prop.children [ 
                    Html.a [
                        prop.href "https://www.nfdi4plants.de/"
                        prop.target.blank 
                        prop.children [
                            Html.img [ prop.src logoDP; prop.height 70; prop.width 70]
                        ]
                    ] 
                    Html.div [ prop.text "BOAT"; prop.className "font-bold text-lg" ]
                  ]
                ]

                Daisy.navbarEnd [
                  prop.className "gap-2 px-2"
                  prop.children [
                    // Html.div [
                    //   prop.className "dropdown dropdown-end"
                    //   prop.children [
                    //     Html.div [
                    //       prop.tabIndex 0
                    //       prop.role.button
                    //       prop.className "btn btn-ghost btn-square"
                    //       prop.children [
                    //         Svg.svg [
                    //           svg.className "inline-block w-5 h-5 stroke-current"
                    //           svg.fill "none"
                    //           svg.viewBox(0, 0, 24, 24)
                    //           svg.xmlns "http://www.w3.org/2000/svg"
                    //           svg.children [
                    //             Svg.path [
                    //               svg.strokeLineCap "round"
                    //               svg.strokeLineJoin "round"
                    //               svg.strokeWidth 2
                    //               svg.d "M4 6h16M4 12h16M4 18h16"
                    //             ]
                    //           ]
                    //         ]
                    //       ]
                    //     ]
                    //     Html.ul [
                    //           prop.tabIndex -1
                    //           prop.className "menu menu-sm dropdown-content bg-base-100 rounded-box z-1 mt-2 w-52 shadow"
                    //           prop.children [
                    //             Navbar.NavbarNavigationLi(Types.Page.Builder, setPage, (statePage = Types.Page.Builder))
                    //             Navbar.NavbarNavigationLi(Types.Page.Contact, setPage, (statePage = Types.Page.Contact))
                    //             Navbar.NavbarNavigationLi(Types.Page.Help, setPage, (statePage = Types.Page.Help))
                    //             Html.li [Html.a [prop.href "https://github.com/nfdi4plants/BOAT-Builder-for-Ontology-ARC-Templates"; prop.target.blank; prop.text "GitHub"]]
                    //           ]
                    //         ]
                    //   ]
                    // ]
                    Daisy.button.a [ 
                        prop.text "Builder"
                        prop.className [
                          if statePage = Types.Page.Builder then "bg-[#3f8fae]"
                          else "hover:bg-[#b9e1f0]"
                        ]
                        prop.onClick (fun _ -> 
                          setPage(Types.Page.Builder)
                        
                        ) 
                    ]
                    Daisy.button.a [ 
                        prop.text "Contact"
                        prop.className [
                          if statePage = Types.Page.Contact then "bg-[#3f8fae]"
                          else "hover:bg-[#b9e1f0]"
                        ]
                        prop.onClick (fun _ -> 
                          setPage(Types.Page.Contact)
                        ) 
                    ]
                    Daisy.button.a [ 
                        prop.text "Help"
                        prop.className [
                          if statePage = Types.Page.Help then "bg-[#3f8fae]"
                          else "hover:bg-[#b9e1f0]"
                        ]
                        prop.onClick (fun _ -> 
                          setPage(Types.Page.Help)
                          ) 
                    ]
                    Html.a [
                      // prop.className "btn btn-square"
                      prop.href "https://github.com/nfdi4plants/BOAT-Builder-for-Ontology-ARC-Templates"
                      prop.target.blank 
                      prop.children [
                          // Html.img [ 
                          //     <i class="fa-brands fa-github"></i>
                          //     prop.className "p-1"
                          //     prop.height 40
                          //     prop.width 40
                          // ]
                          Html.i [
                            prop.className "fa-brands fa-github fa-xl"
                          ]
                      ]
                    ]
                  ]
                ]
            ]
        ] 
        
