import React, { useState } from 'react'
import styled from 'styled-components';
import { Plus } from 'styled-icons/boxicons-regular';
import Button from '../button/button.component';
import CommunicationChannelList from './communication-channel-list.component';
import CommunicationChannelCreatePopup from '../channel/communication-channel-create-popup.component';

const Container = styled.div`
    min-width: 150px;
    background-color: #545454;
    flex-grow: 1;
    display: flex;
    flex-direction: column;
`;

const Heading = styled.div`
    padding-left: 1em;
    display: flex;
    align-items: center;
    flex-direction: row;
    color: white;
`;

const Title = styled.h2`
    margin-right: 0.5em;
`;

const ChatLeftPanel: React.FC<any> = (props: any) => {
    const [showCreateCommunicationChannelPopup, setShowCreateCommunicationChannelPopup] = useState<boolean>();

    return (
        <>
            {showCreateCommunicationChannelPopup && <CommunicationChannelCreatePopup onClose={ () => { setShowCreateCommunicationChannelPopup(false) } }/>}
            <Container>
                <Heading>
                    <Title>Chaty</Title>
                    <Button onClick={() => { setShowCreateCommunicationChannelPopup(true); }} icon={Plus}/>
                </Heading>
                <CommunicationChannelList />
            </Container>
        </>
    );
}

export default ChatLeftPanel;