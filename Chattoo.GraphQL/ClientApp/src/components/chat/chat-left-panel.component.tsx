import { FC, useCallback, useState } from 'react'
import CommunicationChannelList from './communication-channel-list.component';
import CommunicationChannelCreatePopup from '../channel/communication-channel-create-popup.component';
import { IconButton, Paper, Stack, Typography } from '@mui/material';
import AddCircleIcon from '@mui/icons-material/AddCircle';

/**
 * Komponenta - levá část komponenty chatu.
 */
const ChatLeftPanel: FC = () => {
    const [showCreateCommunicationChannelPopup, setShowCreateCommunicationChannelPopup] = useState<boolean>();

    /** Obstará otevření dialogu pro vytvoření komunikačního kanálu. */
    const handleCreateCommunicationChannelDialogOpen = useCallback(() => {
        setShowCreateCommunicationChannelPopup(true);
    }, [setShowCreateCommunicationChannelPopup]);

    /** Obstará uvazření dialogu pro vytvoření komunikačního kanálu. */
    const handleCreateCommunicationChannelDialogClose = useCallback(() => {
        setShowCreateCommunicationChannelPopup(false);
    }, [setShowCreateCommunicationChannelPopup]);

    return (
        <Paper sx={{ p: 1, height: "80vh", display: "flex" }}>
            {/* Dialog pro vytvoření kanálu */}
            <CommunicationChannelCreatePopup onClose={handleCreateCommunicationChannelDialogClose} open={showCreateCommunicationChannelPopup} />
            <Stack sx={{ flexGrow: 1 }}>
                <Stack direction="row" justifyContent="space-between">
                    {/* Nadpis */}
                    <Typography variant="h5">Chaty</Typography>
                    {/* Ikona pro otevření dialogu na vytvoření kanálu */}
                    <IconButton color="primary" onClick={handleCreateCommunicationChannelDialogOpen}>
                        <AddCircleIcon />
                    </IconButton>
                </Stack>
                {/* Seznam kanálů */}
                <CommunicationChannelList />
            </Stack>
        </Paper>
    );
}

ChatLeftPanel.displayName = "ChatLeftPanelComponent";
export default ChatLeftPanel;