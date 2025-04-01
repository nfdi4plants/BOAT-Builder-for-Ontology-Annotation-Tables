module DownloadParser

open ARCtrl
open Fable.Remoting.Client



let growth = ArcTable.init("Growth")
let userTable = ArcTable.init("myTable") // possible userinput to change table name

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
///////////////
let prevFileName = "<PLACEHOLDER>"

let template = 
  // create a template with the given file name
  Template.create(
      System.Guid.NewGuid(),
      growth,
      prevFileName,
      lastUpdated = System.DateTime.UtcNow
  )

open FsSpreadsheet.Js
open ARCtrl.Json

let private download(filename, bytes:byte []) = bytes.SaveFileAs(filename)

let private downloadFromString(filename, content:string) =
    let bytes = System.Text.Encoding.UTF8.GetBytes(content)
    bytes.SaveFileAs(filename)

let downloadXlsxProm() =
  promise {
    let! bytes =
      Spreadsheet.Template.toFsWorkbook 
        template
        |> Xlsx.toXlsxBytes 
    download("ExamplePaper" + ".xlsx", bytes)
  }

open ARCtrl.Json

let downloadJsonProm() =
  promise {
    let jsonString = Template.toJsonString 0 template
    downloadFromString("ExamplePaper" + ".json", jsonString)
  }