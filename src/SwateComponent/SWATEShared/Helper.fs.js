import { value as value_2, some } from "../fable_modules/fable-library-js.4.24.0/Option.js";
import { toText } from "../fable_modules/fable-library-js.4.24.0/String.js";
import { Dictionary } from "../fable_modules/fable-library-js.4.24.0/MutableMap.js";
import { HashIdentity_Structural } from "../fable_modules/fable-library-js.4.24.0/FSharp.Collections.js";
import { class_type } from "../fable_modules/fable-library-js.4.24.0/Reflection.js";
import { tryGetValue } from "../fable_modules/fable-library-js.4.24.0/MapUtil.js";
import { FSharpRef } from "../fable_modules/fable-library-js.4.24.0/Types.js";
import { defaultOf, disposeSafe, getEnumerator } from "../fable_modules/fable-library-js.4.24.0/Util.js";
import { op_Subtraction, utcNow, minValue } from "../fable_modules/fable-library-js.4.24.0/Date.js";
import { CompositeHeaderDiscriminate__HasIOType, CompositeHeaderDiscriminate__IsTermColumn } from "./ARCtrl.Helper.fs.js";

export function log(a) {
    console.log(some(a));
}

export function logw(a) {
    console.warn(some(a));
}

export function logf(a, b) {
    const txt = toText(a)(b);
    log(txt);
}

export class DebounceStorage {
    constructor() {
        this._storage = (new Dictionary([], HashIdentity_Structural()));
        this._fnStorage = (new Map([]));
    }
}

export function DebounceStorage_$reflection() {
    return class_type("Helper.DebounceStorage", undefined, DebounceStorage);
}

export function DebounceStorage_$ctor() {
    return new DebounceStorage();
}

export function DebounceStorage__Add_675329D2(this$, key, timeoutId, fn) {
    this$._storage.set(key, timeoutId);
    if (fn != null) {
        this$._fnStorage.set(key, value_2(fn));
    }
}

export function DebounceStorage__TryGetValue_Z721C83C5(this$, key) {
    let matchValue;
    let outArg = 0;
    matchValue = [tryGetValue(this$._storage, key, new FSharpRef(() => outArg, (v) => {
        outArg = (v | 0);
    })), outArg];
    if (matchValue[0]) {
        const timeoutId = matchValue[1] | 0;
        return timeoutId;
    }
    else {
        return undefined;
    }
}

export function DebounceStorage__Remove_Z721C83C5(this$, key) {
    this$._storage.delete(key);
    this$._fnStorage.delete(key);
}

export function DebounceStorage__ClearAndRun(this$) {
    let enumerator = getEnumerator(this$._storage);
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            const kv = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            clearTimeout(kv[1]);
            let matchValue;
            let outArg = defaultOf();
            matchValue = [tryGetValue(this$._fnStorage, kv[0], new FSharpRef(() => outArg, (v) => {
                outArg = v;
            })), outArg];
            if (matchValue[0]) {
                const fn = matchValue[1];
                fn();
            }
        }
    }
    finally {
        disposeSafe(enumerator);
    }
    this$._storage.clear();
    this$._fnStorage.clear();
}

export function DebounceStorage__Clear(this$) {
    let enumerator = getEnumerator(this$._storage);
    try {
        while (enumerator["System.Collections.IEnumerator.MoveNext"]()) {
            const kv = enumerator["System.Collections.Generic.IEnumerator`1.get_Current"]();
            clearTimeout(kv[1]);
        }
    }
    finally {
        disposeSafe(enumerator);
    }
    this$._storage.clear();
    this$._fnStorage.clear();
}

export function debounce(storage, key, timeout, fn, value) {
    const key_1 = key;
    const matchValue = DebounceStorage__TryGetValue_Z721C83C5(storage, key_1);
    if (matchValue != null) {
        const timeoutId = matchValue | 0;
        clearTimeout(timeoutId);
    }
    const timeoutId_1 = setTimeout(() => {
        const value_1 = DebounceStorage__Remove_Z721C83C5(storage, key_1);
        fn(value);
    }, timeout) | 0;
    DebounceStorage__Add_675329D2(storage, key_1, timeoutId_1, () => {
        fn(value);
    });
}

export function debouncel(storage, key, timeout, setLoading, fn, value) {
    const key_1 = key;
    const matchValue = DebounceStorage__TryGetValue_Z721C83C5(storage, key_1);
    if (matchValue != null) {
        const timeoutId = matchValue | 0;
        clearTimeout(timeoutId);
    }
    else {
        setLoading(true);
    }
    const timeoutId_1 = setTimeout(() => {
        const matchValue_1 = DebounceStorage__TryGetValue_Z721C83C5(storage, key_1);
        if (matchValue_1 == null) {
            setLoading(false);
        }
        else {
            const value_1 = DebounceStorage__Remove_Z721C83C5(storage, key_1);
            setLoading(false);
            fn(value);
        }
    }, timeout) | 0;
    DebounceStorage__Add_675329D2(storage, key_1, timeoutId_1, () => {
        fn(value);
    });
}

export function newDebounceStorage() {
    return DebounceStorage_$ctor();
}

export function throttle(fn, interval) {
    let lastCall = minValue();
    return (arg) => {
        const now = utcNow();
        if (op_Subtraction(now, lastCall) > interval) {
            lastCall = now;
            fn(arg);
        }
    };
}

export function throttleAndDebounce(fn, timespan) {
    let id = undefined;
    let lastCall = minValue();
    return (arg) => {
        const now = utcNow();
        const isThrottled = op_Subtraction(now, lastCall) > timespan;
        const id_1 = id;
        let matchResult;
        if (isThrottled) {
            if (id_1 == null) {
                matchResult = 2;
            }
            else {
                matchResult = 0;
            }
        }
        else if (id_1 == null) {
            matchResult = 2;
        }
        else {
            matchResult = 1;
        }
        switch (matchResult) {
            case 0: {
                const id_2 = id_1 | 0;
                clearTimeout(id_2);
                lastCall = now;
                fn(arg);
                break;
            }
            case 1: {
                const id_3 = id_1 | 0;
                clearTimeout(id_3);
                break;
            }
        }
        const timeoutId = setTimeout(() => {
            fn(arg);
            id = undefined;
            lastCall = now;
        }, timespan) | 0;
        id = timeoutId;
    };
}

export function isSameMajorCompositeHeaderDiscriminate(hct1, hct2) {
    if (CompositeHeaderDiscriminate__IsTermColumn(hct1) === CompositeHeaderDiscriminate__IsTermColumn(hct2)) {
        return CompositeHeaderDiscriminate__HasIOType(hct1) === CompositeHeaderDiscriminate__HasIOType(hct2);
    }
    else {
        return false;
    }
}

export function isValidColumn(header) {
    if (header.IsFeaturedColumn ? true : (header.IsTermColumn && (header.ToTerm().NameText.length > 0))) {
        return true;
    }
    else {
        return header.IsSingleColumn;
    }
}

