module BuildingBlock.Dropdown

open Feliz
open Feliz.DaisyUI
open BuildingBlock
open ARCtrl
open Shared
open Types



[<ReactComponent>]
let FreeTextInputElement(onSubmit: string -> unit) =
    let inputS, setInput = React.useState ""
    Html.div [
        prop.className "flex flex-row gap-0 p-0"
        prop.children [
            Daisy.input [
                join.item
                input.sm
                prop.placeholder "..."
                prop.className "grow truncate"
                prop.onClick (fun e -> e.stopPropagation())
                prop.onChange (fun (v:string) -> setInput v)
                prop.onKeyDown(key.enter, fun e -> e.stopPropagation(); onSubmit inputS)
            ]
            Daisy.button.button [
                join.item
                button.accent
                button.sm
                prop.onClick (fun e -> e.stopPropagation(); onSubmit inputS)
                prop.children [
                    Html.i [prop.className "fa-solid fa-check"]
                ]
            ]
        ]
    ]

module private DropdownElements =

    let divider = Daisy.divider [prop.className "mx-2 my-0"]
    let private annotationsPrinciplesLink = Html.a [prop.href "https://nfdi4plants.github.io/AnnotationPrinciples/"; prop.target.blank; prop.className "ml-auto link-info"; prop.text "info"]

    let createSubBuildingBlockDropdownLink (state:BuildingBlockUIState) setState (subpage: DropdownPage) =
        Html.li [Html.button [
            prop.onClick(fun e ->
                log $"DropdownPage: {subpage}"
                e.preventDefault()
                e.stopPropagation()
                // setopen false
                setState {state with DropdownPage = subpage}
            )
            prop.children [
                Html.div [
                    prop.className "flex flex-row justify-between"
                    prop.children [
                        Html.span subpage.toString
                        Html.i [prop.className "fa-solid fa-arrow-right pl-3 pt-1"]
                    ]
                ]
            ]
        ]]

    /// Navigation element back to main page
    let DropdownContentInfoFooter setState (hasBack: bool) =
        Html.li [
            prop.className "flex flex-row justify-between pt-1"
            prop.onClick(fun e ->
                e.preventDefault()
                e.stopPropagation()
            
                setState {DropdownPage = DropdownPage.Main; DropdownIsActive = false}

            )
            prop.children [
                if hasBack then
                    Html.button [
                        prop.className "content-center"
                        prop.children [
                            Html.i [ prop.className "fa-solid fa-arrow-left" ]
                        ]
                    ]
                annotationsPrinciplesLink
            ]
        ]

    let isSameMajorCompositeHeaderDiscriminate (hct1: CompositeHeaderDiscriminate) (hct2: CompositeHeaderDiscriminate) =
        (hct1.IsTermColumn() = hct2.IsTermColumn())
        && (hct1.HasIOType() = hct2.HasIOType())

    let createBuildingBlockDropdownItem setUiState setopen (annoState: Annotation list)(setState:CompositeHeaderDiscriminate option -> unit)(a)(headerType: CompositeHeaderDiscriminate)  =
        Html.li [Html.button [
            prop.onClick (fun (e: Browser.Types.MouseEvent) ->
                e.stopPropagation()
                e.preventDefault()
                // (annoState |> List.mapi (fun i e ->
                //     if i = a then {e with Search.KeyType = headerType}
                //     else e
                // )) |> setState
                setState (Some headerType)
                setopen false
                { DropdownPage = DropdownPage.Main; DropdownIsActive = false }|> setUiState
            
                
            )
            prop.text (headerType.ToString())
        ]]

    // let createIOTypeDropdownItem setUiState setopen (annoState: Annotation list)(setState: Annotation list -> unit)(a) header (iotype: IOType)  =
        // let setIO (iotype: IOType) =
        //     (annoState |> List.mapi (fun i e ->
        //             if i = a then {e with Search.KeyType = header; Search.Key =  }
        //             else e
        //         )) |> setState
        //     setopen false
        //     { DropdownPage = DropdownPage.Main; DropdownIsActive = false } |> setUiState
        // Html.li [
        //     match iotype with
        //     | IOType.FreeText s ->
        //         let onSubmit = fun (v: string) ->
        //             let header = IOType.FreeText v
        //             setIO header
        //         prop.children [FreeTextInputElement onSubmit]
        //     | _ ->
        //         prop.onClick (fun e -> e.stopPropagation(); setIO iotype)
        //         prop.onKeyDown(fun k -> if (int k.which) = 13 then setIO iotype)
        //         prop.children [
        //             Html.div [prop.text (iotype.ToString())]
        //         ]
        // ]

    /// Main column types subpage for dropdown
    let dropdownContentMain state setUi setopen (annoState: Annotation list) (setAnnoState:CompositeHeaderDiscriminate option -> unit) (a)=
            React.fragment [
                // DropdownPage.IOTypes CompositeHeaderDiscriminate.Input |> createSubBuildingBlockDropdownLink state setState
                CompositeHeaderDiscriminate.Input      |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
                divider
                CompositeHeaderDiscriminate.Parameter      |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
                CompositeHeaderDiscriminate.Factor         |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
                CompositeHeaderDiscriminate.Characteristic |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
                CompositeHeaderDiscriminate.Component      |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
                DropdownPage.More       |> createSubBuildingBlockDropdownLink state setUi
                divider
                // DropdownPage.IOTypes CompositeHeaderDiscriminate.Output |> createSubBuildingBlockDropdownLink state setState
                CompositeHeaderDiscriminate.Output    |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
                DropdownContentInfoFooter setUi false
            ]

        

    /// Protocol Type subpage for dropdown
    let dropdownContentProtocolTypeColumns state setUi setopen (annoState: Annotation list)(setAnnoState:CompositeHeaderDiscriminate option -> unit)(a) =
        React.fragment [
            CompositeHeaderDiscriminate.Date                |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
            CompositeHeaderDiscriminate.Performer           |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
            CompositeHeaderDiscriminate.ProtocolDescription |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
            CompositeHeaderDiscriminate.ProtocolType        |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
            CompositeHeaderDiscriminate.ProtocolUri         |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
            CompositeHeaderDiscriminate.ProtocolVersion     |> createBuildingBlockDropdownItem setUi setopen annoState setAnnoState a
            // Navigation element back to main page
            DropdownContentInfoFooter setUi true
            
        ]

    // / Output columns subpage for dropdown
    // let dropdownContentIOTypeColumns setState setopen (annoState: Annotation list)(setAnnoState: Annotation list -> unit)(a) (header: Types.CompositeHeaderDiscriminate)  =
    //     React.fragment [
    //         IOType.Source           |> createIOTypeDropdownItem setState setopen annoState setAnnoState a header
    //         IOType.Sample           |> createIOTypeDropdownItem setState setopen annoState setAnnoState a header
    //         IOType.Material         |> createIOTypeDropdownItem setState setopen annoState setAnnoState a header
    //         IOType.Data             |> createIOTypeDropdownItem setState setopen annoState setAnnoState a header
    //         IOType.FreeText ""      |> createIOTypeDropdownItem setState setopen annoState setAnnoState a header
    //         // Navigation element back to main page
    //         DropdownContentInfoFooter setState true
    //     ]

