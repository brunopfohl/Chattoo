import { Avatar, ListItemAvatar, ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import { CommunicationChannel } from 'graphql/graphql-types';
import { FC, useCallback, useContext } from 'react'
import { ChatStateContext } from './chat-state-provider.component';
import PeopleAltIcon from '@mui/icons-material/PeopleAlt';

/** Parametry komponenty náhledu komunikačního kanálu */
interface CommunicationChannelPreviewProps {
    /** Objekt reprezentující komunikanční kanál */
    channel: CommunicationChannel,
};

/**
 * Komponenta s náhledem komunikačního kanálu.
 * @param props Parametry pro render komponenty.
 */
const CommunicationChannelPreview: FC<CommunicationChannelPreviewProps> = (props) => {
    const { channel } = props;
    const { setCurrentChannel } = useContext(ChatStateContext);

    /** Callback volaný po zvolení komunikačního kanálu */
    const onClickHandler = useCallback(() => {
        setCurrentChannel(channel);
    }, [setCurrentChannel, channel]);

    return (
        // Klikatelný prvek pro zvolení komunikačního kanálu
        <ListItemButton onClick={onClickHandler} sx={{ pl: 1 }}>
            {/* Ikona kanálu */}
            <ListItemAvatar>
                <Avatar>
                    <PeopleAltIcon />
                </Avatar>
            </ListItemAvatar>
            {/* Popis kanálu */}
            <ListItemText primary={channel.name} secondary={channel.description} sx={{ wordBreak: "break-word" }} />
        </ListItemButton>
    );
}

CommunicationChannelPreview.displayName = "CommunicationChannelPreviewComponent";
export default CommunicationChannelPreview;