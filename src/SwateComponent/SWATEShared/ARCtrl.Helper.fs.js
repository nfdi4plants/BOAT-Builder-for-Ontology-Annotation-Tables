import { Record, Union } from "../fable_modules/fable-library-js.4.24.0/Types.js";
import { bool_type, record_type, list_type, union_type } from "../fable_modules/fable-library-js.4.24.0/Reflection.js";
import { Template_$reflection } from "../fable_modules/ARCtrl.Core.2.0.0-swate/Template.fs.js";
import { ArcAssay_$reflection, ArcStudy_$reflection, ArcInvestigation_$reflection } from "../fable_modules/ARCtrl.Core.2.0.0-swate/ArcTypes.fs.js";
import { ArcTables } from "../fable_modules/ARCtrl.Core.2.0.0-swate/Table/ArcTables.fs.js";
import { split, join, replace, printf, toFail } from "../fable_modules/fable-library-js.4.24.0/String.js";
import { contains, toArray, cons, empty } from "../fable_modules/fable-library-js.4.24.0/List.js";
import { defaultOf, numberHash, arrayHash, equalArrays, equals, disposeSafe, getEnumerator } from "../fable_modules/fable-library-js.4.24.0/Util.js";
import { toList, tryFindIndex } from "../fable_modules/fable-library-js.4.24.0/Seq.js";
import { defaultArg, value as value_4 } from "../fable_modules/fable-library-js.4.24.0/Option.js";
import { CompositeColumn } from "../fable_modules/ARCtrl.Core.2.0.0-swate/Table/CompositeColumn.fs.js";
import { Dictionary } from "../fable_modules/fable-library-js.4.24.0/MutableMap.js";
import { rangeDouble } from "../fable_modules/fable-library-js.4.24.0/Range.js";
import { max, min } from "../fable_modules/fable-library-js.4.24.0/Double.js";
import { getItemFromDict } from "../fable_modules/fable-library-js.4.24.0/MapUtil.js";
import { DataMap__get_DataContexts } from "../fable_modules/ARCtrl.Core.2.0.0-swate/DataMap.fs.js";
import { CompositeCell_$reflection, CompositeCell } from "../fable_modules/ARCtrl.Core.2.0.0-swate/Table/CompositeCell.fs.js";
import { DataContext__set_ObjectType_279AAFF2, DataContext__set_Unit_279AAFF2, DataContext__set_Explication_279AAFF2, DataContext__set_GeneratedBy_6DFDD678, DataContext__set_Description_6DFDD678, DataContext__get_ObjectType, DataContext__get_Unit, DataContext__get_Explication, DataContext__get_GeneratedBy, DataContext__get_Description } from "../fable_modules/ARCtrl.Core.2.0.0-swate/DataContext.fs.js";
import { OntologyAnnotation_$reflection, OntologyAnnotation } from "../fable_modules/ARCtrl.Core.2.0.0-swate/OntologyAnnotation.fs.js";
import { CompositeHeader, IOType } from "../fable_modules/ARCtrl.Core.2.0.0-swate/Table/CompositeHeader.fs.js";
import { ObjectType, Unit, Explication } from "./StaticTermCollection.fs.js";
import { DataFile } from "../fable_modules/ARCtrl.Core.2.0.0-swate/DataFile.fs.js";
import { Unchecked_fillMissingCells, Unchecked_setCellAt, SanityChecks_validateColumn } from "../fable_modules/ARCtrl.Core.2.0.0-swate/Table/ArcTableAux.fs.js";
import { Array_groupBy } from "../fable_modules/fable-library-js.4.24.0/Seq2.js";
import { equalsWith, map, item } from "../fable_modules/fable-library-js.4.24.0/Array.js";
import { Data } from "../fable_modules/ARCtrl.Core.2.0.0-swate/Data.fs.js";

export class ARCtrlHelper_ArcFilesDiscriminate extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Assay", "Study", "Investigation", "Template"];
    }
}

export function ARCtrlHelper_ArcFilesDiscriminate_$reflection() {
    return union_type("Shared.ARCtrlHelper.ArcFilesDiscriminate", [], ARCtrlHelper_ArcFilesDiscriminate, () => [[], [], [], []]);
}

export class ARCtrlHelper_ArcFiles extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Template", "Investigation", "Study", "Assay"];
    }
}

export function ARCtrlHelper_ArcFiles_$reflection() {
    return union_type("Shared.ARCtrlHelper.ArcFiles", [], ARCtrlHelper_ArcFiles, () => [[["Item", Template_$reflection()]], [["Item", ArcInvestigation_$reflection()]], [["Item1", ArcStudy_$reflection()], ["Item2", list_type(ArcAssay_$reflection())]], [["Item", ArcAssay_$reflection()]]]);
}

