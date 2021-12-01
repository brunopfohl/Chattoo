import CustomInput from '@components/input/input.component';
import { Box, IconButton, Stack } from '@mui/material';
import React, { useState } from 'react'
import SendIcon from '@mui/icons-material/Send';

interface MessageBoxProps {
    callback: Function;
}

const MessageBox: React.FC<any> = (props: MessageBoxProps) => {
    const [text, setText] = useState("");

    const onSubmit = (ev: any) => {
        ev.preventDefault();

        if(text) {
            setText("");
            props.callback(text);
        }
    };

    return (
        <div>
            <form onSubmit={onSubmit}>
                <Stack direction="row">
                    <CustomInput placeholder="Zadejte zprávu..." value={text} onChange={ev => setText(ev.target.value)} size="small"/>
                    {text.length > 0 &&
                        <IconButton color="primary" onClick={onSubmit}>
                            <SendIcon />
                        </IconButton>
                    }
                </Stack>
            </form>
        </div>
    );
}

export default MessageBox;