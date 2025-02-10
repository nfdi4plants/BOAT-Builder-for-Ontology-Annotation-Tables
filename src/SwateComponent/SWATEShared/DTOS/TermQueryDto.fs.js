import { Record } from "../../fable_modules/fable-library-js.4.24.0/Types.js";
import { array_type, record_type, list_type, option_type, int32_type, string_type } from "../../fable_modules/fable-library-js.4.24.0/Reflection.js";
import { Term_$reflection, FullTextSearch_$reflection } from "../Database.fs.js";

export class TermQueryDto extends Record {
    constructor(query, limit, parentTermId, ontologies, searchMode) {
        super();
        this.query = query;
        this.limit = limit;
        this.parentTermId = parentTermId;
        this.ontologies = ontologies;
        this.searchMode = searchMode;
    }
}

export function TermQueryDto_$reflection() {
    return record_type("Shared.DTOs.TermQuery.TermQueryDto", [], TermQueryDto, () => [["query", string_type], ["limit", option_type(int32_type)], ["parentTermId", option_type(string_type)], ["ontologies", option_type(list_type(string_type))], ["searchMode", option_type(FullTextSearch_$reflection())]]);
}

export function TermQueryDto_create_22AB55AC(query, limit, parentTermId, ontologies, searchMode) {
    return new TermQueryDto(query, limit, parentTermId, ontologies, searchMode);
}

export class TermQueryDtoResults extends Record {
    constructor(query, results) {
        super();
        this.query = query;
        this.results = results;
    }
}

export function TermQueryDtoResults_$reflection() {
    return record_type("Shared.DTOs.TermQuery.TermQueryDtoResults", [], TermQueryDtoResults, () => [["query", TermQueryDto_$reflection()], ["results", array_type(Term_$reflection())]]);
}

export function TermQueryDtoResults_create_29EF691D(query, results) {
    return new TermQueryDtoResults(query, results);
}

