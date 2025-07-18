namespace Components

open Feliz
open Browser.Types
open ARCtrl
open Shared
open Shared.Database
open Shared.DTOs.TermQuery
open Shared.DTOs.ParentTermQuery
open Fable.Core.JsInterop
open Fable.Remoting.Client
open Feliz.DaisyUI

module TermSearchAux =

    let [<Literal>] SelectAreaID = "TermSearch_SelectArea"

    [<RequireQualifiedAccess>]
    type SearchIs =
    | Idle
    | Running
    | Done

    type SearchState = {
        SearchIs: SearchIs
        Results: Term []
    } with
        static member init(?running:bool) = {
            SearchIs = if running.IsSome && running.Value then SearchIs.Running else SearchIs.Idle
            Results = [||]
        }
    let ontology : IOntologyAPIv3 =
        Remoting.createApi()
        |> Remoting.withRouteBuilder Route.builder
        |> Remoting.buildProxy<IOntologyAPIv3>

    let searchByName(query: string, setResults: Term [] -> unit) =
        async {
            let query = TermQueryDto.create(query, 10)
            let! terms = 
                ontology.searchTerm query
            setResults terms
        }

    let searchByParent(query: string, parentTAN: string, setResults: Term [] -> unit) =
        async {
            let query = TermQueryDto.create(query, 50, parentTAN)
            let! terms = ontology.searchTerm query
            setResults terms
        }

    let findAllChildTerms(parentTAN: string, setResults: Term [] -> unit) =
        async {
            let query = ParentTermQueryDto.create(parentTAN, 50)
            let! terms = ontology.findAllChildTerms query
            setResults terms.results
        }

    let allByParentSearch (
        parent: OntologyAnnotation,
        setSearchTreeState: SearchState -> unit,
        setLoading: bool -> unit,
        stopSearch: unit -> unit,
        debounceStorage: DebounceStorage,
        debounceTimer: int
    ) =
        let queryDB() =
            [
                async {
                    ClickOutsideHandler.AddListenerString(SelectAreaID, fun e -> stopSearch())
                }
                findAllChildTerms(parent.TermAccessionShort, fun terms -> setSearchTreeState {Results = terms; SearchIs = SearchIs.Done})
            ]
            |> Async.Parallel
            |> Async.Ignore
            |> Async.StartImmediate
        setSearchTreeState <| {Results = [||]; SearchIs = SearchIs.Running}
        debouncel debounceStorage "TermSearch" debounceTimer setLoading queryDB ()

    let mainSearch (
        queryString: string,
        parent: OntologyAnnotation option,
        setSearchNameState: SearchState -> unit,
        setSearchTreeState: SearchState -> unit,
        setLoading: bool -> unit,
        stopSearch: unit -> unit,
        debounceStorage: DebounceStorage,
        debounceTimer: int
    ) =
        let queryDB() =
            [
                async {
                    ClickOutsideHandler.AddListenerString(SelectAreaID, fun e -> stopSearch())
                }
                searchByName(queryString, fun terms -> setSearchNameState {Results = terms; SearchIs = SearchIs.Done })
                if parent.IsSome then searchByParent(queryString, parent.Value.TermAccessionShort, fun terms -> setSearchTreeState {Results = terms; SearchIs = SearchIs.Done })
            ]
            |> Async.Parallel
            |> Async.Ignore
            |> Async.StartImmediate
        setSearchNameState <| SearchState.init()
        debouncel debounceStorage "TermSearch" debounceTimer setLoading queryDB ()

    module Components =

        let termSeachNoResults (advancedTermSearchActiveSetter: (bool -> unit) option) =
            Html.div [
                
                prop.className "col-span-4 gap-y-2"
                prop.children [
                    Html.div [
                        prop.key $"TermSelectItem_NoResults"
                        prop.children [
                            Html.div "No terms found matching your input."
                        ]
                    ]
                    if advancedTermSearchActiveSetter.IsSome then
                        Html.div [
                            prop.key $"TermSelectItem_Suggestion"
                            prop.classes ["term-select-item"]
                            prop.children [
                                Html.span "Can't find the term you are looking for? "
                                Html.a [
                                    prop.className "link link-primary"
                                    prop.onClick(fun e -> e.preventDefault(); e.stopPropagation(); advancedTermSearchActiveSetter.Value true)
                                    prop.text "Try Advanced Search!"
                                ]
                            ]
                        ]
                    Html.div [
                        prop.key $"TermSelectItem_Contact"
                        prop.classes ["term-select-item"]
                        prop.children [
                            Html.div [
                                Html.span "Still can't find what you need? Get in "
                                Html.a [prop.href (@"https://support.nfdi4plants.org" + "/?topic=Metadata_OntologyUpdate") ; prop.target.blank; prop.text "contact"; prop.className "link link-primary"]
                                Html.span " with us!"
                            ]
                        ]
                    ]
                ]
            ]

        let loadingIcon (loading: bool) =
            Daisy.loading [
                prop.className [
                    if not loading then "invisible";
                ]
            ]
        let searchIcon = Html.i [prop.className "fa-solid fa-magnifying-glass text-black"]
        let verifiedIcon = Html.i [prop.className "fa-solid fa-check text-primary"]
        let termSelectItemMain (term: Term, show, setShow, setTerm, isDirectedSearchResult: bool) =
            Html.div [
                prop.className "grid grid-cols-subgrid col-span-4 gap-2 cursor-pointer hover:bg-[#ffecb3] transition-colors py-0.5 items-center"
                prop.onClick setTerm
                prop.title term.Name
                prop.children [
                    Html.i [
                        prop.style [style.width (length.px 20)]
                        prop.className [
                            if term.IsObsolete then
                                "fa-solid fa-link-slash text-error";
                            elif isDirectedSearchResult then
                                "fa-solid fa-diagram-project text-primary"
                        ]
                        if term.IsObsolete then
                            prop.title "Obsolete"
                        elif isDirectedSearchResult then
                            prop.title "Related Term"
                    ]
                    Html.div [
                        prop.className "font-bold grow truncate"
                        prop.text term.Name
                    ]
                    Html.div [
                        prop.className "grow"
                        prop.children [
                            Html.a [
                                prop.href (ARCtrl.OntologyAnnotation(tan=term.Accession).TermAccessionOntobeeUrl)
                                prop.target.blank
                                prop.onClick(fun e -> e.stopPropagation())
                                prop.text term.Accession
                            ]
                        ]
                    ]
                    FileUpload.CollapseButton(show, setShow, classes="btn-sm btn-ghost")
                ]
            ]

        let termSelectItemMore (term: Term, show) =
            Html.div [
                prop.className [
                    // padding is sum of padding + possible icon of main term info
                    "col-span-4 pl-[28px] grid grid-cols-[auto,1fr] gap-x-2 border-b"
                    if not show then "hidden";
                ]
                prop.children [
                    Html.div [ prop.className "font-semibold"; prop.text "Name:"]
                    Html.div [ prop.text (if System.String.IsNullOrWhiteSpace term.Name then "<no name>" else term.Name)]
                    Html.div [ prop.className "font-semibold"; prop.text "Description:"]
                    Html.div [ prop.text (if System.String.IsNullOrWhiteSpace term.Description then "<no description>" else term.Description)]
                    Html.div [ prop.className "font-semibold"; prop.text "Source:"]
                    Html.div [ prop.text (term.FK_Ontology)]
                    if term.IsObsolete then
                        Html.div [ prop.className "font-semibold text-error"; prop.text "Obsolete"]
                        Html.div [ ]
                ]
            ]

