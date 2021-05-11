import React, { useContext } from 'react'
import styled from 'styled-components';
import CommunicationChannel from '../channel/communication-channel.component';
import { ChatStateContext } from './chat-state-provider.component';

const Container = styled.div`
    background-color: rgba(105, 116, 124, 0.6);
    display: flex;
    flex-direction: column;
    flex-grow: 3;
`;

const ChatRightPanel: React.FC = () => {
    const { currentChannel } = useContext(ChatStateContext);

    return (
        <Container>
            {currentChannel
                ? <CommunicationChannel/>
                : <h4>Nebyl zvolen komunikační kanál.</h4>
            }
        </Container>
    );
}

export default ChatRightPanel;