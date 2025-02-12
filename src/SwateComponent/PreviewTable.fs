namespace Components

open Feliz
open ARCtrl
open Feliz.DaisyUI
open Types

module PreviewTable =

    let table (annoState: Annotation list, setState: Annotation list -> unit) =
        Html.div [
            prop.className "w-96 bg-white"
            prop.children [
                Daisy.table [
                    prop.className "bg-white"
                    prop.children [
                        if annoState = [] then
                            Html.head []
                        else
                            Html.thead [
                                Html.tr [
                                    Html.th [prop.text "No.";prop.style [style.color.black]]
                                    Html.th [prop.text "Key";prop.style [style.color.black]]
                                    Html.th [prop.text "KeyType";prop.style [style.color.black]]
                                    Html.th [prop.text "Term";prop.style [style.color.black]]
                                    Html.th [prop.text "Value (if unitized)";prop.style [style.color.black]]
                                    Html.th [prop.text "";prop.style [style.color.black]]
                                ]
                            ]
                        Html.tbody [
                        for a in 0 .. annoState.Length - 1 do
                            let isBodyempty = 
                                match annoState[a].Search.Body with
                                | CompositeCell.Term oa -> System.String.IsNullOrWhiteSpace oa.NameText
                                | CompositeCell.Unitized (v,oa) -> System.String.IsNullOrWhiteSpace v
                                | _ -> true
                                
                            let isKeyempty = System.String.IsNullOrWhiteSpace annoState[a].Search.Key.NameText

                            if isBodyempty && isKeyempty  then
                                Html.tr []
                            else
                                Html.tr [
                                    prop.children [
                                        Html.td [prop.text(a + 1); prop.style [style.color.black]]
                                        Html.td [prop.text (annoState[a].Search.Key.NameText); prop.style [style.color.black]]
                                        Html.td [prop.text (annoState[a].Search.KeyType.ToString()); prop.style [style.color.black]]
                                        match annoState[a].Search.Body with
                                        | (CompositeCell.Term oa) -> 
                                            Html.td [prop.text(oa.NameText); prop.style [style.color.black]]
                                            Html.td ""
                                        | (CompositeCell.Unitized (v,oa)) ->
                                            Html.td [prop.text(oa.NameText); prop.style [style.color.black]]
                                            Html.td [prop.text v; prop.style [style.color.black]]
                                        |_ -> ()
                                        Html.td [
                                            Html.button [
                                                prop.className "text-black"
                                                prop.onClick (fun _ -> 
                                                    let newAnnoList: Annotation list = annoState |> List.filter (fun x -> x = annoState[a] |> not)  
                                                    setState newAnnoList
                                                )
                                                prop.children [
                                                    Html.i [
                                                        prop.className "fa-regular fa-trash-can"
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