import React, { useEffect, useState } from 'react'
import styled from 'styled-components';
import ProfilePicture from '../profile-picture/profile-picture.component';
import MessageBox from './message-box.component';
import Message from './message.component';

interface CommunicationChannelProps {
    avatarUrl: string,
    channelName: string
};

const Container = styled.div`
    flex-grow: 1;
    display: flex;
    flex-direction: column;
    overflow: hidden;
`;

const Heading = styled.div`
    min-height: 4em;
    background-color: #3f3f3f;
`;

const Content = styled.div`
    flex-grow: 10;
    overflow: auto;
    overflow-x: hidden;
`;

const MessageBoxContainer = styled.div`
    min-height: 4em;
    display: flex;
    flex-direction: column;
    justify-content: center;
    background-color: #3f3f3f;
`;

const CommunicationChannel: React.FC<any> = (props: CommunicationChannelProps) => {
    const [messages, setMessages] = useState<string[]>([]);

    const addMessage = (msg: string) => {
        setMessages([...messages, msg]);
    };

    useEffect(() => {
    }, [messages])
    
    return (
        <Container>
            <Heading/>
            <Content>
                {messages.map((m) =>
                    <Message content={m}/>
                )}
            </Content>
            <MessageBoxContainer>
                <MessageBox callback={addMessage}/>
            </MessageBoxContainer>
        </Container>
    );
}

export default CommunicationChannel;