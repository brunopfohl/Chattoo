import React from 'react'
import styled from 'styled-components';
import ChatLeftPanel from './chat-left-panel.component';
import ChatRightPanel from './chat-right-panel.component';
import ChatStateProvider from './chat-state-provider.component';

interface ChatProps {
    
};

const Container = styled.div`
    min-width: 700px;
    border-radius: 20px;
    display: flex;
    height: 80vh;
    overflow: hidden;
`;

interface ChatBodyProps {
    currentChannelId: string;
}

const Chat: React.FC<any> = (props: ChatProps) => {
    return (
        <ChatStateProvider>
            <Container>
                <ChatLeftPanel></ChatLeftPanel>
                <ChatRightPanel></ChatRightPanel>
            </Container>
        </ChatStateProvider>
    );
}

export default Chat;