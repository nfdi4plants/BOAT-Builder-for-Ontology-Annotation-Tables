import { createElement } from "react";
import React from "react";
import * as react from "react";
import { reactApi } from "../fable_modules/Feliz.2.9.0/./Interop.fs.js";
import { comparePrimitives, createObj } from "../fable_modules/fable-library-js.4.24.0/Util.js";
import { Helpers_combineClasses } from "../fable_modules/Feliz.DaisyUI.4.2.1/./DaisyUI.fs.js";
import { PropHelpers_createOnKey } from "../fable_modules/Feliz.2.9.0/./Properties.fs.js";
import { key_enter } from "../fable_modules/Feliz.2.9.0/Key.fs.js";
import { mapIndexed, item, singleton, ofArray } from "../fable_modules/fable-library-js.4.24.0/List.js";
import { DropdownPage as DropdownPage_1, DropdownPage__get_toString, BuildingBlockUIState } from "./Model.fs.js";
import { empty, singleton as singleton_1, append, delay, toList } from "../fable_modules/fable-library-js.4.24.0/Seq.js";
import { CompositeHeaderDiscriminate, Extensions_Annotation, Extensions_SearchComponent, CompositeHeaderDiscriminate__HasIOType, CompositeHeaderDiscriminate__IsTermColumn } from "./ARCtrl.Helper.fs.js";
import { OntologyAnnotation } from "../fable_modules/ARCtrl.Core.2.0.0-swate/OntologyAnnotation.fs.js";
import { CompositeCell } from "../fable_modules/ARCtrl.Core.2.0.0-swate/Table/CompositeCell.fs.js";
import { toString } from "../fable_modules/fable-library-js.4.24.0/Types.js";
import { Style_init_Z501027E3, BaseDropdown_Main_427AC1C3 } from "./BaseDropdown.fs.js";
import { join } from "../fable_modules/fable-library-js.4.24.0/String.js";
import { defaultOf } from "../fable_modules/Feliz.2.9.0/../Feliz.Router.4.0.0/../fable-library-js.4.24.0/Util.js";
import { ofSeq } from "../fable_modules/fable-library-js.4.24.0/Map.js";

export function FreeTextInputElement(freeTextInputElementInputProps) {
    let elems_1, elems;
    const onSubmit = freeTextInputElementInputProps.onSubmit;
    const patternInput = reactApi.useState("");
    const setInput = patternInput[1];
    const inputS = patternInput[0];
    return createElement("div", createObj(ofArray([["className", "flex flex-row gap-0 p-0"], (elems_1 = [createElement("input", createObj(Helpers_combineClasses("input", ofArray([["type", "text"], ["className", "join-item"], ["className", "input-sm"], ["placeholder", "..."], ["className", "grow truncate"], ["onClick", (e) => {
        e.stopPropagation();
    }], ["onChange", (ev) => {
        setInput(ev.target.value);
    }], ["onKeyDown", (ev_1) => {
        PropHelpers_createOnKey(key_enter, (e_1) => {
            e_1.stopPropagation();
            onSubmit(inputS);
        }, ev_1);
    }]])))), createElement("button", createObj(Helpers_combineClasses("btn", ofArray([["className", "join-item"], ["className", "btn-accent"], ["className", "btn-sm"], ["onClick", (e_2) => {
        e_2.stopPropagation();
        onSubmit(inputS);
    }], (elems = [createElement("i", {
        className: "fa-solid fa-check",
    })], ["children", reactApi.Children.toArray(Array.from(elems))])]))))], ["children", reactApi.Children.toArray(Array.from(elems_1))])])));
}

const DropdownElements_divider = createElement("div", createObj(Helpers_combineClasses("divider", singleton(["className", "mx-2 my-0"]))));

const DropdownElements_annotationsPrinciplesLink = createElement("a", {
    href: "https://nfdi4plants.github.io/AnnotationPrinciples/",
    target: "_blank",
    className: "ml-auto link-info",
    children: "info",
});

