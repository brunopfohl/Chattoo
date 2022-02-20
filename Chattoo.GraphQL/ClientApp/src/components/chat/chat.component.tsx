import { Grid } from '@mui/material';
import { FC } from 'react';
import ChatLeftPanel from './chat-left-panel.component';
import ChatRightPanel from './chat-right-panel.component';
import ChatStateProvider from './chat-state-provider.component';

/**
 * Komponenta - chat.
 */
const Chat: FC = () => (
    // Komponenta má přístup k globálnímu datovému kontextu ChatStateContext
    <ChatStateProvider>
        <Grid spacing={1} container sx={{ height: "100%", p: 1, pt: 2 }}>
            {/* Levá strana */}
            <Grid item md={3}>
                <ChatLeftPanel />
            </Grid>
            {/* Pravá strana */}
            <Grid item md={9}>
                <ChatRightPanel />
            </Grid>
        </Grid>
    </ChatStateProvider>
);

Chat.displayName = "ChatComponent";
export default Chat;