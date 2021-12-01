import { ListItemButton, ListItemIcon, ListItemText } from '@mui/material';
import { CommunicationChannel } from 'graphql/graphql-types';
import React, { useContext } from 'react'
import { ChatStateContext } from './chat-state-provider.component';
import PeopleAltIcon from '@mui/icons-material/PeopleAlt';

interface CommunicationChannelPreviewProps {
    channel: CommunicationChannel,
};

const CommunicationChannelPreview: React.FC<any> = (props: CommunicationChannelPreviewProps) => {
    const { channel } = props;
    const { setCurrentChannel } = useContext(ChatStateContext);

    // Po kliknutí zvolím tento komunikační kanál.
    const onClickHandler = () => {
        setCurrentChannel(channel);
    }

    return (
        <ListItemButton onClick={onClickHandler}>
            <ListItemIcon>
                <PeopleAltIcon />
            </ListItemIcon>
            <ListItemText primary={channel.name} secondary={channel.description} />
        </ListItemButton>
    );
}

export default CommunicationChannelPreview;