function DropdownElements_createSubBuildingBlockDropdownLink(state, setState, subpage) {
    let elems_1, elems;
    return createElement("li", createObj(ofArray([["onClick", (e) => {
        e.preventDefault();
        e.stopPropagation();
        setState(new BuildingBlockUIState(state.DropdownIsActive, subpage));
    }], (elems_1 = [createElement("div", createObj(ofArray([["className", "flex flex-row justify-between"], (elems = [createElement("span", {
        children: [DropdownPage__get_toString(subpage)],
    }), createElement("i", {
        className: "fa-solid fa-arrow-right",
    })], ["children", reactApi.Children.toArray(Array.from(elems))])])))], ["children", reactApi.Children.toArray(Array.from(elems_1))])])));
}

function DropdownElements_DropdownContentInfoFooter(setState, hasBack) {
    let elems_1;
    return createElement("li", createObj(ofArray([["className", "flex flex-row justify-between pt-1"], ["onClick", (e) => {
        e.preventDefault();
        e.stopPropagation();
        setState(new BuildingBlockUIState(true, new DropdownPage_1(0, [])));
    }], (elems_1 = toList(delay(() => {
        let elems;
        return append(hasBack ? singleton_1(createElement("a", createObj(ofArray([["className", "content-center"], (elems = [createElement("i", {
            className: "fa-solid fa-arrow-left",
        })], ["children", reactApi.Children.toArray(Array.from(elems))])])))) : empty(), delay(() => singleton_1(DropdownElements_annotationsPrinciplesLink)));
    })), ["children", reactApi.Children.toArray(Array.from(elems_1))])])));
}

function DropdownElements_isSameMajorCompositeHeaderDiscriminate(hct1, hct2) {
    if (CompositeHeaderDiscriminate__IsTermColumn(hct1) === CompositeHeaderDiscriminate__IsTermColumn(hct2)) {
        return CompositeHeaderDiscriminate__HasIOType(hct1) === CompositeHeaderDiscriminate__HasIOType(hct2);
    }
    else {
        return false;
    }
}

function DropdownElements_selectCompositeHeaderDiscriminate(hct, setUiState, close, annoState, setAnnoState, a) {
    let nextState;
    if (DropdownElements_isSameMajorCompositeHeaderDiscriminate(item(a, annoState).Search.KeyType, hct)) {
        nextState = mapIndexed((i, anno) => {
            let bind$0040_1;
            if (i === a) {
                const bind$0040 = item(a, annoState);
                return new Extensions_Annotation(bind$0040.IsOpen, (bind$0040_1 = bind$0040.Search, new Extensions_SearchComponent(bind$0040_1.Key, hct, bind$0040_1.Body)));
            }
            else {
                return anno;
            }
        }, annoState);
    }
    else {
        const nextBodyCellType = CompositeHeaderDiscriminate__IsTermColumn(hct) ? (new CompositeCell(0, [new OntologyAnnotation("")])) : (new CompositeCell(1, [""]));
        nextState = mapIndexed((i_1, anno_1) => {
            if (i_1 === a) {
                const bind$0040_2 = item(a, annoState);
                return new Extensions_Annotation(bind$0040_2.IsOpen, new Extensions_SearchComponent(bind$0040_2.Search.Key, hct, nextBodyCellType));
            }
            else {
                return anno_1;
            }
        }, annoState);
    }
    setAnnoState(nextState);
    close();
    return setUiState(new BuildingBlockUIState(false, new DropdownPage_1(0, [])));
}

function DropdownElements_createBuildingBlockDropdownItem(setUiState, close, annoState, setState, a, headerType) {
    const children = singleton(createElement("a", {
        onClick: (e) => {
            e.stopPropagation();
            DropdownElements_selectCompositeHeaderDiscriminate(headerType, setUiState, close, annoState, setState, a);
            setState(mapIndexed((i, e_1) => {
                let bind$0040;
                if (i === a) {
                    return new Extensions_Annotation(e_1.IsOpen, (bind$0040 = e_1.Search, new Extensions_SearchComponent(bind$0040.Key, headerType, bind$0040.Body)));
                }
                else {
                    return e_1;
                }
            }, annoState));
        },
        onKeyDown: (k) => {
            if (~~k.which === 13) {
                DropdownElements_selectCompositeHeaderDiscriminate(headerType, setUiState, close, annoState, setState, a);
            }
        },
        children: toString(headerType),
        onBlur: (e_2) => {
            close();
        },
    }));
    return createElement("li", {
        children: reactApi.Children.toArray(Array.from(children)),
    });
}

