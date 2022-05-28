import CustomInput from '@components/input/input.component';
import { Box, IconButton, Stack } from '@mui/material';
import { FC, useCallback, useState } from 'react'
import SendIcon from '@mui/icons-material/Send';
import { ThumbUp } from '@mui/icons-material';

interface MessageBoxProps {
    callback: Function;
}

const MessageBox: FC<MessageBoxProps> = (props) => {
    const { callback } = props;
    const [text, setText] = useState<string>("");

    const handleInputOnChange = useCallback((ev: any) => {
        setText(ev.target.value);
    }, [setText]);

    const handleOnSubmit = useCallback((ev: any) => {
        ev.preventDefault();

        callback(text || "👍");
        setText("");
    }, [props.callback, text]);

    return (
        <Box>
            <form onSubmit={handleOnSubmit}>
                <Stack direction="row">
                    <CustomInput placeholder="Zadejte zprávu..." value={text} onChange={handleInputOnChange} full={true} />
                    <IconButton color="primary" onClick={handleOnSubmit}>
                        {text && text.length > 0 ? <SendIcon /> : <ThumbUp />}
                    </IconButton>
                </Stack>
            </form>
        </Box>
    );
}

MessageBox.displayName = "MessageBoxComponent";
export default MessageBox;