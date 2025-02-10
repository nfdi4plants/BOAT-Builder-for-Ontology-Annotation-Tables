import { class_type } from "../fable_modules/fable-library-js.4.24.0/Reflection.js";

export class ClickOutsideHandler {
    constructor() {
    }
}

export function ClickOutsideHandler_$reflection() {
    return class_type("Components.ClickOutsideHandler", undefined, ClickOutsideHandler);
}

export function ClickOutsideHandler_AddListenerString_378D00DF(containerId, clickedOutsideEvent) {
    const closeEvent = (e) => {
        const rmv = () => {
            document.removeEventListener("click", closeEvent);
        };
        const dropdown = document.getElementById(containerId);
        if (dropdown == null) {
            rmv();
        }
        else {
            const isClickedInsideDropdown = dropdown.contains(e.target);
            if (!isClickedInsideDropdown) {
                clickedOutsideEvent(e);
                rmv();
            }
        }
    };
    document.addEventListener("click", closeEvent);
}

export function ClickOutsideHandler_AddListenerElement_333502F(element, clickedOutsideEvent) {
    const closeEvent = (e) => {
        const rmv = () => {
            document.removeEventListener("click", closeEvent);
        };
        const dropdown = element.current;
        if (dropdown == null) {
            rmv();
        }
        else {
            const isClickedInsideDropdown = dropdown.contains(e.target);
            if (!isClickedInsideDropdown) {
                clickedOutsideEvent(e);
                rmv();
            }
        }
    };
    document.addEventListener("click", closeEvent);
}

