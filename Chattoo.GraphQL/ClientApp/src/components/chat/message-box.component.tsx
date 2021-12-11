import CustomInput from '@components/input/input.component';
import { Box, IconButton, Stack } from '@mui/material';
import { FC, useCallback, useState } from 'react'
import SendIcon from '@mui/icons-material/Send';
import { ThumbUp } from '@mui/icons-material';

/** Parametry pro vykreslení komponenty s textovým polem pro odeslání zprávy */
interface MessageBoxProps {
    callback: Function;
}

/**
 * Komponenta - pole pro odeslání zprávy do komunikačního kanálu.
 */
const MessageBox: FC<MessageBoxProps> = (props) => {
    const { callback } = props;
    const [text, setText] = useState<string | null>(null);

    /** Callback vyvolaný po změne textu v textovém poli */
    const handleInputOnChange = useCallback((ev: any) => {
        setText(ev.target.value);
    }, [setText]);

    /** Callback vyvolaný po odeslání zprávy */
    const handleOnSubmit = useCallback((ev: any) => {
        ev.preventDefault();

        if (text) {
            setText("");
            callback(text);
        }
        else {
            callback("👍");
        }
    }, [props.callback, text]);

    return (
        <Box>
            <form onSubmit={handleOnSubmit}>
                <Stack direction="row">
                    <CustomInput placeholder="Zadejte zprávu..." value={text} onChange={handleInputOnChange} size="small" />
                    <IconButton color="primary" onClick={handleOnSubmit}>
                        {text && text.length > 0
                            ? <SendIcon />
                            : <ThumbUp />
                        }
                    </IconButton>
                </Stack>
            </form>
        </Box>
    );
}

MessageBox.displayName = "MessageBoxComponent";
export default MessageBox;