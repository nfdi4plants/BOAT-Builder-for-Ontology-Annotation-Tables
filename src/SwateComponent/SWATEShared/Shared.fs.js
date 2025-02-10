import { replace, create, match } from "../fable_modules/fable-library-js.4.24.0/RegExp.js";
import { Record } from "../fable_modules/fable-library-js.4.24.0/Types.js";
import { anonRecord_type, tuple_type, array_type, lambda_type, class_type, int32_type, unit_type, record_type, bool_type, option_type, string_type } from "../fable_modules/fable-library-js.4.24.0/Reflection.js";
import { printf, toText } from "../fable_modules/fable-library-js.4.24.0/String.js";
import { intersect, count, FSharpSet__get_Count, ofSeq } from "../fable_modules/fable-library-js.4.24.0/Set.js";
import { sortByDescending, windowed, item, map } from "../fable_modules/fable-library-js.4.24.0/Array.js";
import { comparePrimitives } from "../fable_modules/fable-library-js.4.24.0/Util.js";
import { TermQueryDtoResults_$reflection, TermQueryDto_$reflection } from "./DTOS/TermQueryDto.fs.js";
import { TreeTypes_Tree_$reflection, Ontology_$reflection, Term_$reflection } from "./Database.fs.js";
import { ParentTermQueryDtoResults_$reflection, ParentTermQueryDto_$reflection } from "./DTOS/ParentTermQueryDto.fs.js";

/**
 * (|Regex|_|) pattern input
 */
export function Regex_$007CRegex$007C_$007C(pattern, input) {
    const m = match(create(pattern), input);
    if (m != null) {
        return m;
    }
    else {
        return undefined;
    }
}

export class AdvancedSearchTypes_AdvancedSearchOptions extends Record {
    constructor(OntologyName, TermName, TermDefinition, KeepObsolete) {
        super();
        this.OntologyName = OntologyName;
        this.TermName = TermName;
        this.TermDefinition = TermDefinition;
        this.KeepObsolete = KeepObsolete;
    }
}

export function AdvancedSearchTypes_AdvancedSearchOptions_$reflection() {
    return record_type("Shared.AdvancedSearchTypes.AdvancedSearchOptions", [], AdvancedSearchTypes_AdvancedSearchOptions, () => [["OntologyName", option_type(string_type)], ["TermName", string_type], ["TermDefinition", string_type], ["KeepObsolete", bool_type]]);
}

export function AdvancedSearchTypes_AdvancedSearchOptions_init() {
    return new AdvancedSearchTypes_AdvancedSearchOptions(undefined, "", "", false);
}

export function Route_builder(typeName, methodName) {
    return toText(printf("https://swate-alpha.nfdi4plants.org/api/%s/%s"))(typeName)(methodName);
}

export function SorensenDice_createBigrams(s) {
    return ofSeq(map((inner) => {
        const arg = item(0, inner);
        const arg_1 = item(1, inner);
        return toText(printf("%c%c"))(arg)(arg_1);
    }, windowed(2, s.toUpperCase().split(""))), {
        Compare: comparePrimitives,
    });
}

export function SorensenDice_sortBySimilarity(searchStr, f, arrayToSort) {
    const searchSet = SorensenDice_createBigrams(searchStr);
    return sortByDescending((result) => {
        const resultSet = SorensenDice_createBigrams(f(result));
        const x = resultSet;
        const y = searchSet;
        const matchValue = FSharpSet__get_Count(x) | 0;
        const matchValue_1 = FSharpSet__get_Count(y) | 0;
        let matchResult, xCount, yCount;
        if (matchValue === 0) {
            if (matchValue_1 === 0) {
                matchResult = 0;
            }
            else {
                matchResult = 1;
                xCount = matchValue;
                yCount = matchValue_1;
            }
        }
        else {
            matchResult = 1;
            xCount = matchValue;
            yCount = matchValue_1;
        }
        switch (matchResult) {
            case 0:
                return 1;
            default:
                return (2 * count(intersect(x, y))) / (xCount + yCount);
        }
    }, arrayToSort, {
        Compare: comparePrimitives,
    });
}

