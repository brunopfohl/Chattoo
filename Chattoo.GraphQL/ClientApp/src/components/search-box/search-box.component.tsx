import { useDebounce } from '@hooks/useDebounceHook';
import { TextField } from '@mui/material';
import { FC, useCallback, useEffect, useState } from 'react'

/** Parametry komponenty pro vyhledávání */
export interface SearchBoxProps {
    /** Hodnota vyhledávacího pole */
    text: string;
    /** Nápověda ve vyhledávacím poli */
    placeholder: string;
    /** Callback vyvolaný po změně hodnoty vyhledávacího pole */
    onValueChanged: Function;
    /** Počet milisekund, jak dlouho má vyhledávací pole pozdržet vyvolání callbacku (doba čekání na změnu hodnoty pole). */
    onValueChangedTimeout: number;
}

/**
 * Komponenta - textové pole pro vyhledávání.
 */
const SearchBox: FC<SearchBoxProps> = (props) => {
    const { text, placeholder, onValueChanged, onValueChangedTimeout } = props;

    const [searchTerm, setSearchTerm] = useState("");

    const debouncedSearchTerm = useDebounce(searchTerm, onValueChangedTimeout);

    /** Callback vyvolaný po změně hodnoty vyhledávacího pole */
    const handleOnChange = useCallback((event: React.ChangeEvent<HTMLInputElement>) => {
        let value = event.target.value;
        setSearchTerm(value);
    }, [setSearchTerm]);

    useEffect(() => {
        if (onValueChanged && debouncedSearchTerm !== text) {
            onValueChanged(searchTerm);
        }
    }, [debouncedSearchTerm]);

    return (
        <TextField size="small" fullWidth value={searchTerm} onChange={handleOnChange} placeholder={placeholder} />
    );
}

SearchBox.displayName = "SearchBoxComponent";
export default SearchBox;