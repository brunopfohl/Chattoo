import React from 'react'
import styled from 'styled-components';
import ChatLeftPanel from './chat-left-panel.component';
import ChatRightPanel from './chat-right-panel.component';

const Container = styled.div`
    border-radius: 20px;
    display: flex;
    height: 80vh;
    overflow: hidden;
`;

const Chat: React.FC<any> = (props: any) => {

    return (
        <Container>
            <ChatLeftPanel></ChatLeftPanel>
            <ChatRightPanel></ChatRightPanel>
        </Container>
    );
}

export default Chat;