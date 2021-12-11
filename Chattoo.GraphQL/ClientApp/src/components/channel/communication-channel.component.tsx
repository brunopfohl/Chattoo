import React, { FC, memo, useCallback, useContext, useEffect, useMemo, useRef, useState } from 'react'
import { AppStateContext } from '../app-state-provider.component';
import { ChatStateContext } from '../chat/chat-state-provider.component';
import MessageBox from '../chat/message-box.component';
import MessageComponent, { MessageComponentProps } from '../chat/message.component';
import CommunicationChannelSettingsPopup from './communication-channel-settings-popup.component';
import { CommunicationChannelMessage, GetMessagesForChannelQueryVariables, MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables, useCreateMessageMutation, useGetMessagesForChannelQuery } from 'graphql/graphql-types';
import produce from 'immer';
import { MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION } from 'graphql/subscriptions/messages/messageAddedToChannel';
import { Box, Divider, IconButton, Stack, Typography } from '@mui/material';
import SettingsIcon from '@mui/icons-material/Settings';

/** Počet zpráv na stránku */
const MESSAGES_PAGE_SIZE = 20;

/** Číslo 1. stránky */
const MESSAGES_FIRST_PAGE = 1;

/**
 * Komponenta komunikačního kanálu.
 */
const CommunicationChannel: FC = () => {
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    // Aktuálně využívaný komunikační kanál.
    const { currentChannel } = useContext(ChatStateContext);

    /** Parametry dotazu pro načtení zpráv z komunikačního kanálu */
    const messagesQueryVariables: GetMessagesForChannelQueryVariables = {
        channelId: currentChannel.id,
        pageNumber: MESSAGES_FIRST_PAGE,
        pageSize: MESSAGES_PAGE_SIZE
    }

    const { data, fetchMore, subscribeToMore } = useGetMessagesForChannelQuery({
        variables: messagesQueryVariables
    });

    // Pomocné proměnné pro přehlednější přístup k aktuálně načteným zprávám.
    const getForChannelRes = data?.communicationChannelMessages?.getForChannel;
    const messages = getForChannelRes?.data;

    // Metoda, po jejíž zavolání se načtou další zprávy (pokud jsou nějaké dostupné).
    const tryLoadMoreMessages = useCallback(() => {
        if (data?.communicationChannelMessages?.getForChannel?.hasNextPage) {
            fetchMore({
                variables: { ...messagesQueryVariables, pageNumber: getForChannelRes.pageIndex + 1 },
                updateQuery: (prev, { fetchMoreResult }) => {
                    if (!fetchMoreResult) {
                        return prev;
                    }

                    const getForChannel = fetchMoreResult.communicationChannelMessages?.getForChannel;

                    if (!getForChannel)
                        return prev;

                    const { data: newPageData, ...newPage } = getForChannel;

                    let res = produce(prev, draftState => {
                        let prevPage = draftState.communicationChannelMessages?.getForChannel;
                        let prevPageData = prevPage?.data;

                        if (prevPageData && newPageData && prevPage) {
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
    }, []);

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

                const oldMessages = prev.communicationChannelMessages?.getForChannel?.data;

                if (oldMessages && oldMessages[oldMessages.length - 1]?.id !== newMessage?.id) {
                    return produce(prev, draftState => {
                        let data = draftState.communicationChannelMessages?.getForChannel?.data;

                        if (data) {
                            data.push(newMessage)
                            data = data.filter((v, i, a) => a.indexOf(v) === i && v.channelId == currentChannel.id);
                        }
                    });
                }

                return prev;
            }
        });

        return () => {
            // Po desktrukci komponenty komunikačního kanálu přestane naslouchání k serveru.
            unsub && unsub();
        }
    }, [currentChannel]);

    // Proměnná určující, jestli je zobrazený panel s nastavením komunikačního kanálu.
    const [showSettings, setShowSettings] = useState<boolean>();

    // Metoda pro přidání nové zprávy.
    const [createMessage] = useCreateMessageMutation();
    const addMessage = useCallback((msg: string) => {
        createMessage({
            variables: {
                channelId: currentChannel.id,
                userId: user.id,
                content: msg
            }
        })
    }, [currentChannel, user]);

    // Pomocná metoda pro render prvku se zprávou.
    const renderMessage = useCallback((message: CommunicationChannelMessage, previousMesssage?: CommunicationChannelMessage, nextMessage?: CommunicationChannelMessage) => {
        /** Příznak, zda-li je zpráva od aktuálně přihlášeného uživatele. */
        const isFromCurrentUser = appState.user?.id === message.userId;
        /** Příznak, zda-li se jedná o první zprávu od daného uživatele. */
        const isStartOfBatch = previousMesssage?.userId !== message.userId;
        /** Příznak, zda-li následující zpráva je od jiného uživatele. */
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
            <MessageComponent {...messageProps} key={message.id} />
        );
    }, [])

    // Element automaticky scrolluje k nově přidané zprávě.
    const messagesRef = useRef<HTMLDivElement>(null);

    // Pokud uživatel sescrolluje nahoru, donačtou se další zprávy.
    const onScroll = useCallback(() => {
        const currMsgRef = messagesRef?.current;

        if (currMsgRef && (currMsgRef.scrollHeight - currMsgRef.clientHeight + currMsgRef.scrollTop) <= 2) {
            tryLoadMoreMessages();
        }
    }, [messagesRef, tryLoadMoreMessages]);

    useEffect(() => {
        // Pokud odkaz na element se zprávami není null.
        if (messagesRef.current) {
            // Přiřadím scroll eventu onScroll callback.
            messagesRef.current.addEventListener('scroll', onScroll);
        }

        return () => {
            // Odeberu scroll eventu onScroll callback.
            messagesRef.current?.removeEventListener('scroll', onScroll);
        };
    }, [messagesRef]);

    /** Pole zpráv pro zobrazení (memoized pro zamezení zbytečného výpočítávání) */
    const messagesFiltered = useMemo(() => (messages &&
        [...messages].reverse().map((msg, i, arr) =>
            renderMessage(msg, arr[i - 1], arr[i + 1]))
    ), [messages]);

    /** Callback volaný po zavření dialogu s nastavením komunikačního kanálu */
    const onSettingsClose = useCallback(() => { setShowSettings(false) }, [setShowSettings]);

    /** Callback volaný po otevření dialogu s nastavením komunikačního kanálu */
    const onSettingsOpen = useCallback(() => { setShowSettings(true) }, [setShowSettings])

    return (
        <Stack sx={{ p: 1, height: "100%" }}>
            {/* Dialog pro nastavení komunikačního kanálu */}
            {showSettings &&
                <CommunicationChannelSettingsPopup onClose={onSettingsClose} />
            }

            {/* Záhlaví komponenty komunikačního kanálu */}
            <Stack direction="row" justifyContent="space-between">
                <Typography variant="h5">{currentChannel?.name}</Typography>
                <IconButton color="primary" onClick={onSettingsOpen}>
                    <SettingsIcon />
                </IconButton>
            </Stack>

            <Divider />

            {/* Komponenta se zprávami komunikačního kanálu */}
            <Stack sx={{ p: 1, flexDirection: "column-reverse", flexGrow: 1, flexBasis: "100px", overflowY: "auto", overflowX: "hidden" }} id="scrollableMessages" ref={messagesRef}>
                {messagesFiltered && messagesFiltered}
            </Stack>

            {/* Komponenta pro odeslání zprávy */}
            <Box pt={1}>
                <MessageBox callback={addMessage} />
            </Box>
        </Stack>
    );
}

const CommunicationChannelMemo = memo(CommunicationChannel);

CommunicationChannelMemo.displayName = "CommunicationChannelMemoComponent";
export default CommunicationChannelMemo;