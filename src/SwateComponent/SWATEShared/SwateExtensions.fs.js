import { min } from "../fable_modules/fable-library-js.4.24.0/Double.js";
import { take } from "../fable_modules/fable-library-js.4.24.0/Array.js";
import { some } from "../fable_modules/fable-library-js.4.24.0/Option.js";

/**
 * Take "count" many items from array if existing. if not enough items return as many as possible
 */
export function Array_takeSafe(count, array) {
    const count_1 = min(count, array.length) | 0;
    return take(count_1, array);
}

/**
 * If function returns `true` then return `Some x` otherwise `None`
 */
export function Option_where(f, x) {
    if (f(x)) {
        return some(x);
    }
    else {
        return undefined;
    }
}

/**
 * If function return `true` then return `None` otherwise `Some x`
 */
export function Option_whereNot(f, x) {
    if (f(x)) {
        return undefined;
    }
    else {
        return some(x);
    }
}

