import { SxProps, TextField, TextFieldProps } from '@mui/material';
import React from 'react';

const sx: SxProps = {
    borderRadius: 30,
    flexGrow: 1
};

/** Komponenta - textové pole pro zadání vstupu */
const CustomInput = React.forwardRef((props: TextFieldProps, ref: React.ForwardedRef<HTMLDivElement>) => (
    <TextField {...props} ref={ref} sx={sx} />
));

CustomInput.displayName = "CustomInputComponent";
export default CustomInput;