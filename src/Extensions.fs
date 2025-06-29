[<AutoOpen>]
module Extensions

open System
open Fable.Core
open Fable.Core.JsInterop
open Types
open ARCtrl.Json
open Thoth.Json.Core

let log o = Browser.Dom.console.log o

module Jsonkeys = 
    [<Literal>]
    let Key = "key"
    [<Literal>]
    let Body = "Body"
    [<Literal>]
    let IsOpen = "isOpen"
    [<Literal>]
    let Height = "height"
    [<Literal>]
    let HighlightKeys = "highlightKeys"
    [<Literal>]
    let HighlightTerms = "highlightTerms"
    [<Literal>]
    let HighlightValues = "highlightValues"
 
    

let encoderAnno (anno: Annotation) = //encodes annotation to json         
    [
        Encode.tryInclude Jsonkeys.Key OntologyAnnotation.encoder (Some anno.Search.Key)
        Encode.tryInclude Jsonkeys.Body CompositeCell.encoder (Some anno.Search.Body)
        Encode.tryInclude Jsonkeys.IsOpen Encode.bool (Some anno.IsOpen)
        Encode.tryInclude Jsonkeys.Height Encode.float (Some anno.Height)
        Encode.tryInclude Jsonkeys.HighlightKeys Encode.string (Some anno.HighlightKeys)
        Encode.tryInclude Jsonkeys.HighlightTerms Encode.string (Some anno.HighlightTerms)
        Encode.tryInclude Jsonkeys.HighlightValues Encode.string (Some anno.HighlightValues)
    ]
    |> Encode.choose
    |> Encode.object



let decoderAnno : Decoder<Annotation list> = //decodes json to annotation  
    Decode.list (
        Decode.object (fun get ->
            {
            IsOpen = get.Required.Field Jsonkeys.IsOpen Decode.bool
            Search = {
                Key = get.Required.Field  Jsonkeys.Key OntologyAnnotation.decoder
                KeyType =  CompositeHeaderDiscriminate.Parameter
                Body = get.Required.Field  Jsonkeys.Body CompositeCell.decoder
                }
            Height = get.Required.Field Jsonkeys.Height Decode.float
            HighlightKeys = get.Required.Field Jsonkeys.HighlightKeys Decode.string
            HighlightTerms = get.Required.Field Jsonkeys.HighlightTerms Decode.string
            HighlightValues = get.Required.Field Jsonkeys.HighlightValues Decode.string
            }
        )
    )
    
type URL = 
  abstract member createObjectURL: Browser.Types.File -> string

[<Emit("URL")>]
let URL: URL = jsNative

[<RequireQualifiedAccess>]
module StaticFile =

    /// Function that imports a static file by it's relative path.
    let inline import (path: string) : string = importDefault<string> path

/// Stylesheet API
/// let private stylesheet = Stylesheet.load "./fancy.module.css"
/// stylesheet.["fancy-class-name"] which returns a string
module Stylesheet =

    type IStylesheet =
        [<Emit "$0[$1]">]
        abstract Item : className:string -> string

    /// Loads a CSS module and makes the classes within available
    let inline load (path: string) = importDefault<IStylesheet> path