[<ReactComponent>]
let Main(state:  BuildingBlock.BuildingBlockUIState, setUi: BuildingBlock.BuildingBlockUIState -> unit, annoState: Annotation list, setAnnoState: CompositeHeaderDiscriminate option -> unit, a) =
    let isOpen, setOpen = React.useState false
    let updateDropdown = fun b -> setUi {state with DropdownIsActive = b}
    let close = fun _ -> updateDropdown false
    let dropdownRef:IRefValue<Browser.Types.HTMLElement option> = React.useRef(None)

    // Hook to handle clicks outside
    React.useEffect(fun () ->
        let handleClickOutside (event: Browser.Types.Event) =
            match dropdownRef.current with
            | Some element when element.contains(event.target :?> Browser.Types.Node) |> not ->
                close() //if clicked target is not the ref element, close the dropdown
            | _ -> ()
        Browser.Dom.document.addEventListener("click", handleClickOutside)
    )
        
    // Add event listener
    Components.BaseDropdown.Main(
        isOpen,
        setOpen,
        Daisy.button.button [
                prop.tabIndex 0
                button.info
                // prop.onClick (fun e -> log "hehehe")
                prop.role "button"
                join.item
                prop.className "flex-nowrap"
                prop.children [
                    Html.span (annoState[a].Search.KeyType.ToString())
                    Html.i [
                        prop.className "fa-solid fa-angle-down"
                    ]
                ]
                prop.ref dropdownRef
            ],
        [
            match state.DropdownPage with
            | DropdownPage.Main ->
                DropdownElements.dropdownContentMain state setUi setOpen annoState setAnnoState a 
            | DropdownPage.More ->
                DropdownElements.dropdownContentProtocolTypeColumns state setUi setOpen annoState setAnnoState a 
            | DropdownPage.IOTypes (iotype: Types.CompositeHeaderDiscriminate) -> Html.none
                // DropdownElements.dropdownContentIOTypeColumns  setState setOpen annoState setAnnoState a iotype
        ],
        style=Components.Style.init("join-item dropdown text-black z-20", Map [
            "content", Components.Style.init("!min-w-64")
        ])
    )

 