export class IOntologyAPIv3 extends Record {
    constructor(getTestNumber, searchTerm, searchTerms, getTermById, findAllChildTerms) {
        super();
        this.getTestNumber = getTestNumber;
        this.searchTerm = searchTerm;
        this.searchTerms = searchTerms;
        this.getTermById = getTermById;
        this.findAllChildTerms = findAllChildTerms;
    }
}

export function IOntologyAPIv3_$reflection() {
    return record_type("Shared.IOntologyAPIv3", [], IOntologyAPIv3, () => [["getTestNumber", lambda_type(unit_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [int32_type]))], ["searchTerm", lambda_type(TermQueryDto_$reflection(), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["searchTerms", lambda_type(array_type(TermQueryDto_$reflection()), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(TermQueryDtoResults_$reflection())]))], ["getTermById", lambda_type(string_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [option_type(Term_$reflection())]))], ["findAllChildTerms", lambda_type(ParentTermQueryDto_$reflection(), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [ParentTermQueryDtoResults_$reflection()]))]]);
}

export class ITestAPI extends Record {
    constructor(test, postTest) {
        super();
        this.test = test;
        this.postTest = postTest;
    }
}

export function ITestAPI_$reflection() {
    return record_type("Shared.ITestAPI", [], ITestAPI, () => [["test", lambda_type(unit_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [tuple_type(string_type, string_type)]))], ["postTest", lambda_type(string_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [tuple_type(string_type, string_type)]))]]);
}

export class IServiceAPIv1 extends Record {
    constructor(getAppVersion) {
        super();
        this.getAppVersion = getAppVersion;
    }
}

export function IServiceAPIv1_$reflection() {
    return record_type("Shared.IServiceAPIv1", [], IServiceAPIv1, () => [["getAppVersion", lambda_type(unit_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [string_type]))]]);
}

export class ITemplateAPIv1 extends Record {
    constructor(getTemplates, getTemplateById) {
        super();
        this.getTemplates = getTemplates;
        this.getTemplateById = getTemplateById;
    }
}

export function ITemplateAPIv1_$reflection() {
    return record_type("Shared.ITemplateAPIv1", [], ITemplateAPIv1, () => [["getTemplates", lambda_type(unit_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [string_type]))], ["getTemplateById", lambda_type(string_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [string_type]))]]);
}

export const SwateObsolete_Regex_Pattern_TermAnnotationShortPattern = `(?<${"idspace"}>\\w+?):(?<${"localid"}>\\w+)`;

export const SwateObsolete_Regex_Pattern_TermAnnotationURIPattern = `.*\\/(?<${"idspace"}>\\w+?)[:_](?<${"localid"}>\\w+)`;

export function SwateObsolete_Regex_parseSquaredTermNameBrackets(headerStr) {
    const activePatternResult = Regex_$007CRegex$007C_$007C("\\[.*\\]", headerStr);
    if (activePatternResult != null) {
        const value = activePatternResult;
        return replace(value[0].trim().slice(1, (value[0].length - 2) + 1), "#\\d+", "");
    }
    else {
        return undefined;
    }
}

export function SwateObsolete_Regex_parseCoreName(headerStr) {
    const activePatternResult = Regex_$007CRegex$007C_$007C("^[^[(]*", headerStr);
    if (activePatternResult != null) {
        const value = activePatternResult;
        return value[0].trim();
    }
    else {
        return undefined;
    }
}

/**
 * This function can be used to extract `IDSPACE:LOCALID` (or: `Term Accession` from Swate header strings or obofoundry conform URI strings.
 */
