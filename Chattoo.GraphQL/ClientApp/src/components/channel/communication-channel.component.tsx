import React, { useCallback, useContext, useEffect, useRef, useState } from 'react'
import styled from 'styled-components';
import { Settings } from 'styled-icons/evaicons-solid';
import { CreateMessageInput, useCreateMessage } from '../../hooks/messages/mutations/useCreateMessage';
import { AppStateContext } from '../app-state-provider.component';
import Button from '../button/button.component';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import MessageBox from '../chat/message-box.component';
import MessageComponent, { MessageComponentProps } from '../chat/message.component';
import CommunicationChannelSettingsPopup from './communication-channel-settings-popup.component';
import { CommunicationChannelMessage, GetMessagesForChannelQueryVariables, MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables, useCreateMessageMutation, useGetMessagesForChannelQuery } from 'graphql/graphql-types';
import produce from 'immer';
import { MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION } from 'graphql/subscriptions/messages/messageAddedToChannel';

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
    height: 4em;
    display: flex;
    flex-direction: row;
    padding: 0 1em;
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
    padding: 0.5em;
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

    // Aktuálně využívaný komunikační kanál.
    const { currentChannel } = useContext(ChatStateContext);

    const messagesQueryVariables: GetMessagesForChannelQueryVariables = {
        channelId: currentChannel.id,
        pageNumber: 1,
        pageSize: 20
    }

    const { data, fetchMore, subscribeToMore } = useGetMessagesForChannelQuery({
        variables: messagesQueryVariables
    });

    // Pomocné proměnné pro přehlednější přístup k aktuálně načteným zprávám.
    const getForChannelRes = data?.communicationChannelMessages?.getForChannel;
    const messages = getForChannelRes?.data;
    
    // Metoda, po jejíž zavolání se načtou další zprávy (pokud jsou nějaké dostupné).
    const tryLoadMoreMessages = () => {
        if(data?.communicationChannelMessages?.getForChannel.hasNextPage)
        {
            fetchMore({
                variables: { ...messagesQueryVariables, pageNumber: getForChannelRes.pageIndex + 1},
                updateQuery: (prev, { fetchMoreResult }) => {
                    if(!fetchMoreResult) {
                        return prev;
                    }

                    const getForChannel = fetchMoreResult.communicationChannelMessages?.getForChannel;

                    if (!getForChannel)
                        return prev;

                    const { data: newPageData, ...newPage } = getForChannel;

                    let res = produce(prev, draftState => {
                        let prevPage = draftState.communicationChannelMessages?.getForChannel;
                        let prevPageData = prevPage?.data;

                        if (prevPageData) {
                            prevPageData.unshift(...newPageData);
                            prevPageData = prevPageData.filter((v, i, a) => a.indexOf(v) === i && v.channelId === currentChannel.id);

                            prevPage.pageIndex = newPage.pageIndex;
                            prevPage.hasNextPage = newPage.hasNextPage;
                        }
                    });

                    return res;
                }
            });
        }
    }

    // UseEffect pro nastavení callbacku po přijetí nové zprávy z websocketu.
    useEffect(() => {
        const unsub = subscribeToMore<MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables>({
            document: MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION,
            variables: {
                channelId: currentChannel.id
            },
            updateQuery: (prev, { subscriptionData }) => {
                // Pokud nemám žádná nová data, vrátím původní hodnotu.
                if (!subscriptionData.data)
                    return prev;

                // Vytáhnu si novou zprávu.
                const newMessage = subscriptionData.data.communicationChannelMessageAddedToChannel;

                return produce(prev, draftState => {
                    let data = draftState.communicationChannelMessages?.getForChannel?.data;

                    if (data) {
                        data.push(newMessage)
                        data = data.filter((v, i, a) => a.indexOf(v) === i && v.channelId == currentChannel.id);
                    }
                });
            }
        });

        return () => {
            unsub();
        }
    }, [currentChannel]);

    // Proměnná určující, jestli je zobrazený panel s nastavením komunikačního kanálu.
    const [showSettings, setShowSettings] = useState<boolean>();

    // Metoda pro přidání nové zprávy.
    const [createMessage] = useCreateMessageMutation();
    const addMessage = (msg: string) => {
        createMessage({
            variables: {
                channelId: currentChannel.id,
                userId: user.id,
                content: msg
            }
        })
    };

    // Pomocná metoda pro render prvku se zprávou.
    const renderMessage = (message: CommunicationChannelMessage, previousMesssage?: CommunicationChannelMessage, nextMessage?: CommunicationChannelMessage) => {
        // Příznak, zda-li je zpráva od aktuálně přihlášeného uživatele.
        const isFromCurrentUser = appState.user?.id === message.userId;
        // Příznak, zda-li se jedná o první zprávu od daného uživatele.
        const isStartOfBatch = previousMesssage?.userId !== message.userId;
        // Příznak, zda-li následující zpráva je od jiného uživatele.
        const isEndOfBatch = nextMessage?.userId !== message.userId;

        // Parametry pro komponent se zprávou.
        const messageProps: MessageComponentProps = {
            content: message.content,
            createdAt: message.createdAt,
            isStartOfBatch: isStartOfBatch,
            isEndOfBatch: isEndOfBatch,
            isFromCurrentUser: isFromCurrentUser,
            userName: message.userName
        };

        return (
            <MessageComponent {...messageProps} key={message.id}/>
        );
    }

    // Element automaticky scrolluje k nově přidané zprávě.
    const messagesRef = useRef(null);

    // Pokud uživatel sescrolluje nahoru, donačtou se další zprávy.
    const onScroll = useCallback(() => {
        if (messagesRef !== null && messagesRef.current.scrollTop === 0) {
            tryLoadMoreMessages();
        }
    }, [messagesRef, tryLoadMoreMessages]);

    useEffect(() => {
        // Pokud odkaz na element se zprávami není null.
        if(messagesRef) {
            // Přiřadím scroll eventu onScroll callback.
            messagesRef.current.addEventListener('scroll', onScroll);
        }

        return () => {
            // Odeberu scroll eventu onScroll callback.
            messagesRef.current.removeEventListener('scroll', onScroll);
        };
    }, [onScroll]);

    return (
        <Container>
            { showSettings &&
                <CommunicationChannelSettingsPopup onClose={() => { setShowSettings(false) }}/>
            }
            <ChannelHeader>
                <ChannelHeaderLeft>
                    <ChannelTitle>{currentChannel?.name}</ChannelTitle>
                </ChannelHeaderLeft>
                <ChannelHeaderRight>
                    <Button onClick={() => { setShowSettings(true) } } icon={Settings}/>
                </ChannelHeaderRight>
            </ChannelHeader>
            <Content id="scrollableMessages" ref={messagesRef}>
                {messages &&
                    messages.map((msg, i, arr) =>
                        renderMessage(msg, arr[i - 1], arr[i + 1])
                    )
                }
            </Content>
            <MessageBoxContainer>
                <MessageBox callback={addMessage}/>
            </MessageBoxContainer>
        </Container>
    );
}

export default CommunicationChannel;