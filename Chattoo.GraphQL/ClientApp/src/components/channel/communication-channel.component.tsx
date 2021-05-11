import React, { useCallback, useContext, useEffect, useRef, useState } from 'react'
import styled from 'styled-components';
import { Settings } from 'styled-icons/evaicons-solid';
import { Message } from '../../common/interfaces/message.interface';
import { CreateMessageInput, useCreateMessage } from '../../hooks/messages/mutations/useCreateMessage';
import { AppStateContext } from '../app-state-provider.component';
import Button from '../button/button.component';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import MessageBox from '../chat/message-box.component';
import MessageComponent, { MessageComponentProps } from '../chat/message.component';
import CommunicationChannelSettingsPopup from './communication-channel-settings-popup.component';
import InfiniteScroll from 'react-infinite-scroll-component'

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
    const { currentChannel, currentChannelMessages, tryLoadMoreMessages } = useContext(ChatStateContext);

    const createMessage = useCreateMessage()

    const [showSettings, setShowSettings] = useState<boolean>()


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

    const renderMessage = (message: Message, previousMesssage?: Message, nextMessage?: Message) => {
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
            userName: message.userId
        };

        return (
            <MessageComponent {...messageProps} />
        );
    }

    // Element automaticky scrolluje k nově přidané zprávě.
    const messagesRef = useRef(null);

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
                    <ChannelTitle>{currentChannel?.name}{currentChannel?.id}</ChannelTitle>
                </ChannelHeaderLeft>
                <ChannelHeaderRight>
                    <Button onClick={() => { setShowSettings(true) } } icon={Settings}/>
                </ChannelHeaderRight>
            </ChannelHeader>
            <Content id="scrollableMessages" ref={messagesRef}>
                {currentChannelMessages?.data &&
                    currentChannelMessages.data.map((m, i, arr) =>
                        renderMessage(m, arr[i - 1], arr[i + 1])
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
export const MemoizedCommunicationChannel = React.memo(CommunicationChannel);