export function ARCtrlHelper_ArcFiles__HasTableAt_Z524259A4(this$, index) {
    switch (this$.tag) {
        case 1: {
            const i = this$.fields[0];
            return false;
        }
        case 2: {
            const s = this$.fields[0];
            return s.TableCount <= index;
        }
        case 3: {
            const a = this$.fields[0];
            return a.TableCount <= index;
        }
        default:
            return index === 0;
    }
}

export function ARCtrlHelper_ArcFiles__HasDataMap(this$) {
    switch (this$.tag) {
        case 1: {
            const i = this$.fields[0];
            return false;
        }
        case 2: {
            const s = this$.fields[0];
            return s.DataMap != null;
        }
        case 3: {
            const a = this$.fields[0];
            return a.DataMap != null;
        }
        default:
            return false;
    }
}

export function ARCtrlHelper_ArcFiles__Tables(this$) {
    switch (this$.tag) {
        case 1:
            return new ArcTables([]);
        case 2: {
            const s = this$.fields[0];
            return s;
        }
        case 3: {
            const a = this$.fields[0];
            return a;
        }
        default: {
            const t = this$.fields[0];
            return new ArcTables([t.Table]);
        }
    }
}

export class ARCtrlHelper_JsonExportFormat extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["ARCtrl", "ARCtrlCompressed", "ISA", "ROCrate"];
    }
}

export function ARCtrlHelper_JsonExportFormat_$reflection() {
    return union_type("Shared.ARCtrlHelper.JsonExportFormat", [], ARCtrlHelper_JsonExportFormat, () => [[], [], [], []]);
}

export function ARCtrlHelper_JsonExportFormat_fromString_Z721C83C5(str) {
    const matchValue = str.toLocaleLowerCase();
    switch (matchValue) {
        case "arctrl":
            return new ARCtrlHelper_JsonExportFormat(0, []);
        case "arctrlcompressed":
            return new ARCtrlHelper_JsonExportFormat(1, []);
        case "isa":
            return new ARCtrlHelper_JsonExportFormat(2, []);
        case "rocrate":
            return new ARCtrlHelper_JsonExportFormat(3, []);
        default:
            return toFail(printf("Unknown JSON export format: %s"))(str);
    }
}

export function ARCtrlHelper_JsonExportFormat__get_AsStringRdbl(this$) {
    switch (this$.tag) {
        case 1:
            return "ARCtrl Compressed";
        case 2:
            return "ISA";
        case 3:
            return "RO-Crate Metadata";
        default:
            return "ARCtrl";
    }
}

/**
 * This functions returns a **copy** of `toJoinTable` without any column already in `activeTable`.
 */
export function Table_distinctByHeader(activeTable, toJoinTable) {
    let columnsToRemove = empty();
    const tablecopy = toJoinTable.Copy();
    let enumerator = getEnumerator(activeTable.Headers);
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            const header = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            const containsAtIndex = tryFindIndex((h) => equals(h, header), tablecopy.Headers);
            if (containsAtIndex != null) {
                columnsToRemove = cons(value_4(containsAtIndex), columnsToRemove);
            }
        }
    }
    finally {
        disposeSafe(enumerator);
    }
    tablecopy.RemoveColumns(toArray(columnsToRemove));
    return tablecopy;
}

/**
 * This function is meant to prepare a table for joining with another table.
 * 
 * It removes columns that are already present in the active table.
 * It removes all values from the new table.
 * It also fills new Input/Output columns with the input/output values of the active table.
 * 
 * The output of this function can be used with the SpreadsheetInterface.JoinTable Message.
 */
export function Table_selectiveTablePrepare(activeTable, toJoinTable) {
    let columnsToRemove = empty();
    const tablecopy = toJoinTable.Copy();
    let enumerator = getEnumerator(activeTable.Headers);
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            const header = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            const containsAtIndex = tryFindIndex((h) => equals(h, header), tablecopy.Headers);
            if (containsAtIndex != null) {
                columnsToRemove = cons(value_4(containsAtIndex), columnsToRemove);
            }
        }
    }
    finally {
        disposeSafe(enumerator);
    }
    tablecopy.RemoveColumns(toArray(columnsToRemove));
    tablecopy.IteriColumns((i, c0) => {
        const c1 = new CompositeColumn(c0.Header, []);
        let c2;
        if (c1.Header.isInput) {
            const matchValue = activeTable.TryGetInputColumn();
            if (matchValue != null) {
                const ic = matchValue;
                c2 = (new CompositeColumn(c1.Header, ic.Cells));
            }
            else {
                c2 = c1;
            }
        }
        else if (c1.Header.isOutput) {
            const matchValue_1 = activeTable.TryGetOutputColumn();
            if (matchValue_1 != null) {
                const oc = matchValue_1;
                c2 = (new CompositeColumn(c1.Header, oc.Cells));
            }
            else {
                c2 = c1;
            }
        }
        else {
            c2 = c1;
        }
        tablecopy.UpdateColumn(i, c2.Header, c2.Cells);
    });
    return tablecopy;
}

