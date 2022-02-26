import { AppStateContext } from '@components/app-state-provider.component';
import { List } from '@mui/material';
import { useGetChannelsForUserQuery, useUserAddedToChannelSubscription } from 'graphql/graphql-types';
import { FC, useContext, useMemo } from 'react'
import { useEffect } from 'react';
import { ChatStateContext } from './chat-state-provider.component';
import CommunicationChannelPreview from './communication-channel-preview.component';

/** Počet kanálů na stránku */
const CHANNELS_PAGE_SIZE = 20;

/** Číslo 1. stránky */
const CHANNELS_FIRST_PAGE = 1;

interface CommunicationChannelProps {
    filterText: string;
}

/**
 * Komponenta se seznamem komunikačních kanálů.
 */
const CommunicationChannelList: FC<CommunicationChannelProps> = (props) => {
    const { filterText } = props;
    const { appState } = useContext(AppStateContext);
    const { currentChannel, setCurrentChannel } = useContext(ChatStateContext);
    const { user } = appState;

    const { data, refetch } = useGetChannelsForUserQuery({
        variables: {
            userId: user.id,
            pageNumber: CHANNELS_FIRST_PAGE,
            pageSize: CHANNELS_PAGE_SIZE
        }
    });

    useUserAddedToChannelSubscription({
        variables: {
            userId: user.id
        },
        onSubscriptionData: () => {
            refetch();
        }
    })

    const channels = useMemo(() => {
        const items = data?.communicationChannels?.getForUser?.data || [];

        return items.filter((i) => i.name.toLowerCase().includes(filterText.toLowerCase()));
    }, [data, filterText]);

    // Po změne pole s dostupnými komunikačními kanály nastavím aktuální komunikační kanál
    // na první z nich, pokud uživatel ještě nezvolil žádný komunikační kanál.
    useEffect(() => {
        if (!currentChannel && channels?.length > 0) {
            setCurrentChannel(channels[0]);
        }
    }, [channels]);

    return (
        <List sx={{ overflowY: "auto", flexGrow: 1, flexBasis: 100 }}>
            {channels && channels.map((ch) => (
                <CommunicationChannelPreview channel={ch} key={ch.id} selected={ch.id === currentChannel?.id} />
            ))}
        </List>
    );
}
CommunicationChannelList.displayName = "CommunicationChannelListComponent";
export default CommunicationChannelList;