export function SwateObsolete_Regex_parseTermAccession(headerStr) {
    const matchValue = headerStr.trim();
    const activePatternResult = Regex_$007CRegex$007C_$007C(SwateObsolete_Regex_Pattern_TermAnnotationShortPattern, matchValue);
    if (activePatternResult != null) {
        const value = activePatternResult;
        return value[0].trim();
    }
    else {
        const activePatternResult_1 = Regex_$007CRegex$007C_$007C(SwateObsolete_Regex_Pattern_TermAnnotationURIPattern, matchValue);
        if (activePatternResult_1 != null) {
            const value_1 = activePatternResult_1;
            const idspace = (value_1.groups && value_1.groups.idspace) || "";
            const localid = (value_1.groups && value_1.groups.localid) || "";
            return (idspace + ":") + localid;
        }
        else {
            return undefined;
        }
    }
}

export function SwateObsolete_Regex_parseDoubleQuotes(headerStr) {
    const activePatternResult = Regex_$007CRegex$007C_$007C("\"(.*?)\"", headerStr);
    if (activePatternResult != null) {
        const value = activePatternResult;
        return value[0].slice(1, (value[0].length - 2) + 1).trim();
    }
    else {
        return undefined;
    }
}

export function SwateObsolete_Regex_getId(headerStr) {
    const activePatternResult = Regex_$007CRegex$007C_$007C("#\\d+", headerStr);
    if (activePatternResult != null) {
        const value = activePatternResult;
        return value[0].trim().slice(1, value[0].trim().length);
    }
    else {
        return undefined;
    }
}

export class SwateObsolete_TermMinimal extends Record {
    constructor(Name, TermAccession) {
        super();
        this.Name = Name;
        this.TermAccession = TermAccession;
    }
}

export function SwateObsolete_TermMinimal_$reflection() {
    return record_type("Shared.SwateObsolete.TermMinimal", [], SwateObsolete_TermMinimal, () => [["Name", string_type], ["TermAccession", string_type]]);
}

export function SwateObsolete_TermMinimal_create(name, tan) {
    return new SwateObsolete_TermMinimal(name, tan);
}

export class SwateObsolete_TermSearchable extends Record {
    constructor(Term, ParentTerm, IsUnit, ColIndex, RowIndices, SearchResultTerm) {
        super();
        this.Term = Term;
        this.ParentTerm = ParentTerm;
        this.IsUnit = IsUnit;
        this.ColIndex = (ColIndex | 0);
        this.RowIndices = RowIndices;
        this.SearchResultTerm = SearchResultTerm;
    }
}

export function SwateObsolete_TermSearchable_$reflection() {
    return record_type("Shared.SwateObsolete.TermSearchable", [], SwateObsolete_TermSearchable, () => [["Term", SwateObsolete_TermMinimal_$reflection()], ["ParentTerm", option_type(SwateObsolete_TermMinimal_$reflection())], ["IsUnit", bool_type], ["ColIndex", int32_type], ["RowIndices", array_type(int32_type)], ["SearchResultTerm", option_type(Term_$reflection())]]);
}

export class IOntologyAPIv1 extends Record {
    constructor(getTestNumber, getAllOntologies, getTermSuggestions, getTermSuggestionsByParentTerm, getAllTermsByParentTerm, getTermSuggestionsByChildTerm, getAllTermsByChildTerm, getTermsForAdvancedSearch, getUnitTermSuggestions, getTermsByNames, getTreeByAccession) {
        super();
        this.getTestNumber = getTestNumber;
        this.getAllOntologies = getAllOntologies;
        this.getTermSuggestions = getTermSuggestions;
        this.getTermSuggestionsByParentTerm = getTermSuggestionsByParentTerm;
        this.getAllTermsByParentTerm = getAllTermsByParentTerm;
        this.getTermSuggestionsByChildTerm = getTermSuggestionsByChildTerm;
        this.getAllTermsByChildTerm = getAllTermsByChildTerm;
        this.getTermsForAdvancedSearch = getTermsForAdvancedSearch;
        this.getUnitTermSuggestions = getUnitTermSuggestions;
        this.getTermsByNames = getTermsByNames;
        this.getTreeByAccession = getTreeByAccession;
    }
}

