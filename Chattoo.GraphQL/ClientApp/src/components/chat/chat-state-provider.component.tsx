import { CommunicationChannel } from "graphql/graphql-types";
import React, { createContext, ReactNode, useState } from "react";

export interface ChatStateContext {
    currentChannel: CommunicationChannel;
    setCurrentChannel: (channel: CommunicationChannel | undefined) => void;
}

const initial: ChatStateContext = {
    currentChannel: null,
    setCurrentChannel: (channelId: CommunicationChannel) => {}
};

export const ChatStateContext = createContext(initial);

const ChatStateProvider = ({children}: {children: ReactNode}) => {
    // Aktuální komunikační kanál.
    const [currentChannel, setCurrentChannel] = useState<CommunicationChannel | undefined>();

    return (
        <ChatStateContext.Provider value={{
            currentChannel,
            setCurrentChannel
        }}>
            {children}
        </ChatStateContext.Provider>
    );
};

export default ChatStateProvider;