open TermSearchAux
open Fable.Core.JsInterop

type TermSearch =

    [<ReactComponent>]
    static member TermSelectItem (term: Term, setTerm, ?isDirectedSearchResult: bool) =
        let isDirectedSearchResult = defaultArg isDirectedSearchResult false
        let show, setShow = React.useState(false)
        Html.div [
            prop.className "grid grid-cols-subgrid col-span-4"
            prop.key $"TermSelectItem_{term.Accession}"
            prop.children [
                Components.termSelectItemMain(term, show, setShow, setTerm, isDirectedSearchResult)
                Components.termSelectItemMore(term, show)
            ]
        ]

    static member TermSelectArea (id: string, searchNameState: SearchState, searchTreeState: SearchState, setTerm: Term option -> unit, show: bool, setAdvancedTermSearchActive) =
        let searchesAreComplete = searchNameState.SearchIs = SearchIs.Done && searchTreeState.SearchIs = SearchIs.Done
        let foundInBoth (term:Term) =
            (searchTreeState.Results |> Array.contains term)
            && (searchNameState.Results |> Array.contains term)
        let matchSearchState (ss: SearchState) (isDirectedSearch: bool) =
            match ss with
            | {SearchIs = SearchIs.Done; Results = [||]} when isDirectedSearch ->
                Components.termSeachNoResults setAdvancedTermSearchActive
            | {SearchIs = SearchIs.Done; Results = results} ->
                React.fragment [
                    for term in results do
                        let setTerm = fun (e: MouseEvent) -> setTerm (Some term)
                        // Term is found in both: Do not show in real directed search, update first search hit instead
                        if searchesAreComplete && foundInBoth term then
                            if isDirectedSearch then
                                Html.none
                            else
                                TermSearch.TermSelectItem (term, setTerm, true)
                        else
                            TermSearch.TermSelectItem (term, setTerm, isDirectedSearch)
                    ]
            | _ -> Html.none
        Html.div [
            prop.id id
            prop.className [
                "top-full scrollbar-gutter:stable grid grid-cols-[auto,1fr,1fr,auto] absolute left-0 z-20 w-full
                bg-[#fff2cc] rounded shadow-md border-2 border-info py-2 pl-4 max-h-[400px] overflow-y-auto w-full"
                if not show then "hidden"
            ]
            prop.children [
                match searchNameState.SearchIs, searchTreeState.SearchIs, show with
                | SearchIs.Done, _,_ | _, SearchIs.Done, _->
                    matchSearchState searchNameState false
                    matchSearchState searchTreeState true
                | _,_, true -> 
                    Html.div [
                        prop.className "px-3 col-span-4"
                        prop.children [
                            // Daisy.loading [
                            //     loading.dots
                            //     prop.className "text-primary inline-block align-middle"
                            // ]
                        ]
                    ]
                | _ ->
                    Html.none
            ]
        ]

    [<ReactComponent>]
    static member Input (
        setter: OntologyAnnotation option -> unit,
        ?input: OntologyAnnotation , ?parent: OntologyAnnotation,
        ?isSearchable: bool,
        // ?advancedSearchDispatch: Messages.Msg -> unit,
        ?portalTermSelectArea: HTMLElement,
        ?onBlur: Event -> unit, ?onEscape: KeyboardEvent -> unit, ?onEnter: KeyboardEvent -> unit, ?onFocus: FocusEvent -> Fable.Core.JS.Promise<unit>,
        ?autofocus: bool, ?fullwidth: bool, ?isjoin: bool, ?displayParent: bool, ?classes: string)
        =
        let isjoin = defaultArg isjoin false
        let isSearchable = defaultArg isSearchable true
        let autofocus = defaultArg autofocus false
        let displayParent = defaultArg displayParent true
        let advancedSearchActive, setAdvancedSearchActive = React.useState(false)
        let fullwidth = defaultArg fullwidth false
        let loading, setLoading = React.useState(false)
        let searchNameState, setSearchNameState = React.useState(SearchState.init)
        let searchTreeState, setSearchTreeState = React.useState(SearchState.init)
        let isSearching, setIsSearching = React.useState(false)
        let debounceStorage = React.useRef(newDebounceStorage())
        let ref = React.useElementRef()
        let inputRef = React.useInputRef()
        React.useEffect(
            (fun () ->
                if inputRef.current.IsSome && input.IsSome
                    then inputRef.current.Value.value <- input.Value.NameText
                else ()
            ),
            [|box input|]
        )

        React.useLayoutEffectOnce(fun _ ->
            ClickOutsideHandler.AddListenerElement (ref, fun e ->
                debounceStorage.current.ClearAndRun()
                if onBlur.IsSome then onBlur.Value e
            )
        )
        let stopSearch() =
            debounceStorage.current.Remove("TermSearch") |> ignore
            setLoading false
            setIsSearching false
            setSearchTreeState {searchTreeState with SearchIs = SearchIs.Idle}
            setSearchNameState {searchNameState with SearchIs = SearchIs.Idle}
        let selectTerm (t:Term option) =
            let oaOpt = t |> Option.map OntologyAnnotation.fromTerm
            setter oaOpt
            if inputRef.current.IsSome then
                inputRef.current.Value.value <- oaOpt |> Option.map (fun oa -> oa.Name) |> Option.flatten |> Option.defaultValue ""
            setIsSearching false
        let startSearch() =
        
            setLoading true
            setSearchNameState <| SearchState.init(running=true)
            setSearchTreeState <| SearchState.init(running=true)
            setIsSearching true
        let registerChange(searchTest: string option) =
            let oa = searchTest  |> Option.map (fun x -> OntologyAnnotation x)

            debouncel debounceStorage.current "SetterDebounce" 500 setLoading setter oa

        React.useLayoutEffect((
            fun _ ->
                if autofocus && inputRef.current.IsSome then
                    inputRef.current.Value.focus()
        ), [|box parent|])
        Daisy.formControl [
                prop.className "w-full"
                prop.children [
                    Html.div [
                        prop.className [
                            "input input-bordered flex items-center gap-2 relative"
                            if isjoin then "join-item";
                            if classes.IsSome then classes.Value;
                        ]
                        prop.ref ref
                        prop.style [
                            if fullwidth then style.flexGrow 1;
                        ]
                        prop.children [
                            Components.searchIcon
                            Html.input [
                                prop.className "grow text-black"
                                prop.autoFocus autofocus
                                if input.IsSome then prop.valueOrDefault input.Value.NameText
                                prop.ref inputRef
                                prop.onMouseDown(fun e ->
                                    e.stopPropagation()
                                )
                                if onFocus.IsSome then
                                    prop.onFocus(fun fe ->
                                        promise {
                                            do! onFocus.Value fe
                                            inputRef.current.Value.focus()
                                        }
                                        |> Promise.start
                                    )
                                prop.onDoubleClick(fun e ->
                                    let s : string = e.target?value
                                    if s.Trim() = "" && parent.IsSome && parent.Value.TermAccessionShort <> "" then // trigger get all by parent search
                                        log "Double click empty + parent"
                                        if isSearchable then
                                            startSearch()
                                            allByParentSearch(parent.Value, setSearchTreeState, setLoading, stopSearch, debounceStorage.current, 0)
                                    elif s.Trim() <> "" then
                                        log "Double click not empty"
                                        if isSearchable then
                                            startSearch()
                                            mainSearch(s, parent, setSearchNameState, setSearchTreeState, setLoading, stopSearch, debounceStorage.current, 0)
                                    else
                                        ()
                                )
                                prop.onChange(fun (s: string) ->
                                    if System.String.IsNullOrWhiteSpace s then
                                        log "no input"
                                        registerChange(None)
                                        stopSearch() // When deleting text this should stop search from completing
                                    else
                                        registerChange(Some s)
                                        if isSearchable then
                                            startSearch()
                                            mainSearch(s, parent, setSearchNameState, setSearchTreeState, setLoading, stopSearch, debounceStorage.current, 1000)
                                )
                                prop.onKeyDown(fun e ->
                                    e.stopPropagation()
                                    match e.which with
                                    | 27. -> //escape
                                        stopSearch()
                                        debounceStorage.current.ClearAndRun()
                                        if onEscape.IsSome then onEscape.Value e
                                    | 13. -> //enter
                                        debounceStorage.current.ClearAndRun()
                                        if onEnter.IsSome then onEnter.Value e
                                    | _ -> ()
                                )
                            ]
                            let TermSelectArea = TermSearch.TermSelectArea (SelectAreaID, searchNameState, searchTreeState, selectTerm, isSearching, None
                                // (if advancedSearchDispatch.IsSome then Some setAdvancedSearchActive else None)
                                )
                            if portalTermSelectArea.IsSome then
                                ReactDOM.createPortal(TermSelectArea, portalTermSelectArea.Value)
                            elif ref.current.IsSome then
                                ReactDOM.createPortal(TermSelectArea, ref.current.Value)
                            else
                                TermSelectArea
                            Components.loadingIcon loading
                            if input.IsSome && input.Value.Name.IsSome && input.Value.TermAccessionNumber.IsSome && not isSearching then Components.verifiedIcon
                        ]
                    ]
                    // if (parent.IsSome && displayParent) 
                    // // || advancedSearchDispatch.IsSome 
                    // then
                    //     // Optional elements
                    //     Html.div [
                    //         prop.className "label not-prose"
                    //         prop.children [
                    //             if parent.IsSome && displayParent then
                    //                 Html.span [
                    //                     prop.className "text-sm label-text-alt"
                    //                     prop.children [
                    //                         Html.span "Parent: "
                    //                         Html.span $"{parent.Value.NameText}, {parent.Value.TermAccessionShort}"
                    //                     ]
                    //                 ]
                    //             // if advancedSearchDispatch.IsSome then
                    //             //     Components.AdvancedSearch.Main(advancedSearchActive, setAdvancedSearchActive, (fun t ->
                    //             //         setAdvancedSearchActive false
                    //             //         Some t |> selectTerm),
                    //             //         advancedSearchDispatch.Value
                    //             //     )
                    //             //     Html.span [
                    //             //         prop.className "label-text-alt link-primary cursor-pointer"
                    //             //         prop.onClick(fun e -> e.preventDefault(); e.stopPropagation(); setAdvancedSearchActive true)
                    //             //         prop.text "Use advanced search"
                    //             //     ]
                    //             ]
                    // ]
                ]
            
        ]