export function Helper_doptstr(o) {
    return defaultArg(o, "");
}

export function Helper_arrayMoveColumn(currentColumnIndex, newColumnIndex, arr) {
    const ele = arr[currentColumnIndex];
    arr.splice(currentColumnIndex, 1);
    arr.splice(newColumnIndex, 0, ele);
}

export function Helper_dictMoveColumn(currentColumnIndex, newColumnIndex, table) {
    const backupTable = new Dictionary(table, {
        Equals: equalArrays,
        GetHashCode: arrayHash,
    });
    const range = toList(rangeDouble(min(currentColumnIndex, newColumnIndex), 1, max(currentColumnIndex, newColumnIndex)));
    let enumerator = getEnumerator(backupTable.keys());
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            const forLoopVar = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            const rowIndex = forLoopVar[1] | 0;
            const columnIndex = forLoopVar[0] | 0;
            const value = getItemFromDict(backupTable, [columnIndex, rowIndex]);
            let newColumnIndex_1;
            if (columnIndex === currentColumnIndex) {
                newColumnIndex_1 = newColumnIndex;
            }
            else if (contains(columnIndex, range, {
                Equals: (x_1, y_1) => (x_1 === y_1),
                GetHashCode: numberHash,
            })) {
                const modifier = ((currentColumnIndex < newColumnIndex) ? -1 : 1) | 0;
                const moveTo = (modifier + columnIndex) | 0;
                newColumnIndex_1 = moveTo;
            }
            else {
                newColumnIndex_1 = (0 + columnIndex);
            }
            const updatedKey = [newColumnIndex_1, rowIndex];
            table.set(updatedKey, value);
        }
    }
    finally {
        disposeSafe(enumerator);
    }
}

export class CompositeHeaderDiscriminate extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Component", "Characteristic", "Factor", "Parameter", "ProtocolType", "ProtocolDescription", "ProtocolUri", "ProtocolVersion", "ProtocolREF", "Performer", "Date", "Input", "Output", "Comment", "Freetext"];
    }
}

export function CompositeHeaderDiscriminate_$reflection() {
    return union_type("Shared.CompositeHeaderDiscriminate", [], CompositeHeaderDiscriminate, () => [[], [], [], [], [], [], [], [], [], [], [], [], [], [], []]);
}

/**
 * Returns true if the Building Block is a term column
 */
export function CompositeHeaderDiscriminate__IsTermColumn(this$) {
    switch (this$.tag) {
        case 0:
        case 1:
        case 2:
        case 3:
        case 4:
            return true;
        default:
            return false;
    }
}

export function CompositeHeaderDiscriminate__HasOA(this$) {
    switch (this$.tag) {
        case 0:
        case 1:
        case 2:
        case 3:
            return true;
        default:
            return false;
    }
}

export function CompositeHeaderDiscriminate__HasIOType(this$) {
    switch (this$.tag) {
        case 11:
        case 12:
            return true;
        default:
            return false;
    }
}

export function CompositeHeaderDiscriminate_fromString_Z721C83C5(str) {
    switch (str) {
        case "Component":
            return new CompositeHeaderDiscriminate(0, []);
        case "Characteristic":
            return new CompositeHeaderDiscriminate(1, []);
        case "Factor":
            return new CompositeHeaderDiscriminate(2, []);
        case "Parameter":
            return new CompositeHeaderDiscriminate(3, []);
        case "ProtocolType":
            return new CompositeHeaderDiscriminate(4, []);
        case "ProtocolDescription":
            return new CompositeHeaderDiscriminate(5, []);
        case "ProtocolUri":
            return new CompositeHeaderDiscriminate(6, []);
        case "ProtocolVersion":
            return new CompositeHeaderDiscriminate(7, []);
        case "ProtocolREF":
            return new CompositeHeaderDiscriminate(8, []);
        case "Performer":
            return new CompositeHeaderDiscriminate(9, []);
        case "Date":
            return new CompositeHeaderDiscriminate(10, []);
        case "Input":
            return new CompositeHeaderDiscriminate(11, []);
        case "Output":
            return new CompositeHeaderDiscriminate(12, []);
        case "Comment":
            return new CompositeHeaderDiscriminate(13, []);
        default: {
            const anyElse = str;
            return toFail(printf("BuildingBlock.HeaderCellType.fromString: \'%s\' is not a valid HeaderCellType"))(anyElse);
        }
    }
}

export class CompositeCellDiscriminate extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Term", "Unitized", "Text", "Data"];
    }
}

export function CompositeCellDiscriminate_$reflection() {
    return union_type("Shared.CompositeCellDiscriminate", [], CompositeCellDiscriminate, () => [[], [], [], []]);
}

