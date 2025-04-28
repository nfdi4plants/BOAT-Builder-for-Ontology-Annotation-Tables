namespace Components

open Feliz
open Feliz.Router
open Feliz.DaisyUI

open Browser.Dom
open Fable.Core.JsInterop

type Contact =
    [<ReactComponent>]
    static member Main() = 
        Html.div [
            Daisy.button.button [
                prop.className "m-10"
                button.info
                button.lg
                prop.text "Report an issue"
                prop.onClick (fun _ -> window.``open``("https://github.com/nfdi4plants/BOAT-Builder-for-Ontology-ARC-Tables/issues", "_blank") |> ignore)
            ]
        ]