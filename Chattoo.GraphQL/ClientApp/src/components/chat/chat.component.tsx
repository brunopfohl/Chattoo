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

const usePagedQuery = () => {
    return { allMessages };
}

const ChatBody = (props: ChatBodyProps) => {
    const { currentChannelId } = props;

    const [ previousMessages, setPreviousMessages ] = useState<Message[]>([]);
    const [ newMessages, setNewMessages ] = useState<Message[]>([]);
    const [ pageNumber, setPageNumber ] = useState(1);
    const queryVariables: GetMessagesForChannelVariables = {
        channelId: currentChannelId,
        pageNumber: pageNumber,
        pageSize: 20
    };

    const { loading, error, data, updateQuery, fetchMore, subscribeToMore } = useQuery<GetMessagesForChannel, GetMessagesForChannelVariables>(GET_MESSAGES_FOR_CHANNEL, {
        variables: queryVariables
    });
    const currentMessages = data?.communicationChannelMessages?.getForChannel?.data ?? [];

    
    const doFetchMore = () => {
        setPageNumber(pageNumber + 1);
        setPreviousMessages([...currentMessages, ...previousMessages]);
    }

    useEffect(() => {
        const unsub = subscribeToMore<MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables>({
            document: MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION,
            variables: { channelId: currentChannelId },
            updateQuery: (prev, { subscriptionData }) => {
                    // Pokud nemám žádná nová data, vrátím původní hodnotu.
                    const newMessage = subscriptionData.data.communicationChannelMessageAddedToChannel;
                    const lastMessage = newMessages[newMessages.length - 1];
                    if (!subscriptionData.data) {
                        return prev;
                    }

                    // Vytáhnu si novou zprávu.
                    if (lastMessage?.id != newMessage.id) {
                        setNewMessages([...newMessages, newMessage]);
                    }
                    return prev;
                }
        });

        return () => {
            unsub();
        }
    }, [ newMessages ]);
    

    if (loading) {
        return <div>loading</div>
    }


    const allMessages = [...previousMessages, ...currentMessages, ...newMessages];
    return (
        <div>
            <button onClick={doFetchMore}>more</button>
            <div>
                <ul>
                    {previousMessages.map((i, j) => <li key={j}>
                        <code>OLD: {j+1} {i.id} {i.content}</code>
                    </li>)}
                    {currentMessages.map((i, j) => <li key={j}>
                        <code>CUR: {j+1} {i.id} {i.content}</code>
                    </li>)}
                    {newMessages.map((i, j) => <li key={j}>
                        <code>NEW: {j+1} {i.id} {i.content}</code>
                    </li>)}
                </ul>
            </div>
        </div>
    )
}


const Chat: React.FC<any> = (props: ChatProps) => {
    console.log('chat_rendered');
    const { appState } = useContext(AppStateContext);
    const { user } = appState;
    const [ channels, query ] = useGetCommunicationChannelsForUser({ userId: user.id, pageNumber: 1, pageSize: 20 });
    const [ currentChannel, setCurrentChannel ] = useState<CommunicationChannel>();


    if (channels == null || channels.data == null) {
        return <div>loading {channels?.data?.length}</div>
    }

    return (
        <div>
            <ul>
                {channels.data.map((i, j) => <li key={j}>
                    <button onClick={() => setCurrentChannel(i)}>{i.id} {i.name}</button>
                </li>)}
            </ul>
            {currentChannel != null && <ChatBody currentChannelId={currentChannel.id} />}
        </div>
        // <ChatStateProvider>
            // <Container>
            //     <ChatLeftPanel></ChatLeftPanel>
            //     <ChatRightPanel></ChatRightPanel>
            // </Container>
        //</ChatStateProvider>
    );
}

export default Chat;