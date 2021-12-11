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
        <Grid container spacing={1} mx={{ height: "80vh" }}>
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