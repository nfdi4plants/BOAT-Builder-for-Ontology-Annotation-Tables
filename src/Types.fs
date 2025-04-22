module Types

open ARCtrl

[<RequireQualifiedAccess>]
type CompositeHeaderDiscriminate =
| Component
| Characteristic
| Factor
| Parameter
| ProtocolType
| ProtocolDescription
| ProtocolUri
| ProtocolVersion
| ProtocolREF
| Performer
| Date
| Input
| Output
| Comment
| Freetext
with
    /// <summary>
    /// Returns true if the Building Block is a term column
    /// </summary>
    member this.IsTermColumn() =
        match this with
        | Component
        | Characteristic
        | Factor
        | Parameter
        | ProtocolType -> true
        | _ -> false
    member this.HasOA() =
        match this with
        | Component
        | Characteristic
        | Factor
        | Parameter -> true
        | _ -> false

    member this.HasIOType() =
        match this with
        | Input
        | Output -> true
        | _ -> false

    static member fromString(str: string) =
        match str with
        | "Component"           -> Component
        | "Characteristic"      -> Characteristic
        | "Factor"              -> Factor
        | "Parameter"           -> Parameter
        | "ProtocolType"        -> ProtocolType
        | "ProtocolDescription" -> ProtocolDescription
        | "ProtocolUri"         -> ProtocolUri
        | "ProtocolVersion"     -> ProtocolVersion
        | "ProtocolREF"         -> ProtocolREF
        | "Performer"           -> Performer
        | "Date"                -> Date
        | "Input"               -> Input
        | "Output"              -> Output
        | "Comment"             -> Comment
        | anyElse -> failwithf "BuildingBlock.HeaderCellType.fromString: '%s' is not a valid HeaderCellType" anyElse

type SearchComponent = 
    {
    Key: OntologyAnnotation 
    KeyType: CompositeHeaderDiscriminate 
    Body: CompositeCell 
    
    }

type Annotation = 
    {
    IsOpen: bool
    Search: SearchComponent
    Height: float
    HighlightKeys: string
    HighlightTerms: string
    } 

    static member init (key, body , highKey, highTerm, ?keyType, ?isOpen,  ?search, ?height ) = 
        let isOpen = defaultArg isOpen true
        let keyType = defaultArg keyType CompositeHeaderDiscriminate.Parameter
        let search = defaultArg search {
            Key= key
            KeyType= keyType
            Body= body
            }
        let height = defaultArg height 0.0
        {
            IsOpen= isOpen
            Search = search
            Height= height
            HighlightKeys = highKey
            HighlightTerms = highTerm

        }
    member this.ToggleOpen () = {this with IsOpen = not this.IsOpen}

type ModalInfo = {
    isActive: bool
    location: float * float
}

type DropdownModal = {
    modalState: ModalInfo
    setter: ModalInfo -> unit 
}


[<RequireQualifiedAccess>]

type Page =
    |Builder
    |Contact
    |Help

type UploadFileType =
  | Docx
  | PDF

type UploadedFile =
  | PDF of string
  | Docx of string
  | Unset
