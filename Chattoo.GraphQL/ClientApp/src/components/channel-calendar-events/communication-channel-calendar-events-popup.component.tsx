import React, { useContext } from 'react'
import styled from 'styled-components';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import Popup from '../popup/popup.component';

interface CommunicationChannelCalendarEventsProps {
    onClose: () => void
}

const Container = styled.div`
    color: white;
    display: flex;
    flex-direction: column;
    background-color: #545454;
    width: 30vw;
    min-width: 400px;
    padding: 1em;
    overflow: hidden;
`;

const CommunicationChannelCalendarEvents: React.FC<CommunicationChannelCalendarEventsProps> = (props: CommunicationChannelCalendarEventsProps) => {
    const { currentChannel } = useContext(ChatStateContext);

    return (
        <>
            <Container>
            </Container>
        </>
    );
}

const CommunicationChannelCalendarEventsPopup: React.FC<CommunicationChannelCalendarEventsProps> = (props: CommunicationChannelCalendarEventsProps) => {
    const { onClose } = props;

    return (
        <Popup title="Kalendářní akce" onClose={onClose}>
            <CommunicationChannelCalendarEvents {...props} />
        </Popup>
    );
}

export default CommunicationChannelCalendarEventsPopup;