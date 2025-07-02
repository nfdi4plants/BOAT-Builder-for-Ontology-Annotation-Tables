namespace Components

open Feliz
open ARCtrl
open Feliz.DaisyUI
open Types

open Components.FunctionsContextmenu

module PreviewTable =

    let table (annoState: Annotation list, setState: Annotation list -> unit, toggleActive) =
        Html.div [
            prop.className "table border border-black"
            prop.children [
                Daisy.table [
                    prop.className "border-b border-black bg-white"
                    prop.children [
                        if annoState = [] then
                            Html.none
                        else
                            Html.thead [
                                Html.tr [
                                    Html.th [prop.text "No."; prop.className "border border-black text-black"]
                                    Html.th [prop.text "Key";prop.className "border border-black text-black"]
                                    Html.th [prop.text "KeyType";prop.className "border border-black text-black"]
                                    Html.th [prop.text "Term";prop.className "border border-black text-black"]
                                    Html.th [prop.text "Value (Unit)";prop.className "border border-black text-black"]
                                    Html.th [prop.text "";prop.className "border border-black text-black"]
                                ]
                            ]
                        Html.tbody [
                        for a in 0 .. annoState.Length - 1 do
                        
                            // let isBodyempty = 
                            //     match annoState[a].Search.Body with
                            //     | CompositeCell.Term oa -> System.String.IsNullOrWhiteSpace oa.NameText
                            //     | CompositeCell.Unitized (v,oa) -> System.String.IsNullOrWhiteSpace v
                            //     | _ -> true
                                
                            // let isKeyempty = System.String.IsNullOrWhiteSpace annoState[a].Search.Key.NameText

                            // if isBodyempty && isKeyempty  then
                            //     Html.tr []
                            // else
                                Html.tr [
                                    prop.children [
                                        Html.td [prop.text(a + 1); prop.className "border border-black text-black"]
                                        Html.td [prop.text (annoState[a].Search.Key.NameText);prop.className "border border-black text-black"]
                                        Html.td [prop.text (annoState[a].Search.KeyType.ToString()); prop.className "border border-black text-black"]
                                        match annoState[a].Search.Body with
                                        | (CompositeCell.Term oa) -> 
                                            Html.td [prop.text(oa.NameText); prop.className "border border-black text-black"]
                                            Html.td [prop.text ""; prop.className "border border-black text-black"]
                                        | (CompositeCell.Unitized (v,oa)) ->
                                            Html.td [prop.text(oa.NameText); prop.className "border border-black text-black"]
                                            Html.td [prop.text v; prop.className "border border-black text-black"]
                                        |_ -> ()
                                        Html.td [
                                            prop.className "border border-black text-black"
                                            prop.children [
                                                Html.button [
                                                    prop.className "text-black cursor-pointer hover:text-error"
                                                    prop.onClick (fun _ -> 
                                                        let newAnnoList: Annotation list = annoState |> List.filter (fun x -> x = annoState[a] |> not)  
                                                        setState newAnnoList
                                                       
                                                            )
                                                    prop.children [
                                                        Html.i [
                                                            prop.className "fa-regular fa-trash-can cursor-pointer hover:text-error"
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
            ]
        ]