import { useState } from "react";
import { useDebounce } from "./useDebounceHook";
import { useInputOnChange } from "./useInputOnChange";

export const useTextInputValue = (initialValue: string, debounceInMiliseconds: number = 0) => {
    const [value, setValue] = useState<string>(initialValue);

    const debouncedValue = debounceInMiliseconds > 0
        ? useDebounce(value, debounceInMiliseconds)
        : value;

    const inputOnChange = useInputOnChange(setValue);

    return [value, setValue, debouncedValue, inputOnChange];
}