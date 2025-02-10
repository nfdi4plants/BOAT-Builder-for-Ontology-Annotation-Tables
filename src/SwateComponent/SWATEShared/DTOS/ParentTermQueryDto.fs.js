import { Record } from "../../fable_modules/fable-library-js.4.24.0/Types.js";
import { array_type, record_type, option_type, int32_type, string_type } from "../../fable_modules/fable-library-js.4.24.0/Reflection.js";
import { Term_$reflection } from "../Database.fs.js";

export class ParentTermQueryDto extends Record {
    constructor(parentTermId, limit) {
        super();
        this.parentTermId = parentTermId;
        this.limit = limit;
    }
}

export function ParentTermQueryDto_$reflection() {
    return record_type("Shared.DTOs.ParentTermQuery.ParentTermQueryDto", [], ParentTermQueryDto, () => [["parentTermId", string_type], ["limit", option_type(int32_type)]]);
}

export function ParentTermQueryDto_create_3B406CA4(parentTermId, limit) {
    return new ParentTermQueryDto(parentTermId, limit);
}

export class ParentTermQueryDtoResults extends Record {
    constructor(query, results) {
        super();
        this.query = query;
        this.results = results;
    }
}

export function ParentTermQueryDtoResults_$reflection() {
    return record_type("Shared.DTOs.ParentTermQuery.ParentTermQueryDtoResults", [], ParentTermQueryDtoResults, () => [["query", ParentTermQueryDto_$reflection()], ["results", array_type(Term_$reflection())]]);
}

export function ParentTermQueryDtoResults_create_6576985D(query, results) {
    return new ParentTermQueryDtoResults(query, results);
}

