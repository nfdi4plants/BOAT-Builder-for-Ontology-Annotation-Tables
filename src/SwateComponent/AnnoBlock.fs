
namespace App

open Feliz
open ARCtrl
open Feliz.DaisyUI
open Shared
open Fable.Core
open Fable.Core.JsInterop 
open Types

module private Helperfuncs =
    let updateAnnotation (func:Annotation -> Annotation, indx: int, annoState: Annotation list, setState: Annotation list -> unit) =
            let nextA = func annoState[indx]
            annoState |> List.mapi (fun i a ->
                if i = indx  then nextA else a 
            ) |> setState

[<AutoOpen>]
module private ARCtrlExtensions =
    type CompositeCell with
        member this.UpdateWithString(s: string) =
            match this with
            |CompositeCell.Unitized (_,oa) ->
                CompositeCell.Unitized (s,oa) 
            |_ -> this

module Searchblock =

    let TermOrUnitizedSwitch ( a: int, annoState: Annotation list, setState: Annotation list -> unit) =
        React.fragment [
            Daisy.button.a [
                join.item
                let isActive = annoState[a].Search.Body.isTerm
                if isActive then button.info
                prop.onClick (fun _ -> 
                    (annoState |> List.mapi (fun i e ->
                        if i = a then {e with Search.Body = e.Search.Body.ToTermCell()}
                        elif i= a then {e with Search.Body = e.Search.Body.UpdateWithString("")}
                        else e
                    )) |> setState

                )
                prop.text "Term"
            ]
            Daisy.button.a [
                join.item
                let isActive = annoState[a].Search.Body.isUnitized
                if isActive then button.info
                prop.onClick (fun _ -> 
                    (annoState |> List.mapi (fun i e ->
                        if i = a then {e with Search.Body = e.Search.Body.ToUnitizedCell()}
                        else e
                    )) |> setState
                )
                prop.text "Unit"
            ]
        ]

    [<ReactComponent>]
    let SearchElementKey (ui, setUi,annoState: Annotation list, setAnnoState, a ) = //missing ui and setui for dropdown
        let element = React.useElementRef()
        Html.div [
            prop.ref element // The ref must be place here, otherwise the portalled term select area will trigger daisy join syntax
            prop.className "relative"
            prop.children [
                Daisy.join [
                    prop.className "w-full z-20 text-white"
                    prop.children [
                        // Choose building block type dropdown element
                        // Dropdown building block type choice
                        let setKeyType (cHDOpt: CompositeHeaderDiscriminate option) =
                            let cHD = cHDOpt |> Option.defaultValue CompositeHeaderDiscriminate.Parameter
                            Helperfuncs.updateAnnotation((fun anno -> 
                                {anno with Search.KeyType = cHD }
                            ), a, annoState, setAnnoState)
                        Dropdown.Main(ui, setUi, annoState, setKeyType, a)
                        // Term search field
                        // if model.HeaderCellType.HasOA() then
                        let setter (oaOpt: OntologyAnnotation option) =
                            let oa = oaOpt |> Option.defaultValue (OntologyAnnotation())
                            Helperfuncs.updateAnnotation((fun anno -> 
                                {anno with Search.Key = oa}
                            ), a, annoState, setAnnoState)
                            //selectHeader ui setUi h |> dispatch
                        let input = annoState[a].Search.Key
                        Components.TermSearch.Input(setter, fullwidth=true, input=input, isjoin=true, ?portalTermSelectArea=element.current, classes="")
                    ]
                ]
            ]
        ]

    [<ReactComponent>]
    let SearchElementBody ( a, annoState, setAnnoState) =
        let element = React.useElementRef()
        Html.div [
            prop.ref element
            prop.className "relative"
            prop.children [
                Daisy.join [
                    prop.className "w-full z-20 text-white"
                    prop.children [
                        TermOrUnitizedSwitch (a, annoState, setAnnoState)
                        // helper for setting the body cell type
                        let setter (oaOpt: OntologyAnnotation option) =
                            Helperfuncs.updateAnnotation((fun anno -> 
                                let oa = oaOpt |> Option.defaultValue (OntologyAnnotation()) 
                                let nextCell = anno.Search.Body.UpdateWithOA(oa)
                                {anno with Search.Body = nextCell}
                                
                            ), a, annoState, setAnnoState)

                        // let parent = model.TryHeaderOA()
                        let input = annoState[a].Search.Body.ToOA()
                        Components.TermSearch.Input(setter, fullwidth=true, input=input, 
                        parent=input, displayParent=false, ?portalTermSelectArea=element.current, isjoin=true, classes="")   
                    ]
                ]
            ]
        ]

    

open Components.FunctionsContextmenu

