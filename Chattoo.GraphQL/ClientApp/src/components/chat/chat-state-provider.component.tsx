import React, { createContext, ReactNode, useCallback, useContext, useEffect, useMemo, useState } from "react";
import { CommunicationChannel } from "../../common/interfaces/communication-channel.interface";
import { PaginatedList } from "../../common/interfaces/paginated-list";
import { useGetCommunicationChannelsForUser } from "../../hooks/channels/queries/useGetChannelsForUser";
import { useGetMessagesForChannel } from "../../hooks/messages/queries/useGetMessagesForChannel";
import { useMessageAddedToChannelSubscription } from "../../hooks/messages/subscriptions/useMessageAddedToChannelSubscription";
import { AppStateContext } from "../app-state-provider.component";
import { Message } from "../../common/interfaces/message.interface";
import { GetMessagesForChannelVariables } from "../../common/interfaces/schema-types";

export interface ChatStateContext {
    channels: PaginatedList<CommunicationChannel>;
    currentChannel: CommunicationChannel;
    currentChannelMessages: PaginatedList<Message>;
    tryLoadMoreMessages: () => void;
    setCurrentChannel: (channel: CommunicationChannel) => void;
}

const initial: ChatStateContext = {
    channels: null,
    currentChannel: null,
    currentChannelMessages: null,
    tryLoadMoreMessages: () => {},
    setCurrentChannel: (channel: CommunicationChannel) => {}
};

export const ChatStateContext = createContext(initial);

const ChatStateProvider = ({children}: {children: ReactNode}) => {
    // Datový kontext aplikace.
    const { appState } = useContext(AppStateContext);
    // Aktuální uživatel přihlášený do aplikace.
    const { user } = appState;
    // Dostupné komunikační kanály.
    let [channels, query] = useGetCommunicationChannelsForUser({ userId: user.id, pageNumber: 1, pageSize: 20 });
    // Aktuální komunikační kanál.
    const [currentChannel, setCurrentChannel] = useState<CommunicationChannel>();

    const [queryVariables, setQueryVariables] = useState<GetMessagesForChannelVariables>({
        channelId: null,
        pageNumber: 1,
        pageSize: 20
    });

    // Zobrazené zprávy z aktuálního komunikačního kanálu.
    const [currentChannelMessages, messagesQuery] = useGetMessagesForChannel(queryVariables);

    // Metoda pro načtení dalších zpráv z aktuálního komunikačního kanálu.
    const tryLoadMoreMessages = () => {
        console.log('tryloadmore');
        if (currentChannelMessages?.hasNextPage) {
            messagesQuery?.fetchMore({
                variables: { ...queryVariables, pageNumber: currentChannelMessages.pageIndex + 1},
                updateQuery: (prev, {fetchMoreResult}) => {
                    console.log('fetchmore');
                    if(!fetchMoreResult?.communicationChannelMessages?.getForChannel || fetchMoreResult.communicationChannelMessages.getForChannel.pageIndex < (currentChannelMessages.pageIndex + 1)) {
                        return prev;
                    }

                    const newEntries = fetchMoreResult.communicationChannelMessages.getForChannel;

                    return Object.assign({}, prev,  {
                        communicationChannelMessages: {
                            getForChannel: {
                                data: [ ...newEntries.data, ...prev.communicationChannelMessages.getForChannel.data],
                                hasNextPage: newEntries.hasNextPage,
                                hasPreviousPage: newEntries.hasPreviousPage,
                                pageIndex: newEntries.pageIndex,
                                totalCount: newEntries.totalCount,
                                totalPages: newEntries.totalPages,
                            }
                        }
                    });
                }
            });
        }
    };

    useEffect(() => {
        console.log('chat_state_render');

        return () => {
            console.log('chat_state_destroj');
        }
    });

    useEffect(() => {
        currentChannel && setQueryVariables({  ...queryVariables, channelId: currentChannel.id});
    }, [currentChannel]);

    return (
        <ChatStateContext.Provider value={{
            channels,
            currentChannel,
            currentChannelMessages: currentChannelMessages,
            tryLoadMoreMessages,
            setCurrentChannel
        }}>
            {children}
        </ChatStateContext.Provider>
    );
};

export default ChatStateProvider;