export function ARCtrl_DataMap__DataMap_GetCell_Z37302880(this$, columnIndex, rowIndex) {
    const r = DataMap__get_DataContexts(this$)[rowIndex];
    switch (columnIndex) {
        case 0:
            return CompositeCell.createData(r);
        case 1:
            return new CompositeCell(1, [Helper_doptstr(DataContext__get_Description(r))]);
        case 2:
            return new CompositeCell(1, [Helper_doptstr(DataContext__get_GeneratedBy(r))]);
        case 3:
            return new CompositeCell(0, [defaultArg(DataContext__get_Explication(r), new OntologyAnnotation())]);
        case 4:
            return new CompositeCell(0, [defaultArg(DataContext__get_Unit(r), new OntologyAnnotation())]);
        case 5:
            return new CompositeCell(0, [defaultArg(DataContext__get_ObjectType(r), new OntologyAnnotation())]);
        default: {
            const i = columnIndex | 0;
            return toFail(printf("Invalid column index for DataMap: %i"))(i);
        }
    }
}

export function ARCtrl_DataMap__DataMap_SetCell_5E511882(this$, columnIndex, rowIndex, cell) {
    const r = DataMap__get_DataContexts(this$)[rowIndex];
    switch (columnIndex) {
        case 0: {
            const nd = cell.AsData;
            r.FilePath = nd.FilePath;
            r.Selector = nd.Selector;
            r.Format = nd.Format;
            r.SelectorFormat = nd.SelectorFormat;
            break;
        }
        case 1: {
            DataContext__set_Description_6DFDD678(r, cell.AsFreeText);
            break;
        }
        case 2: {
            DataContext__set_GeneratedBy_6DFDD678(r, cell.AsFreeText);
            break;
        }
        case 3: {
            DataContext__set_Explication_279AAFF2(r, cell.AsTerm);
            break;
        }
        case 4: {
            DataContext__set_Unit_279AAFF2(r, cell.AsTerm);
            break;
        }
        case 5: {
            DataContext__set_ObjectType_279AAFF2(r, cell.AsTerm);
            break;
        }
        default: {
            const i = columnIndex | 0;
            toFail(printf("Invalid column index for DataMap: %i"))(i);
        }
    }
}

export function ARCtrl_DataMap__DataMap_getHeader_Static_Z524259A4(columnIndex) {
    switch (columnIndex) {
        case 0:
            return new CompositeHeader(11, [new IOType(2, [])]);
        case 1:
            return new CompositeHeader(13, ["Description"]);
        case 2:
            return new CompositeHeader(13, ["Generated By"]);
        case 3:
            return new CompositeHeader(3, [Explication]);
        case 4:
            return new CompositeHeader(3, [Unit]);
        case 5:
            return new CompositeHeader(3, [ObjectType]);
        default: {
            const i = columnIndex | 0;
            return toFail(printf("Invalid column index for DataMap: %i"))(i);
        }
    }
}

export function ARCtrl_DataMap__DataMap_GetHeader_Z524259A4(this$, columnIndex) {
    return ARCtrl_DataMap__DataMap_getHeader_Static_Z524259A4(columnIndex);
}

export function ARCtrl_DataMap__DataMap_get_ColumnCount_Static() {
    return 6;
}

export function ARCtrl_DataMap__DataMap_get_ColumnCount(this$) {
    return ARCtrl_DataMap__DataMap_get_ColumnCount_Static();
}

export function ARCtrl_DataMap__DataMap_get_RowCount(this$) {
    return DataMap__get_DataContexts(this$).length;
}

export function ARCtrl_DataFile__DataFile_ToStringRdb(this$) {
    switch (this$.tag) {
        case 2:
            return "Image File";
        case 0:
            return "Raw Data File";
        default:
            return "Derived Data File";
    }
}

export function ARCtrl_DataFile__DataFile_tryFromString_Static_Z721C83C5(str) {
    const matchValue = str.toLocaleLowerCase();
    switch (matchValue) {
        case "derived data file":
        case "deriveddatafile":
            return new DataFile(1, []);
        case "image file":
        case "imagefile":
            return new DataFile(2, []);
        case "raw data file":
        case "rawdatafile":
            return new DataFile(0, []);
        default:
            return undefined;
    }
}

export function ARCtrl_DataFile__DataFile_fromString_Static_Z721C83C5(str) {
    const matchValue = ARCtrl_DataFile__DataFile_tryFromString_Static_Z721C83C5(str);
    if (matchValue == null) {
        return toFail(printf("Unknown DataFile: %s"))(str);
    }
    else {
        const r = matchValue;
        return r;
    }
}

export function ARCtrl_OntologyAnnotation__OntologyAnnotation_empty_Static() {
    return OntologyAnnotation.create();
}

export function ARCtrl_OntologyAnnotation__OntologyAnnotation_fromTerm_Static_Z5C13A4B9(term) {
    return new OntologyAnnotation(term.Name, term.FK_Ontology, term.Accession);
}

