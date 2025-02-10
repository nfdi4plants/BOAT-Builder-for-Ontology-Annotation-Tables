import { Record } from "../fable_modules/fable-library-js.4.24.0/Types.js";
import { record_type, option_type, class_type, array_type, string_type } from "../fable_modules/fable-library-js.4.24.0/Reflection.js";
import { createObj, isArrayLike } from "../fable_modules/fable-library-js.4.24.0/Util.js";
import { join } from "../fable_modules/fable-library-js.4.24.0/String.js";
import { value as value_5, map, defaultArg } from "../fable_modules/fable-library-js.4.24.0/Option.js";
import { FSharpMap__TryFind } from "../fable_modules/fable-library-js.4.24.0/Map.js";
import { createElement } from "react";
import React from "react";
import { reactApi } from "../fable_modules/Feliz.2.9.0/./Interop.fs.js";
import { empty, singleton, append, delay, toList } from "../fable_modules/fable-library-js.4.24.0/Seq.js";
import { ofArray } from "../fable_modules/fable-library-js.4.24.0/List.js";

export class Style extends Record {
    constructor(classes, subClasses) {
        super();
        this.classes = classes;
        this.subClasses = subClasses;
    }
}

export function Style_$reflection() {
    return record_type("Components.Style", [], Style, () => [["classes", class_type("Fable.Core.U2`2", [string_type, array_type(string_type)])], ["subClasses", option_type(class_type("Microsoft.FSharp.Collections.FSharpMap`2", [string_type, Style_$reflection()]))]]);
}

export function Style_init_Z501027E3(classes, subClasses) {
    return new Style(classes, subClasses);
}

export function Style_init_ZAC2BB4A(classes, subClasses) {
    return new Style(classes, subClasses);
}

export function Style__get_StyleString(this$) {
    const matchValue = this$.classes;
    if (isArrayLike(matchValue)) {
        const styleArr = matchValue;
        return join(" ", styleArr);
    }
    else {
        const style = matchValue;
        return style;
    }
}

export function Style__GetSubclassStyle_Z721C83C5(this$, name) {
    let matchValue, subClasses;
    return defaultArg(map(Style__get_StyleString, (matchValue = this$.subClasses, (matchValue == null) ? undefined : ((subClasses = matchValue, FSharpMap__TryFind(subClasses, name))))), "");
}

export class BaseDropdown {
    constructor() {
    }
}

export function BaseDropdown_$reflection() {
    return class_type("Components.BaseDropdown", undefined, BaseDropdown);
}

export function BaseDropdown_Main_427AC1C3(baseDropdown_Main_427AC1C3InputProps) {
    let elems_1;
    const style = baseDropdown_Main_427AC1C3InputProps.style;
    const children = baseDropdown_Main_427AC1C3InputProps.children;
    const toggle = baseDropdown_Main_427AC1C3InputProps.toggle;
    const setIsOpen = baseDropdown_Main_427AC1C3InputProps.setIsOpen;
    const isOpen = baseDropdown_Main_427AC1C3InputProps.isOpen;
    const ref = reactApi.useRef(undefined);
    return createElement("div", createObj(ofArray([["ref", ref], ["className", join(" ", toList(delay(() => append(singleton("dropdown"), delay(() => append(isOpen ? singleton("dropdown-open") : empty(), delay(() => ((style != null) ? singleton(Style__get_StyleString(value_5(style))) : empty()))))))))], (elems_1 = [toggle, createElement("ul", {
        className: join(" ", toList(delay(() => append(singleton("dropdown-content min-w-48 menu bg-base-200 rounded-box z-[1] p-2 shadow !top-[110%]"), delay(() => ((style != null) ? singleton(Style__GetSubclassStyle_Z721C83C5(value_5(style), "content")) : empty())))))),
        children: reactApi.Children.toArray(Array.from(children)),
    })], ["children", reactApi.Children.toArray(Array.from(elems_1))])])));
}

