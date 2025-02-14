namespace Components

open Feliz
open Feliz.DaisyUI

type Footer =
    static member Main =
        Daisy.footer [
            prop.className "text-base-content footer-center bg-base-300 p-4 select-none"
            prop.children [
                Html.aside [
                    prop.className "flex justify-center"
                    prop.children [
                        Html.span [
                            prop.text "This is a footer. By"
                        ]
                        Html.a [
                            prop.text "ndfdi4plants"
                            prop.href "https://www.nfdi4plants.de/"
                            prop.target.blank 
                            prop.className "underline text-blue-400"
                        ]
                    ]
                ]
            ]
        ]