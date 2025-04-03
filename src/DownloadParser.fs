module DownloadParser

open ARCtrl
open Fable.Remoting.Client
open Types


let growth = ArcTable.init("Growth")

// create ontology annotation for "species"

// let oa_ofTable =
//     for a in 
let oa_species =
    OntologyAnnotation(
        "species", "NCIT", "NCIT:C45293" //Key
    )
// create ontology annotation for "chlamy"
let oa_chlamy =
    OntologyAnnotation(
        "Chlamydomonas reinhardtii", "NCBITaxon", "NCBITaxon_3055"
    )

let oa_time =
    OntologyAnnotation(
        "time", "EFO", "EFO:0000721"
    )

let oa_day =
    OntologyAnnotation(
        "day", "UO", "UO:0000033"
    )

// let oa_user (annoState: Annotation list) =
//     for a in annoState do
//         match a.Search.Key with
//         | oa-> OntologyAnnotation(oa.NameText, oa.TermSourceREF.Value, oa.TermAccessionNumber.Value)
//         | _ -> OntologyAnnotation("", "", "")

// This will create an input column with one row cell.
// In xlsx this will be exactly 1 column.
growth.AddColumn(
    CompositeHeader.Input IOType.Source,
    [|CompositeCell.createFreeText "Input1"|]
)

// this will create an Characteristic [species] column with one row cell.
// in xlsx this will be exactly 3 columns.
growth.AddColumn(
    CompositeHeader.Characteristic oa_species,
    [|CompositeCell.createTerm oa_chlamy|]
)

// this will create an Parameter [time] column with one row cell.
// in xlsx this will be exactly 4 columns.
growth.AddColumn(
    CompositeHeader.Parameter oa_time,
    [|CompositeCell.createUnitized("5", oa_day)|]
)

growth.AddColumn(
    CompositeHeader.Output IOType.Sample,
    [|CompositeCell.createFreeText "Output1"|]
)

let userTablewithColumns (annoState: Annotation list, fileName: string) =
    let userTable = ArcTable.init(fileName) // possible userinput to change table name
    for i in 0 .. annoState.Length - 1 do
        let revIndex = annoState.Length - 1 - i
        let a = annoState.[revIndex] 
        let header =
            match a.Search.KeyType with
            |CompositeHeaderDiscriminate.Component -> CompositeHeader.Component a.Search.Key
            |CompositeHeaderDiscriminate.Characteristic -> CompositeHeader.Characteristic a.Search.Key
            |CompositeHeaderDiscriminate.Parameter -> CompositeHeader.Parameter a.Search.Key
            |CompositeHeaderDiscriminate.Factor -> CompositeHeader.Factor a.Search.Key
            |_ -> CompositeHeader.OfHeaderString (a.Search.KeyType.ToString())
            
        userTable.AddColumn(
            header,
            [|a.Search.Body|]
        )
    userTable
///////////////


let testTemplate = 
  // create a template with the given file name
  Template.create(
      System.Guid.NewGuid(),
      growth,
      "<PLACEHOLDER>",
      lastUpdated = System.DateTime.UtcNow
  )

let userTemplate (fileName: string, annoState: Annotation list) =
    Template.create(
        System.Guid.NewGuid(),
        userTablewithColumns (annoState, fileName),
        fileName,
        lastUpdated = System.DateTime.UtcNow
        
    )

open FsSpreadsheet.Js
open ARCtrl.Json

let private download(filename, bytes:byte []) = bytes.SaveFileAs(filename)

let private downloadFromString(filename, content:string) =
    let bytes = System.Text.Encoding.UTF8.GetBytes(content)
    bytes.SaveFileAs(filename)

let downloadXlsxProm(fileName, annoState) =
  promise {
    let! bytes =
      Spreadsheet.Template.toFsWorkbook 
        (userTemplate(fileName, annoState))
        |> Xlsx.toXlsxBytes 
    download(fileName + "Table" + ".xlsx", bytes)
  }

open ARCtrl.Json

let downloadJsonProm(fileName, annoState) =
  promise {
    let jsonString = Template.toJsonString 0 (userTemplate(fileName, annoState))
    downloadFromString(fileName + "Table" + ".json", jsonString)
  }