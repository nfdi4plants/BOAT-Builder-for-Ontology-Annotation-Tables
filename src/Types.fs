module Types

open ARCtrl

type Protocoltext = {
    Content: string list
}


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
    } 

    static member init (key,body ,?keyType, ?isOpen,  ?search ) = 
        let isOpen = defaultArg isOpen true
        let keyType = defaultArg keyType CompositeHeaderDiscriminate.Parameter
        let search = defaultArg search {
            Key= key
            KeyType= keyType
            Body= body
            }

        {
            IsOpen= isOpen
            Search = search

        }
    member this.ToggleOpen () = {this with IsOpen = not this.IsOpen}

type ModalInfo = {
    isActive: bool
    location: int * int
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
//   | PDF

type UploadedFile =
//   | PDF of string
  | Docx of string
  | Unset
