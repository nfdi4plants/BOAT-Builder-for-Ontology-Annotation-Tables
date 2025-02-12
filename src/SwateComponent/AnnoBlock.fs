
namespace App

open Feliz
open Feliz.Bulma
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
    let SearchElementKey (ui, setUi,annoState: Annotation list, setAnnoState, a) = //missing ui and setui for dropdown
        let element = React.useElementRef()
        Html.div [
            prop.ref element // The ref must be place here, otherwise the portalled term select area will trigger daisy join syntax
            prop.className "relative"
            prop.children [
                Daisy.join [
                    prop.className "w-full z-30"
                    prop.children [
                        // Choose building block type dropdown element
                        // Dropdown building block type choice
                        BuildingBlock.Dropdown.Main(ui, setUi, annoState, setAnnoState, a)
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
                        // elif model.HeaderCellType.HasIOType() then
                        //     Daisy.input [
                        //         prop.readOnly true
                        //         prop.valueOrDefault (
                        //             model.TryHeaderIO()
                        //             |> Option.get
                        //             |> _.ToString()
                        //         )
                        //     ]
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
                    prop.className "w-full z-30"
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

[<AutoOpen>]

module private ARCtrlExtensions =
    type CompositeCell with
        member this.UpdateWithString(s: string) =
            match this with
            |CompositeCell.Unitized (_,oa) ->
                CompositeCell.Unitized (s,oa) 
            |_ -> this
    
type Components =
    
    [<ReactComponent>]
    static member AnnoBlockwithSwate(annoState: Annotation list, setState: Annotation list -> unit, index: int) =
       
        let (ui: BuildingBlock.BuildingBlockUIState, setUi) = React.useState(BuildingBlock.BuildingBlockUIState.init)        

        let revIndex = annoState.Length - 1 - index
        let a = annoState.[revIndex]

        Bulma.block [
            prop.style [
                style.position.absolute
                //if a.IsOpen = true then annoState |> List.map (fun e -> style.top (int e.Height + 40)) 
                //else style.top (int a.Height)
                style.top (int a.Height)
            ]
            prop.children [
            if annoState[revIndex].IsOpen = false then 
                Html.button [
                    Html.i [
                        prop.className "fa-solid fa-comment-dots"
                        prop.style [style.color "#ffe699"]
                        prop.onClick (fun e ->
                            
                            Helperfuncs.updateAnnotation ((fun e -> e.ToggleOpen()), revIndex, annoState, setState)                            
                        )
                    ]
                ] 
            else
                Html.div [
                    prop.className "bg-[#ffe699] p-3 text-black z-30 w-fit"
                    prop.children [
                        Bulma.columns [
                            Bulma.column [
                                column.is1
                                prop.className "hover:bg-[#ffd966] cursor-pointer"
                                prop.onClick (fun e -> Helperfuncs.updateAnnotation ((fun a -> a.ToggleOpen()), revIndex, annoState, setState))
                                prop.children [
                                    Html.span [
                                        Html.i [
                                            prop.className "fa-solid fa-chevron-left"
                                        ]
                                    ]
                                ]
                            ]
                            Bulma.column [
                                prop.className "space-y-2"
                                prop.children [
                                    Html.span [
                                        prop.className "delete float-right mt-0 mb-2 z-30"
                                        prop.onClick (fun _ -> 
                                            let newAnnoList: Annotation list = annoState |> List.filter (fun x -> x = annoState[revIndex] |> not)  
                                            // List.removeAt (List.filter (fun x -> x = a) state) state
                                            setState newAnnoList
                                        )
                                    ]
                                    Searchblock.SearchElementKey (ui, setUi,annoState, setState, revIndex)
                                    if annoState[revIndex].Search.KeyType.IsTermColumn() then
                                        Searchblock.SearchElementBody(revIndex, annoState, setState)
                                        if annoState[revIndex].Search.Body.isUnitized then
                                            Daisy.formControl [
                                                Daisy.join [
                                                    Daisy.input [
                                                        prop.autoFocus true
                                                        prop.placeholder "Value..."
                                                        prop.onChange (fun (s:string) ->
                                                            Helperfuncs.updateAnnotation((fun anno -> 
                                                                let nextCell = {anno with Search.Body = anno.Search.Body.UpdateWithString(s)}
                                                                nextCell
                                                            ),revIndex,annoState, setState)
                                                        )
                                                    ]
                                                ]
                                            ]
                                ]
                            ]
                        ]
                    ]  
                ] 
            ]
        ]
