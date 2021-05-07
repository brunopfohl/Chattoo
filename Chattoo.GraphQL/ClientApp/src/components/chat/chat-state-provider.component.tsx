import React, { createContext, ReactNode, useContext, useState } from "react";
import { CommunicationChannel } from "../../common/interfaces/communication-channel.interface";
import { PaginatedList } from "../../common/interfaces/paginated-list";
import { useGetCommunicationChannelsForUser } from "../../hooks/channels/queries/useGetChannelsForUser";
import { AppStateContext } from "../app-state-provider.component";

export interface ChatStateContext {
    channels: PaginatedList<CommunicationChannel>;
    currentChannel: CommunicationChannel;
    setCurrentChannel: (channel: CommunicationChannel) => void;
}

const initial: ChatStateContext = {
    channels: null,
    currentChannel: null,
    setCurrentChannel: (channel: CommunicationChannel) => {}
};

export const ChatStateContext = createContext(initial);

const ChatStateProvider = ({children}: {children: ReactNode}) => {
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    const [currentChannel, setCurrentChannel] = useState<CommunicationChannel>();

    let [channels, query] = useGetCommunicationChannelsForUser({ userId: user.id, pageNumber: 1, pageSize: 20 });

    console.log(query.error?.message);

    return (
        <ChatStateContext.Provider value={{
            channels,
            currentChannel,
            setCurrentChannel
        }}>
            {children}
        </ChatStateContext.Provider>
    );
};

export default ChatStateProvider;