export function ARCtrl_ArcTable__ArcTable_SetCellAt_5E511882(this$, columnIndex, rowIndex, cell) {
    SanityChecks_validateColumn(CompositeColumn.create(this$.Headers[columnIndex], [cell]));
    Unchecked_setCellAt(columnIndex, rowIndex, cell, this$.Values);
    Unchecked_fillMissingCells(this$.Headers, this$.Values);
}

export function ARCtrl_ArcTable__ArcTable_SetCellsAt_3236D929(this$, cells) {
    const columns = Array_groupBy((tupledArg) => {
        const index = tupledArg[0];
        const cell = tupledArg[1];
        return index[0] | 0;
    }, cells, {
        Equals: (x, y) => (x === y),
        GetHashCode: numberHash,
    });
    for (let idx = 0; idx <= (columns.length - 1); idx++) {
        const forLoopVar = item(idx, columns);
        const items = forLoopVar[1];
        const columnIndex = forLoopVar[0] | 0;
        SanityChecks_validateColumn(CompositeColumn.create(this$.Headers[columnIndex], map((tuple) => tuple[1], items)));
    }
    for (let idx_1 = 0; idx_1 <= (cells.length - 1); idx_1++) {
        const forLoopVar_1 = item(idx_1, cells);
        const index_1 = forLoopVar_1[0];
        const cell_1 = forLoopVar_1[1];
        Unchecked_setCellAt(index_1[0], index_1[1], cell_1, this$.Values);
    }
    Unchecked_fillMissingCells(this$.Headers, this$.Values);
}

export function ARCtrl_ArcTable__ArcTable_MoveColumn_Z37302880(this$, currentIndex, nextIndex) {
    const updateHeaders = Helper_arrayMoveColumn(currentIndex, nextIndex, this$.Headers);
    const updateBody = Helper_dictMoveColumn(currentIndex, nextIndex, this$.Values);
}

export function ARCtrl_Template__Template_get_FileName(this$) {
    return replace(this$.Name, " ", "_") + ".xlsx";
}

export function ARCtrl_CompositeHeader__CompositeHeader_UpdateWithOA_ZDED3A0F(this$, oa) {
    switch (this$.tag) {
        case 0:
            return new CompositeHeader(0, [oa]);
        case 3:
            return new CompositeHeader(3, [oa]);
        case 1:
            return new CompositeHeader(1, [oa]);
        case 2:
            return new CompositeHeader(2, [oa]);
        default:
            return toFail(printf("Cannot update OntologyAnnotation on CompositeHeader without OntologyAnnotation: \'%A\'"))(this$);
    }
}

export function ARCtrl_CompositeHeader__CompositeHeader_get_ParameterEmpty_Static() {
    return new CompositeHeader(3, [ARCtrl_OntologyAnnotation__OntologyAnnotation_empty_Static()]);
}

export function ARCtrl_CompositeHeader__CompositeHeader_get_CharacteristicEmpty_Static() {
    return new CompositeHeader(1, [ARCtrl_OntologyAnnotation__OntologyAnnotation_empty_Static()]);
}

export function ARCtrl_CompositeHeader__CompositeHeader_get_ComponentEmpty_Static() {
    return new CompositeHeader(0, [ARCtrl_OntologyAnnotation__OntologyAnnotation_empty_Static()]);
}

export function ARCtrl_CompositeHeader__CompositeHeader_get_FactorEmpty_Static() {
    return new CompositeHeader(2, [ARCtrl_OntologyAnnotation__OntologyAnnotation_empty_Static()]);
}

export function ARCtrl_CompositeHeader__CompositeHeader_get_InputEmpty_Static() {
    return new CompositeHeader(11, [new IOType(4, [""])]);
}

export function ARCtrl_CompositeHeader__CompositeHeader_get_OutputEmpty_Static() {
    return new CompositeHeader(12, [new IOType(4, [""])]);
}

/**
 * Keep the outer `CompositeHeader` information (e.g.: Parameter, Factor, Input, Output..) and update the inner "of" value with the value from `other.`
 * This will only run successfully if the inner values are of the same type
 */
export function ARCtrl_CompositeHeader__CompositeHeader_UpdateDeepWith_6CAF647B(this$, other) {
    let h2, h1, h2_1, h1_1;
    if ((h2 = other, (h1 = this$, this$.IsIOType && other.IsIOType))) {
        const h2_2 = other;
        const h1_2 = this$;
        const io1 = value_4(h2_2.TryIOType());
        switch (h1_2.tag) {
            case 11:
                return new CompositeHeader(11, [io1]);
            case 12:
                return new CompositeHeader(12, [io1]);
            default:
                throw new Error("Error 1 in UpdateSurfaceTo. This should never hit.");
        }
    }
    else if ((h2_1 = other, (h1_1 = this$, ((this$.IsTermColumn && other.IsTermColumn) && !this$.IsFeaturedColumn) && !other.IsFeaturedColumn))) {
        const h2_3 = other;
        const h1_3 = this$;
        const oa1 = h2_3.ToTerm();
        return ARCtrl_CompositeHeader__CompositeHeader_UpdateWithOA_ZDED3A0F(h1_3, oa1);
    }
    else {
        return this$;
    }
}

