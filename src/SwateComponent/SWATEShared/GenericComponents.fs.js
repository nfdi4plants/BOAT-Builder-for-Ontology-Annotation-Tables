import { class_type } from "../fable_modules/fable-library-js.4.24.0/Reflection.js";
import { createElement } from "react";
import { createObj } from "../fable_modules/fable-library-js.4.24.0/Util.js";
import { join } from "../fable_modules/fable-library-js.4.24.0/String.js";
import { empty, singleton, append, delay, toList } from "../fable_modules/fable-library-js.4.24.0/Seq.js";
import { value as value_9 } from "../fable_modules/fable-library-js.4.24.0/Option.js";
import { reactApi } from "../fable_modules/Feliz.2.9.0/./Interop.fs.js";
import { ofArray } from "../fable_modules/fable-library-js.4.24.0/List.js";

export class Components {
    constructor() {
    }
}

export function Components_$reflection() {
    return class_type("Components.Components", undefined, Components);
}

export function Components_CollapseButton_Z762A16CE(isCollapsed, setIsCollapsed, collapsedIcon, collapseIcon, classes) {
    let elems;
    return createElement("label", createObj(ofArray([["className", join(" ", toList(delay(() => append(singleton("btn btn-square swap swap-rotate grow-0"), delay(() => ((classes != null) ? singleton(value_9(classes)) : empty()))))))], ["onClick", (e) => {
        e.preventDefault();
        e.stopPropagation();
        setIsCollapsed(!isCollapsed);
    }], (elems = [createElement("input", {
        type: "checkbox",
        checked: isCollapsed,
        onChange: (ev) => {
            const _arg = ev.target.checked;
        },
    }), createElement("i", {
        className: join(" ", toList(delay(() => append(singleton("swap-off fa-solid"), delay(() => {
            if (collapsedIcon != null) {
                value_9(collapsedIcon);
                return empty();
            }
            else {
                return singleton("fa-solid fa-chevron-down");
            }
        }))))),
    }), createElement("i", {
        className: join(" ", toList(delay(() => append(singleton("swap-on"), delay(() => {
            if (collapseIcon != null) {
                value_9(collapseIcon);
                return empty();
            }
            else {
                return singleton("fa-solid fa-x");
            }
        }))))),
    })], ["children", reactApi.Children.toArray(Array.from(elems))])])));
}

