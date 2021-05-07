import React, { useContext } from 'react'
import styled from 'styled-components'
import { useGetUsersForChannel } from '../../hooks/users/queries/useGetUsersForChannel';
import Button from '../button/button.component';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import Popup from '../popup/popup.component';
import Separator from '../separator.component';

interface CommunicationChannelSettingsProps {
    onClose: () => void
}

const Container = styled.div`
    color: white;
    display: flex;
    flex-direction: column;
    background-color: #545454;
    width: 30vw;
    padding: 1em;
    overflow: hidden;
`;

const CommunicationChannelSettings: React.FC<CommunicationChannelSettingsProps> = (props: CommunicationChannelSettingsProps) => {
    const { currentChannel } = useContext(ChatStateContext);

    const [channelUsers, channelUsersQuery] = useGetUsersForChannel({ channelId: currentChannel?.id});

    return (
        <Container>
            <h3>Seznam účastníků</h3>
            {channelUsers?.data && channelUsers.data.map(user => (
                <span>{user.userName}</span>
            ))}
            <Button text="Přidat" onClick={() => {}}/>
            <Separator />
        </Container>
    );
}

const CommunicationChannelSettingsPopup: React.FC<CommunicationChannelSettingsProps> = (props: CommunicationChannelSettingsProps) => {
    const { onClose } = props;

    return (
        <Popup title="Vytvořit skupinu" onClose={onClose}>
            <CommunicationChannelSettings {...props} />
        </Popup>
    );
}

export default CommunicationChannelSettingsPopup;