export function ARCtrl_CompositeHeader__CompositeHeader_TryOA(this$) {
    switch (this$.tag) {
        case 0: {
            const oa = this$.fields[0];
            return oa;
        }
        case 3: {
            const oa_1 = this$.fields[0];
            return oa_1;
        }
        case 1: {
            const oa_2 = this$.fields[0];
            return oa_2;
        }
        case 2: {
            const oa_3 = this$.fields[0];
            return oa_3;
        }
        default:
            return undefined;
    }
}

export function ARCtrl_CompositeHeader__CompositeHeader_get_AsDiscriminate(this$) {
    switch (this$.tag) {
        case 1:
            return new CompositeHeaderDiscriminate(1, []);
        case 2:
            return new CompositeHeaderDiscriminate(2, []);
        case 3:
            return new CompositeHeaderDiscriminate(3, []);
        case 4:
            return new CompositeHeaderDiscriminate(4, []);
        case 5:
            return new CompositeHeaderDiscriminate(5, []);
        case 6:
            return new CompositeHeaderDiscriminate(6, []);
        case 7:
            return new CompositeHeaderDiscriminate(7, []);
        case 8:
            return new CompositeHeaderDiscriminate(8, []);
        case 9:
            return new CompositeHeaderDiscriminate(9, []);
        case 10:
            return new CompositeHeaderDiscriminate(10, []);
        case 11:
            return new CompositeHeaderDiscriminate(11, []);
        case 12:
            return new CompositeHeaderDiscriminate(12, []);
        case 14:
            return new CompositeHeaderDiscriminate(13, []);
        case 13:
            return new CompositeHeaderDiscriminate(14, []);
        default:
            return new CompositeHeaderDiscriminate(0, []);
    }
}

/**
 * This is an override of an existing ARCtrl version which does not return what i want 😤
 */
export function ARCtrl_CompositeCell__CompositeCell_GetContentSwate(this$) {
    switch (this$.tag) {
        case 0: {
            const oa = this$.fields[0];
            return [oa.NameText, defaultArg(oa.TermSourceREF, ""), defaultArg(oa.TermAccessionNumber, "")];
        }
        case 2: {
            const v = this$.fields[0];
            const oa_1 = this$.fields[1];
            return [v, oa_1.NameText, defaultArg(oa_1.TermSourceREF, ""), defaultArg(oa_1.TermAccessionNumber, "")];
        }
        case 3: {
            const d = this$.fields[0];
            return [defaultArg(d.FilePath, ""), defaultArg(d.Selector, ""), defaultArg(d.Format, ""), defaultArg(d.SelectorFormat, "")];
        }
        default: {
            const s = this$.fields[0];
            return [s];
        }
    }
}

export function ARCtrl_CompositeCell__CompositeCell_ToDataCell(this$) {
    switch (this$.tag) {
        case 1: {
            const txt = this$.fields[0];
            return CompositeCell.createDataFromString(txt);
        }
        case 0: {
            const term = this$.fields[0];
            return CompositeCell.createDataFromString(term.NameText);
        }
        case 3:
            return this$;
        default: {
            const unit = this$.fields[1];
            return CompositeCell.createDataFromString(unit.NameText);
        }
    }
}

/**
 * 
 */
