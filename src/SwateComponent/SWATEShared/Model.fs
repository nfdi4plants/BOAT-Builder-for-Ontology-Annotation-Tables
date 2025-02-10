module BuildingBlock 

    open ARCtrl
    open ARCtrl.Helper
    open Shared
    open Fable.Core

    [<RequireQualifiedAccess>]
    type DropdownPage =
    | Main
    | More
    | IOTypes of CompositeHeaderDiscriminate

        member this.toString =
            match this with
            | Main -> "Main Page"
            | More -> "More"
            | IOTypes (t) -> t.ToString()

        member this.toTooltip =
            match this with
            | More -> "More"
            | IOTypes (t) -> $"Per table only one {t} is allowed. The value of this column must be a unique identifier."
            | _ -> ""

    type BuildingBlockUIState = {
        DropdownIsActive    : bool
        DropdownPage        : DropdownPage
    } with
        static member init() = {
            DropdownIsActive    = false
            DropdownPage        = DropdownPage.Main
        }
