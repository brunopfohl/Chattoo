import { useCallback } from "react";

export const useSetter = (set: (value: any) => void, value: any) => {
    return useCallback(() => {
        set(value);
    }, [set]);
}