export function ARCtrl_CompositeCell__CompositeCell_fromContentValid_Static_Z1E83A578(content, header) {
    let freetext, tsr, tan, name, selectorFormat, selector, path, format, value, tsr_1, tan_1, name_1;
    if (header != null) {
        const header_1 = value_4(header);
        if (!equalsWith((x, y) => (x === y), content, defaultOf()) && (content.length === 1)) {
            if ((freetext = item(0, content), header_1.IsSingleColumn)) {
                const freetext_1 = item(0, content);
                return CompositeCell.createFreeText(freetext_1);
            }
            else {
                const freetext_2 = item(0, content);
                return ARCtrl_CompositeCell__CompositeCell_ConvertToValidCell_6CAF647B(CompositeCell.createFreeText(freetext_2), header_1);
            }
        }
        else if (!equalsWith((x_1, y_1) => (x_1 === y_1), content, defaultOf()) && (content.length === 3)) {
            if ((tsr = item(1, content), (tan = item(2, content), (name = item(0, content), header_1.IsTermColumn)))) {
                const tsr_2 = item(1, content);
                const tan_2 = item(2, content);
                const name_2 = item(0, content);
                return CompositeCell.createTermFromString(name_2, tsr_2, tan_2);
            }
            else {
                const tsr_3 = item(1, content);
                const tan_3 = item(2, content);
                const name_3 = item(0, content);
                return ARCtrl_CompositeCell__CompositeCell_ConvertToValidCell_6CAF647B(CompositeCell.createTermFromString(name_3, tsr_3, tan_3), header_1);
            }
        }
        else if (!equalsWith((x_2, y_2) => (x_2 === y_2), content, defaultOf()) && (content.length === 4)) {
            if ((selectorFormat = item(3, content), (selector = item(1, content), (path = item(0, content), (format = item(2, content), header_1.IsDataColumn))))) {
                const selectorFormat_1 = item(3, content);
                const selector_1 = item(1, content);
                const path_1 = item(0, content);
                const format_1 = item(2, content);
                const data = Data.empty;
                data.FilePath = path_1;
                data.Selector = selector_1;
                data.Format = format_1;
                data.SelectorFormat = selectorFormat_1;
                return CompositeCell.createData(data);
            }
            else if ((value = item(0, content), (tsr_1 = item(2, content), (tan_1 = item(3, content), (name_1 = item(1, content), header_1.IsTermColumn))))) {
                const value_1 = item(0, content);
                const tsr_4 = item(2, content);
                const tan_4 = item(3, content);
                const name_4 = item(1, content);
                return CompositeCell.createUnitizedFromString(value_1, name_4, tsr_4, tan_4);
            }
            else {
                const value_2 = item(0, content);
                const tsr_5 = item(2, content);
                const tan_5 = item(3, content);
                const name_5 = item(1, content);
                return ARCtrl_CompositeCell__CompositeCell_ConvertToValidCell_6CAF647B(CompositeCell.createUnitizedFromString(value_2, name_5, tsr_5, tan_5), header_1);
            }
        }
        else {
            const anyElse = content;
            return toFail(printf("Invalid content for header: %A"))(anyElse);
        }
    }
    else if (!equalsWith((x_3, y_3) => (x_3 === y_3), content, defaultOf()) && (content.length === 1)) {
        const freetext_3 = item(0, content);
        return CompositeCell.createFreeText(freetext_3);
    }
    else if (!equalsWith((x_4, y_4) => (x_4 === y_4), content, defaultOf()) && (content.length === 3)) {
        const tsr_6 = item(1, content);
        const tan_6 = item(2, content);
        const name_6 = item(0, content);
        return CompositeCell.createTermFromString(name_6, tsr_6, tan_6);
    }
    else if (!equalsWith((x_5, y_5) => (x_5 === y_5), content, defaultOf()) && (content.length === 4)) {
        const value_3 = item(0, content);
        const tsr_7 = item(2, content);
        const tan_7 = item(3, content);
        const name_7 = item(1, content);
        return CompositeCell.createUnitizedFromString(value_3, name_7, tsr_7, tan_7);
    }
    else {
        const anyElse_1 = content;
        return toFail(printf("Invalid content to parse to CompositeCell: %A"))(anyElse_1);
    }
}

export function ARCtrl_CompositeCell__CompositeCell_ToTabStr(this$) {
    return join("\t", ARCtrl_CompositeCell__CompositeCell_GetContentSwate(this$));
}

export function ARCtrl_CompositeCell__CompositeCell_fromTabStr_Static(str, header) {
    const content = split(str, ["\t"], undefined, 2);
    return ARCtrl_CompositeCell__CompositeCell_fromContentValid_Static_Z1E83A578(content, header);
}

export function ARCtrl_CompositeCell__CompositeCell_ToTabTxt_Static_C98E589(cells) {
    return join("\n", map(ARCtrl_CompositeCell__CompositeCell_ToTabStr, cells));
}

export function ARCtrl_CompositeCell__CompositeCell_fromTabTxt_Static(tabTxt, header) {
    const lines = split(tabTxt, ["\n"], undefined, 0);
    const cells = map((line) => ARCtrl_CompositeCell__CompositeCell_fromTabStr_Static(line, header), lines);
    return cells;
}

export function ARCtrl_CompositeCell__CompositeCell_ConvertToValidCell_6CAF647B(this$, header) {
    let matchResult;
    switch (this$.tag) {
        case 2: {
            if (header.IsTermColumn) {
                matchResult = 1;
            }
            else if (header.IsDataColumn) {
                matchResult = 5;
            }
            else {
                matchResult = 8;
            }
            break;
        }
        case 1: {
            if (header.IsTermColumn) {
                matchResult = 2;
            }
            else if (header.IsDataColumn) {
                matchResult = 6;
            }
            else {
                matchResult = 9;
            }
            break;
        }
        case 3: {
            if (header.IsTermColumn) {
                matchResult = 3;
            }
            else if (header.IsDataColumn) {
                matchResult = 7;
            }
            else {
                matchResult = 10;
            }
            break;
        }
        default:
            if (header.IsTermColumn) {
                matchResult = 0;
            }
            else if (header.IsDataColumn) {
                matchResult = 4;
            }
            else {
                matchResult = 8;
            }
    }
    switch (matchResult) {
        case 0:
            return this$;
        case 1:
            return this$;
        case 2:
            return this$.ToTermCell();
        case 3:
            return this$.ToTermCell();
        case 4:
            return this$.ToDataCell();
        case 5:
            return this$.ToDataCell();
        case 6:
            return this$.ToDataCell();
        case 7:
            return this$;
        case 8:
            return this$.ToFreeTextCell();
        case 9:
            return this$;
        default:
            return this$.ToFreeTextCell();
    }
}