function DropdownElements_dropdownContentMain(state, setState, close, annoState, setAnnoState, a) {
    const xs = [DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(3, [])), DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(2, [])), DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(1, [])), DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(0, [])), DropdownElements_createSubBuildingBlockDropdownLink(state, setState, new DropdownPage_1(1, [])), DropdownElements_DropdownContentInfoFooter(setState, false)];
    return react.createElement(react.Fragment, {}, ...xs);
}

function DropdownElements_dropdownContentProtocolTypeColumns(setState, close, annoState, setAnnoState, a) {
    const xs = [DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(10, [])), DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(9, [])), DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(5, [])), DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(4, [])), DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(6, [])), DropdownElements_createBuildingBlockDropdownItem(setState, close, annoState, setAnnoState, a, new CompositeHeaderDiscriminate(7, [])), DropdownElements_DropdownContentInfoFooter(setState, true)];
    return react.createElement(react.Fragment, {}, ...xs);
}

/**
 * Output columns subpage for dropdown
 */
export function Main(mainInputProps) {
    let elems;
    const a = mainInputProps.a;
    const setAnnoState = mainInputProps.setAnnoState;
    const annoState = mainInputProps.annoState;
    const setState = mainInputProps.setState;
    const state = mainInputProps.state;
    const patternInput = reactApi.useState(false);
    const setOpen = patternInput[1];
    const isOpen = patternInput[0];
    const close = (_arg) => {
        setOpen(false);
    };
    const dropdownRef = reactApi.useRef(undefined);
    reactApi.useEffect(() => {
        const handleClickOutside = (event) => {
            let element;
            const matchValue = dropdownRef.current;
            let matchResult, element_1;
            if (matchValue != null) {
                if ((element = matchValue, !element.contains(event.target))) {
                    matchResult = 0;
                    element_1 = matchValue;
                }
                else {
                    matchResult = 1;
                }
            }
            else {
                matchResult = 1;
            }
            switch (matchResult) {
                case 0: {
                    close();
                    break;
                }
                case 1: {
                    break;
                }
            }
        };
        document.addEventListener("click", handleClickOutside);
    });
    return createElement(BaseDropdown_Main_427AC1C3, {
        isOpen: isOpen,
        setIsOpen: setOpen,
        toggle: createElement("div", createObj(Helpers_combineClasses("btn", ofArray([["tabIndex", 0], ["className", "btn-info"], ["onClick", (_arg_1) => {
            setOpen(!isOpen);
        }], ["role", join(" ", ["button"])], ["className", "join-item"], ["className", "flex-nowrap"], (elems = [createElement("span", {
            children: [toString(item(a, annoState).Search.KeyType)],
        }), createElement("i", {
            className: "fa-solid fa-angle-down",
        })], ["children", reactApi.Children.toArray(Array.from(elems))]), ["ref", dropdownRef]])))),
        children: toList(delay(() => {
            const matchValue_1 = state.DropdownPage;
            switch (matchValue_1.tag) {
                case 1:
                    return singleton_1(DropdownElements_dropdownContentProtocolTypeColumns(setState, close, annoState, setAnnoState, a));
                case 2: {
                    const iotype = matchValue_1.fields[0];
                    return singleton_1(defaultOf());
                }
                default:
                    return singleton_1(DropdownElements_dropdownContentMain(state, setState, close, annoState, setAnnoState, a));
            }
        })),
        style: Style_init_Z501027E3("join-item dropdown text-white", ofSeq([["content", Style_init_Z501027E3("!min-w-64")]], {
            Compare: comparePrimitives,
        })),
    });
}

