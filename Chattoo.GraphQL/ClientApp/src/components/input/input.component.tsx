import { SxProps, TextField, TextFieldProps } from '@mui/material';
import React from 'react';

const sx: SxProps = {
    borderRadius: 30
};

const CustomInput = React.forwardRef((props: TextFieldProps, ref: React.ForwardedRef<HTMLDivElement>) => {
    return (
        <TextField {...props} ref={ref} sx={sx}/>
    );
});

export default CustomInput;