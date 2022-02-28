import { useCallback } from "react";

export const useInputOnChange = (set: (value: any) => void, type: "numeric" | "text" = "text") => {
    return useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
        switch (type) {
            case "text":
                set(event.target.value);
                break;
            case "numeric":
                set(!!event.target.value ? parseInt(event.target.value) : undefined);
                break;
        }
    }, [set]);
}