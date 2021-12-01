import { Grid } from '@mui/material';
import React from 'react'
import ChatLeftPanel from './chat-left-panel.component';
import ChatRightPanel from './chat-right-panel.component';
import ChatStateProvider from './chat-state-provider.component';

const Chat: React.FC<any> = () => {
    return (
        <ChatStateProvider>
            <Grid container spacing={1}>
                <Grid item md={3}>
                    <ChatLeftPanel />
                </Grid>
                <Grid item md={9}>
                    <ChatRightPanel />
                </Grid>
            </Grid>
        </ChatStateProvider>
    );
}

export default Chat;