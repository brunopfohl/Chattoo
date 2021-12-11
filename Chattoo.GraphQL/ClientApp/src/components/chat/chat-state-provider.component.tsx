import { CommunicationChannel } from "graphql/graphql-types";
import React, { createContext, ReactNode, useState } from "react";

/** Globální datový kontext aplikace */
export interface ChatStateContext {
    currentChannel: CommunicationChannel | null;
    setCurrentChannel: (channel: CommunicationChannel | undefined) => void;
}

/** Výchozí stav datového kontextu aplikace */
const initial: ChatStateContext = {
    currentChannel: null,
    setCurrentChannel: (channelId: CommunicationChannel) => { }
};

export const ChatStateContext = createContext(initial);
ChatStateContext.displayName = "ChatStateContext";

const ChatStateProvider = ({ children }: { children: ReactNode }) => {
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