export function ARCtrl_CompositeCell__CompositeCell_UpdateWithData_3A03A101(this$, d) {
    switch (this$.tag) {
        case 2: {
            const v = this$.fields[0];
            return CompositeCell.createUnitized(v, OntologyAnnotation.create(d.NameText));
        }
        case 1:
            return CompositeCell.createFreeText(d.NameText);
        case 3:
            return CompositeCell.createData(d);
        default:
            return CompositeCell.createTerm(OntologyAnnotation.create(d.NameText));
    }
}

export function ARCtrl_CompositeCell__CompositeCell_ToOA(this$) {
    switch (this$.tag) {
        case 2: {
            const v = this$.fields[0];
            const oa_1 = this$.fields[1];
            return oa_1;
        }
        case 1: {
            const t = this$.fields[0];
            return OntologyAnnotation.create(t);
        }
        case 3: {
            const d = this$.fields[0];
            return OntologyAnnotation.create(d.NameText);
        }
        default: {
            const oa = this$.fields[0];
            return oa;
        }
    }
}

export function ARCtrl_CompositeCell__CompositeCell_UpdateMainField_Z721C83C5(this$, s) {
    switch (this$.tag) {
        case 2: {
            const oa_1 = this$.fields[1];
            return new CompositeCell(2, [s, oa_1]);
        }
        case 1:
            return new CompositeCell(1, [s]);
        case 3: {
            const d = this$.fields[0];
            d.FilePath = s;
            return new CompositeCell(3, [d]);
        }
        default: {
            const oa = this$.fields[0];
            oa.Name = s;
            return new CompositeCell(0, [oa]);
        }
    }
}

/**
 * Will return `this` if executed on Freetext cell.
 */
export function ARCtrl_CompositeCell__CompositeCell_UpdateTSR_Z721C83C5(this$, tsr) {
    const updateTSR = (oa) => {
        oa.TermSourceREF = tsr;
        return oa;
    };
    switch (this$.tag) {
        case 0: {
            const oa_1 = this$.fields[0];
            return new CompositeCell(0, [updateTSR(oa_1)]);
        }
        case 2: {
            const v = this$.fields[0];
            const oa_2 = this$.fields[1];
            return new CompositeCell(2, [v, updateTSR(oa_2)]);
        }
        default:
            return this$;
    }
}

/**
 * Will return `this` if executed on Freetext cell.
 */
export function ARCtrl_CompositeCell__CompositeCell_UpdateTAN_Z721C83C5(this$, tan) {
    const updateTAN = (oa) => {
        oa.TermSourceREF = tan;
        return oa;
    };
    switch (this$.tag) {
        case 0: {
            const oa_1 = this$.fields[0];
            return new CompositeCell(0, [updateTAN(oa_1)]);
        }
        case 2: {
            const v = this$.fields[0];
            const oa_2 = this$.fields[1];
            return new CompositeCell(2, [v, updateTAN(oa_2)]);
        }
        default:
            return this$;
    }
}

export class Extensions_SearchComponent extends Record {
    constructor(Key, KeyType, Body) {
        super();
        this.Key = Key;
        this.KeyType = KeyType;
        this.Body = Body;
    }
}

export function Extensions_SearchComponent_$reflection() {
    return record_type("Shared.Extensions.SearchComponent", [], Extensions_SearchComponent, () => [["Key", OntologyAnnotation_$reflection()], ["KeyType", CompositeHeaderDiscriminate_$reflection()], ["Body", CompositeCell_$reflection()]]);
}

export class Extensions_Annotation extends Record {
    constructor(IsOpen, Search) {
        super();
        this.IsOpen = IsOpen;
        this.Search = Search;
    }
}

export function Extensions_Annotation_$reflection() {
    return record_type("Shared.Extensions.Annotation", [], Extensions_Annotation, () => [["IsOpen", bool_type], ["Search", Extensions_SearchComponent_$reflection()]]);
}

export function Extensions_Annotation_init_Z1671BC77(key, body, keyType, isOpen, search) {
    const isOpen_1 = defaultArg(isOpen, true);
    const keyType_1 = defaultArg(keyType, new CompositeHeaderDiscriminate(3, []));
    const search_1 = defaultArg(search, new Extensions_SearchComponent(key, keyType_1, body));
    return new Extensions_Annotation(isOpen_1, search_1);
}

export function Extensions_Annotation__ToggleOpen(this$) {
    return new Extensions_Annotation(!this$.IsOpen, this$.Search);
}

