import React from 'react'
import styled from 'styled-components';
import { MemoizedCommunicationChannel } from '../channel/communication-channel.component';

const Container = styled.div`
    background-color: rgba(105, 116, 124, 0.6);
    display: flex;
    flex-direction: column;
    flex-grow: 3;
`;

const ChatRightPanel: React.FC<any> = (props: any) => {

    return (
        <Container>
            <MemoizedCommunicationChannel/>
        </Container>
    );
}

export default ChatRightPanel;