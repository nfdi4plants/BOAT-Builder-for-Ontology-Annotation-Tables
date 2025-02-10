namespace Components

open Feliz
open Feliz.DaisyUI
open Fable.Core

type Style =
    {
        classes: U2<string, string []>
        subClasses: Map<string, Style> option
    } with
        static member init(classes: string, ?subClasses: Map<string, Style>) =
            {
                classes = U2.Case1 classes
                subClasses = subClasses
            }
        static member init(classes: string [], ?subClasses: Map<string, Style>) =
            {
                classes = U2.Case2 classes
                subClasses = subClasses
            }

        member this.StyleString =
            match this.classes with
            | U2.Case1 style -> style
            | U2.Case2 styleArr -> styleArr |> String.concat " "

        member this.GetSubclassStyle(name: string) =
            match this.subClasses with
            | Some subClasses -> subClasses.TryFind name
            | None -> None
            |> Option.map _.StyleString

            |> Option.defaultValue ""


type BaseDropdown =
    [<ReactComponent>]
    static member Main(isOpen, setIsOpen, toggle: ReactElement, children: ReactElement seq, ?style: Style) =
        let ref = React.useElementRef()
        // React.useListener.onClickAway(ref, fun _ -> setIsOpen false)
        Html.div [
            prop.ref ref
            prop.className [
                "dropdown"
                if isOpen then "dropdown-open"
                if style.IsSome then style.Value.StyleString
            ]
            prop.children [
                toggle
                Html.ul [
                    prop.className [
                        "dropdown-content min-w-48 menu bg-base-200 rounded-box z-[1] p-2 shadow !top-[110%]"
                        if style.IsSome then style.Value.GetSubclassStyle "content"
                    ]
                    prop.children children
                ]
            ]
        ]