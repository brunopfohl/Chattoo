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
            <Grid item md={3} sx={{ flexGrow: 1, height: { xs: "30%", sm: "auto" } }}>
                <ChatLeftPanel />
            </Grid>
            {/* Pravá strana */}
            <Grid item md={9} sx={{ flexGrow: 1, height: { xs: "70%", sm: "auto" } }}>
                <ChatRightPanel />
            </Grid>
        </Grid>
    </ChatStateProvider>
);

Chat.displayName = "ChatComponent";
export default Chat;