export function IOntologyAPIv1_$reflection() {
    return record_type("Shared.IOntologyAPIv1", [], IOntologyAPIv1, () => [["getTestNumber", lambda_type(unit_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [int32_type]))], ["getAllOntologies", lambda_type(unit_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Ontology_$reflection())]))], ["getTermSuggestions", lambda_type(tuple_type(int32_type, string_type), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getTermSuggestionsByParentTerm", lambda_type(tuple_type(int32_type, string_type, SwateObsolete_TermMinimal_$reflection()), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getAllTermsByParentTerm", lambda_type(SwateObsolete_TermMinimal_$reflection(), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getTermSuggestionsByChildTerm", lambda_type(tuple_type(int32_type, string_type, SwateObsolete_TermMinimal_$reflection()), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getAllTermsByChildTerm", lambda_type(SwateObsolete_TermMinimal_$reflection(), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getTermsForAdvancedSearch", lambda_type(AdvancedSearchTypes_AdvancedSearchOptions_$reflection(), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getUnitTermSuggestions", lambda_type(tuple_type(int32_type, string_type), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getTermsByNames", lambda_type(array_type(SwateObsolete_TermSearchable_$reflection()), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(SwateObsolete_TermSearchable_$reflection())]))], ["getTreeByAccession", lambda_type(string_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [TreeTypes_Tree_$reflection()]))]]);
}

export class IOntologyAPIv2 extends Record {
    constructor(getTestNumber, getAllOntologies, getTermSuggestions, getTermSuggestionsByParentTerm, getAllTermsByParentTerm, getTermSuggestionsByChildTerm, getAllTermsByChildTerm, getTermsForAdvancedSearch, getUnitTermSuggestions, getTermsByNames, getTreeByAccession) {
        super();
        this.getTestNumber = getTestNumber;
        this.getAllOntologies = getAllOntologies;
        this.getTermSuggestions = getTermSuggestions;
        this.getTermSuggestionsByParentTerm = getTermSuggestionsByParentTerm;
        this.getAllTermsByParentTerm = getAllTermsByParentTerm;
        this.getTermSuggestionsByChildTerm = getTermSuggestionsByChildTerm;
        this.getAllTermsByChildTerm = getAllTermsByChildTerm;
        this.getTermsForAdvancedSearch = getTermsForAdvancedSearch;
        this.getUnitTermSuggestions = getUnitTermSuggestions;
        this.getTermsByNames = getTermsByNames;
        this.getTreeByAccession = getTreeByAccession;
    }
}

export function IOntologyAPIv2_$reflection() {
    return record_type("Shared.IOntologyAPIv2", [], IOntologyAPIv2, () => [["getTestNumber", lambda_type(unit_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [int32_type]))], ["getAllOntologies", lambda_type(unit_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Ontology_$reflection())]))], ["getTermSuggestions", lambda_type(anonRecord_type(["n", int32_type], ["ontology", option_type(string_type)], ["query", string_type]), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getTermSuggestionsByParentTerm", lambda_type(anonRecord_type(["n", int32_type], ["parent_term", SwateObsolete_TermMinimal_$reflection()], ["query", string_type]), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getAllTermsByParentTerm", lambda_type(SwateObsolete_TermMinimal_$reflection(), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getTermSuggestionsByChildTerm", lambda_type(anonRecord_type(["child_term", SwateObsolete_TermMinimal_$reflection()], ["n", int32_type], ["query", string_type]), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getAllTermsByChildTerm", lambda_type(SwateObsolete_TermMinimal_$reflection(), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getTermsForAdvancedSearch", lambda_type(AdvancedSearchTypes_AdvancedSearchOptions_$reflection(), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getUnitTermSuggestions", lambda_type(anonRecord_type(["n", int32_type], ["query", string_type]), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(Term_$reflection())]))], ["getTermsByNames", lambda_type(array_type(SwateObsolete_TermSearchable_$reflection()), class_type("Microsoft.FSharp.Control.FSharpAsync`1", [array_type(SwateObsolete_TermSearchable_$reflection())]))], ["getTreeByAccession", lambda_type(string_type, class_type("Microsoft.FSharp.Control.FSharpAsync`1", [TreeTypes_Tree_$reflection()]))]]);
}