type Components =
    
    [<ReactComponent>]
    static member AnnoBlockwithSwate(annoState: Annotation list, setState: Annotation list -> unit, index: int) =
       
        let (ui: BuildingBlock.BuildingBlockUIState, setUi) = React.useState(BuildingBlock.BuildingBlockUIState.init)        

        let a = annoState.[index] 
            
        let mapOfSameHeight =
            annoState 
            |> List.groupBy (fun a -> a.Height)
            |> Map.ofList

        let deleteButton (specIndex: int) =
                    Html.span [
                        prop.className "mt-0 cursor-pointer hover:text-error transition-colors"
                        prop.onClick (fun _ -> 
                            annoState 
                            |> List.filter ((<>) annoState[specIndex])
                            |> setState
                        )
                        prop.children [
                            Html.span [
                                Html.i [prop.className "fa-solid fa-trash-can"]
                            ]
                        ]
                    ]

        let closeButton (specIndex: int) =
            Html.div [
                prop.className "cursor-pointer hover:text-info" 
                prop.onClick (fun e -> Helperfuncs.updateAnnotation ((fun a -> a.ToggleOpen()), specIndex, annoState, setState))
                prop.children [
                    Html.span [
                        Html.i [prop.className "fa-solid fa-chevron-left"]
                    ]
                ]
            ]

        let valueInput (specIndex: int)  =
            if annoState[specIndex].Search.Body.isUnitized then
                Daisy.formControl [
                    prop.className "max-w-32"
                    prop.children [
                        Html.div [
                            prop.className "input input-bordered flex items-center gap-2 relative"
                            prop.children [
                                Html.input [
                                    prop.className "grow text-black"
                                    prop.placeholder "Value..."
                                    prop.onChange (fun (s:string) ->
                                        Helperfuncs.updateAnnotation((fun anno -> 
                                            {anno with Search.Body = anno.Search.Body.UpdateWithString(s)}
                                        ), specIndex, annoState, setState)
                                    )
                                    match annoState.[specIndex].Search.Body with
                                    | CompositeCell.Unitized (v, _) -> prop.valueOrDefault v
                                    | _ -> ()
                                ]
                            ]
                        ]
                    ]
                ]
            else Html.none

        let annotationNote (specIndex: int) (hasChevron: bool) =
            Html.div [
                    prop.className "bg-[#ffe699] p-3 text-black z-auto w-fit"
                    prop.children [
                        Html.div [
                            prop.className "flex flex-row"
                            prop.children [
                                if hasChevron then closeButton specIndex
                                Html.div [
                                    prop.className "space-y-2 flex flex-col gap-2"
                                    prop.children [
                                        Html.div [
                                            prop.className "flex flex-row justify-end"
                                            prop.children [deleteButton specIndex]
                                        ]
                                        Searchblock.SearchElementKey (ui, setUi, annoState, setState, specIndex)
                                        if annoState[specIndex].Search.KeyType.IsTermColumn() then
                                            Searchblock.SearchElementBody(specIndex, annoState, setState)
                                            valueInput specIndex
                                    ]
                                ]
                            ]
                        ]
                    ]  
                ] 

    
        Html.div [
            prop.style [
                style.position.absolute
                style.top (int a.Height + 30)
            ]
            prop.children [
                if mapOfSameHeight[a.Height].Length = 1 && a.IsOpen = false then 
                    Html.button [
                        prop.className "cursor-pointer"
                        prop.children [
                            Html.i [
                                prop.className "fa-solid fa-comment-dots"
                                prop.style [style.color "#ffe699"]
                                prop.onClick (fun e ->
                                    Helperfuncs.updateAnnotation ((fun e -> e.ToggleOpen()), index, annoState, setState) 
                                    let updatedAnnos = 
                                        annoState 
                                        |> List.mapi (fun i anno -> 
                                            if i = index then anno.ToggleOpen()
                                            else {anno with IsOpen = false}
                                        )
                                    setState updatedAnnos                    
                                )
                            ]
                        ]
                    ] 
               
                elif mapOfSameHeight[a.Height].Length = 1 && a.IsOpen = true then               
                    annotationNote index true
                elif mapOfSameHeight[a.Height].Length > 1 &&  not (mapOfSameHeight[a.Height] |> List.exists (fun anno -> anno.IsOpen)) then
                        Daisy.indicator [
                            Daisy.indicatorItem [prop.text (string mapOfSameHeight[a.Height].Length); prop.className "badge badge-secondary badge-sm"]
                            Html.button [
                                prop.className "cursor-pointer"
                                prop.children [
                                    Html.i [
                                        prop.className "fa-solid fa-comment-dots"
                                        prop.style [style.color "#ffe699"]
                                        prop.onClick (fun e ->
                                            let updatedAnnos = 
                                                annoState 
                                                |> List.mapi (fun i anno -> 
                                                    if i = index then anno.ToggleOpen()
                                                    else {anno with IsOpen = false}
                                                )
                                            setState updatedAnnos                            
                                        )
                                    ]
                                ]
                            ] 
                        ]


                elif mapOfSameHeight[a.Height].Length > 1 && a.IsOpen = true then 
                    Html.div [
                        prop.className "flex flex-col gap-2 border border-3 border-secondary p-2 bg-white z-20"
                        prop.children [
                            closeButton index
                            for i in 0 .. mapOfSameHeight[a.Height].Length - 1 do
                                let newIndex = 
                                    annoState 
                                    |> List.findIndex (fun anno -> anno = mapOfSameHeight[a.Height][i])
                                annotationNote newIndex false
                        ]
                    ]
            ]
        ]
