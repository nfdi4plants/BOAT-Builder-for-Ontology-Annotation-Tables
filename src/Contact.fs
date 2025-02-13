namespace Components

open Feliz
open Feliz.Router

type Contact =
    [<ReactComponent>]
    static member Main() = Html.div "Contact"