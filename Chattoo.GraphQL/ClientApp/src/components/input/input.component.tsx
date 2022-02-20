import { InputBase, InputProps, Stack, SxProps } from '@mui/material';
import React from 'react';

let stackSx: SxProps = {
    backgroundColor: "#f1f1f1",
    display: "flex",
    borderRadius: "5px",
    p: 0.5,
    alignItems: "center",
};

const inputSx: SxProps = {
    borderRadius: "5px",
    flexGrow: 1
};

interface CustomInputProps extends InputProps {
    icon?: any,
    full?: boolean
}

/** Komponenta - textové pole pro zadání vstupu */
const CustomInput = React.forwardRef((props: CustomInputProps, ref: React.ForwardedRef<HTMLDivElement>) => {
    const { icon, full, ...inputProps } = props;

    stackSx = { ...stackSx, flexGrow: full ? 1 : 0 };

    return (
        <Stack direction="row" sx={stackSx}>
            <InputBase {...inputProps} ref={ref} sx={inputSx} />
            {icon && icon}
        </Stack>
    );
});

CustomInput.displayName = "CustomInputComponent";
export default CustomInput;