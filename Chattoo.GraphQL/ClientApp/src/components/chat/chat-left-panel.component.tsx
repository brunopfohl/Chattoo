import { FC, useCallback, useState } from 'react'
import CommunicationChannelList from './communication-channel-list.component';
import CommunicationChannelCreatePopup from '../channel/communication-channel-create-popup.component';
import { IconButton, InputBase, Paper, Stack, Typography } from '@mui/material';
import AddCircleIcon from '@mui/icons-material/AddCircle';
import { Search } from '@mui/icons-material';
import CustomInput from '@components/input/input.component';

/**
 * Komponenta - levá část komponenty chatu.
 */
const ChatLeftPanel: FC = () => {
    const [showCreateCommunicationChannelPopup, setShowCreateCommunicationChannelPopup] = useState<boolean>(false);

    /** Obstará otevření dialogu pro vytvoření komunikačního kanálu. */
    const handleCreateCommunicationChannelDialogOpen = useCallback(() => {
        setShowCreateCommunicationChannelPopup(true);
    }, [setShowCreateCommunicationChannelPopup]);

    /** Obstará uvazření dialogu pro vytvoření komunikačního kanálu. */
    const handleCreateCommunicationChannelDialogClose = useCallback(() => {
        setShowCreateCommunicationChannelPopup(false);
    }, [setShowCreateCommunicationChannelPopup]);

    return (
        <Paper sx={{ p: 2, height: "100%", display: "flex" }}>
            {/* Dialog pro vytvoření kanálu */}
            <CommunicationChannelCreatePopup onClose={handleCreateCommunicationChannelDialogClose} open={showCreateCommunicationChannelPopup} />
            <Stack sx={{ flexGrow: 1 }}>
                <Stack direction="row" justifyContent="space-between" sx={{ mb: 1 }}>
                    {/* Nadpis */}
                    <Typography variant="h5">Chaty</Typography>

                    {/* Ikona pro otevření dialogu na vytvoření kanálu */}
                    <IconButton color="primary" onClick={handleCreateCommunicationChannelDialogOpen}>
                        <AddCircleIcon />
                    </IconButton>
                </Stack>

                <CustomInput placeholder="Zadejte hledaný výraz" icon={<Search />} />

                {/* Seznam kanálů */}
                <CommunicationChannelList />
            </Stack>
        </Paper>
    );
}

ChatLeftPanel.displayName = "ChatLeftPanelComponent";
export default ChatLeftPanel;