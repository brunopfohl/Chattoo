import React from 'react'
import styled from 'styled-components';
import CommunicationChannel from './communication-channel.component';

const Container = styled.div`
    display: flex;
    flex-direction: column;
    flex-grow: 3;
    position: relative;

    &::before {
        content: "";
        background-color: #69747C;
        filter: brightness(150%);
        background-size: cover;
        position: absolute;
        top: 0px;
        right: 0px;
        bottom: 0px;
        left: 0px;
        opacity: 0.75;
    }
`;

const ChatRightPanel: React.FC<any> = (props: any) => {

    return (
        <Container>
            <CommunicationChannel/>
        </Container>
    );
}

export default ChatRightPanel;