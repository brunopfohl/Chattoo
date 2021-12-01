import { Paper } from '@mui/material';
import React, { useContext } from 'react'
import CommunicationChannel from '../channel/communication-channel.component';
import { ChatStateContext } from './chat-state-provider.component';

const ChatRightPanel: React.FC = () => {
    const { currentChannel } = useContext(ChatStateContext);

    return (
        <Paper>
            {currentChannel
                ? <CommunicationChannel/>
                : <h4>Nebyl zvolen komunikační kanál.</h4>
            }
        </Paper>
    );
}

export default ChatRightPanel;