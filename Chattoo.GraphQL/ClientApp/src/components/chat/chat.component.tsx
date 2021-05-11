import React, { useEffect, useContext, useState } from 'react'
import styled from 'styled-components';
import { useGetCommunicationChannelsForUser } from '../../hooks/channels/queries/useGetChannelsForUser';
import { AppStateContext } from '../app-state-provider.component';
import ChatLeftPanel from './chat-left-panel.component';
import ChatRightPanel from './chat-right-panel.component';
import ChatStateProvider from './chat-state-provider.component';
import { CommunicationChannel } from "../../common/interfaces/communication-channel.interface";
import { GetMessagesForChannel, GetMessagesForChannelVariables, MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables } from '../../common/interfaces/schema-types';
import { GET_MESSAGES_FOR_CHANNEL, useGetMessagesForChannel } from "../../hooks/messages/queries/useGetMessagesForChannel";
import { Message } from '../../common/interfaces/message.interface';
import { useQuery } from '@apollo/client';
import { MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION } from '../../hooks/messages/subscriptions/useMessageAddedToChannelSubscription';

interface ChatProps {
    
};

const Container = styled.div`
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