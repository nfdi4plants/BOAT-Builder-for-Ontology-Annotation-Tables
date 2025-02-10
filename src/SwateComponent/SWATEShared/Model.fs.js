import { Record, toString, Union } from "../fable_modules/fable-library-js.4.24.0/Types.js";
import { CompositeHeaderDiscriminate_$reflection } from "./ARCtrl.Helper.fs.js";
import { record_type, bool_type, union_type } from "../fable_modules/fable-library-js.4.24.0/Reflection.js";

export class DropdownPage extends Union {
    constructor(tag, fields) {
        super();
        this.tag = tag;
        this.fields = fields;
    }
    cases() {
        return ["Main", "More", "IOTypes"];
    }
}

export function DropdownPage_$reflection() {
    return union_type("BuildingBlock.DropdownPage", [], DropdownPage, () => [[], [], [["Item", CompositeHeaderDiscriminate_$reflection()]]]);
}

export function DropdownPage__get_toString(this$) {
    switch (this$.tag) {
        case 1:
            return "More";
        case 2: {
            const t = this$.fields[0];
            return toString(t);
        }
        default:
            return "Main Page";
    }
}

export function DropdownPage__get_toTooltip(this$) {
    switch (this$.tag) {
        case 1:
            return "More";
        case 2: {
            const t = this$.fields[0];
            return `Per table only one ${t} is allowed. The value of this column must be a unique identifier.`;
        }
        default:
            return "";
    }
}

export class BuildingBlockUIState extends Record {
    constructor(DropdownIsActive, DropdownPage) {
        super();
        this.DropdownIsActive = DropdownIsActive;
        this.DropdownPage = DropdownPage;
    }
}

export function BuildingBlockUIState_$reflection() {
    return record_type("BuildingBlock.BuildingBlockUIState", [], BuildingBlockUIState, () => [["DropdownIsActive", bool_type], ["DropdownPage", DropdownPage_$reflection()]]);
}

export function BuildingBlockUIState_init() {
    return new BuildingBlockUIState(false, new DropdownPage(0, []));
}

