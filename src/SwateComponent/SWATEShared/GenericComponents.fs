namespace Components

open Feliz

[<AutoOpen>]

type Components =

    static member CollapseButton(isCollapsed, setIsCollapsed, ?collapsedIcon, ?collapseIcon, ?classes: string) =
        Html.label [
            prop.className [
                "btn btn-square swap swap-rotate grow-0"
                if classes.IsSome then classes.Value
            ]
            prop.onClick (fun e -> e.preventDefault(); e.stopPropagation(); not isCollapsed |> setIsCollapsed)
            prop.children [
                Html.input [prop.type'.checkbox; prop.isChecked isCollapsed; prop.onChange(fun (_:bool) -> ())]
                Html.i [
                    prop.className [
                        "swap-off fa-solid"
                        if collapsedIcon.IsSome then collapsedIcon.Value else "fa-solid fa-chevron-down"
                    ]
                ]
                Html.i [
                    prop.className [
                        "swap-on"
                        if collapseIcon.IsSome then collapseIcon.Value else "fa-solid fa-x"
                    ]
                ]
            ]
        ]