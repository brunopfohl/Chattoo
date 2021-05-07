import React, { useContext, useState } from 'react'
import styled from 'styled-components';
import { Settings } from 'styled-icons/evaicons-solid';
import { CreateMessageInput, useCreateMessage } from '../../hooks/messages/mutations/useCreateMessage';
import { useGetMessagesForChannel } from '../../hooks/messages/queries/useGetMessagesForChannel';
import { useGetUsersForChannel } from '../../hooks/users/queries/useGetUsersForChannel';
import { AppStateContext } from '../app-state-provider.component';
import Button from '../button/button.component';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import MessageBox from '../chat/message-box.component';
import Message from '../chat/message.component';
import CommunicationChannelSettingsPopup from './communication-channel-settings-popup.component';

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

const ChannelHeader = styled.div`
    display: flex;
    flex-direction: row;
    flex-grow: 1;
    padding: 0 1em;
    min-height: 4em;
    background-color: #3f3f3f;
`;

const ChannelHeaderLeft = styled.div`
    display: flex;
    flex-direction: row;
    flex-grow: 1;
    align-items: center;
`;

const ChannelHeaderRight = styled.div`
    display: flex;
    flex-grow: 1;
    flex-direction: row-reverse;
    align-items: center;
`;

const ChannelTitle = styled.h2`
    color: white;
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
    const { appState } = useContext(AppStateContext);
    const { user } = appState;
    const { currentChannel } = useContext(ChatStateContext);

    const createMessage = useCreateMessage()

    const [showSettings, setShowSettings] = useState<boolean>()

    const [messages, messagesQuery] = useGetMessagesForChannel({ channelId: currentChannel?.id, pageNumber: 1, pageSize: 10 });

    const addMessage = (msg: string) => {
        const params: CreateMessageInput = {
            variables: {
                channelId: currentChannel.id,
                userId: user.id,
                content: msg
            }
        }

        createMessage(params);
    };

    return (
        <Container>
            { showSettings &&
                <CommunicationChannelSettingsPopup onClose={() => { setShowSettings(false) }}/>
            }
            <ChannelHeader>
                <ChannelHeaderLeft>
                    <ChannelTitle>{currentChannel?.name}{currentChannel?.id}</ChannelTitle>
                </ChannelHeaderLeft>
                <ChannelHeaderRight>
                    <Button onClick={() => { setShowSettings(true) } } icon={Settings}/>
                </ChannelHeaderRight>
            </ChannelHeader>
            <Content>
                {messages?.data && messages?.data?.map((m) =>
                    <Message content={m.content}/>
                )}
            </Content>
            <MessageBoxContainer>
                <MessageBox callback={addMessage}/>
            </MessageBoxContainer>
        </Container>
    );
}

export default CommunicationChannel;