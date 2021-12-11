import { Paper } from '@mui/material';
import { FC, useContext } from 'react'
import CommunicationChannelMemo from '../channel/communication-channel.component';
import { ChatStateContext } from './chat-state-provider.component';

/**
 * Komponenta - pravá strany komponenty chatu.
 */
const ChatRightPanel: FC = () => {
    const { currentChannel } = useContext(ChatStateContext);

    return (
        // Komponenta komunikačního kanálu je obalena Paperem
        <Paper sx={{ p: 1, height: "100%" }}>
            {currentChannel
                ? <CommunicationChannelMemo />
                : <h4>Nebyl zvolen komunikační kanál.</h4>
            }
        </Paper>
    );
}

ChatRightPanel.displayName = "ChatRightPanelComponent";
export default ChatRightPanel;