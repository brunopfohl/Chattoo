import React, { useState } from 'react'
import CommunicationChannelList from './communication-channel-list.component';
import CommunicationChannelCreatePopup from '../channel/communication-channel-create-popup.component';
import { IconButton, Paper, Stack, Typography } from '@mui/material';
import AddCircleIcon from '@mui/icons-material/AddCircle';

const ChatLeftPanel: React.FC<any> = (props: any) => {
    const [showCreateCommunicationChannelPopup, setShowCreateCommunicationChannelPopup] = useState<boolean>();

    return (
        <Paper sx={{p: 1}}>
            {showCreateCommunicationChannelPopup && <CommunicationChannelCreatePopup onClose={ () => { setShowCreateCommunicationChannelPopup(false) } }/>}
            <Stack direction="row" justifyContent="space-between">
                <Typography variant="h5">Chaty</Typography>
                <IconButton color="primary" onClick={() => { setShowCreateCommunicationChannelPopup(true); }}>
                    <AddCircleIcon />
                </IconButton>
            </Stack>
            <CommunicationChannelList />
        </Paper>
    );
}

export default ChatLeftPanel;