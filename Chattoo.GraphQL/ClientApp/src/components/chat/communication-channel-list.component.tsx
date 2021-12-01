import { Divider, List } from '@mui/material';
import { useGetChannelsForUserQuery, useUserAddedToChannelSubscription } from 'graphql/graphql-types';
import React, { useContext } from 'react'
import { useEffect } from 'react';
import { AppStateContext } from '../app-state-provider.component';
import { ChatStateContext } from './chat-state-provider.component';
import CommunicationChannelPreview from './communication-channel-preview.component';

const CommunicationChannelList: React.FC = () => {
    const { appState } = useContext(AppStateContext);
    const { currentChannel, setCurrentChannel } = useContext(ChatStateContext);
    const { user } = appState;

    const { data, refetch } = useGetChannelsForUserQuery({
        variables: {
            userId: user.id,
            pageNumber: 1, 
            pageSize: 20
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

    const channels = data?.communicationChannels?.getForUser?.data;

    // Po změne pole s dostupnými komunikačními kanály nastavím aktuální komunikační kanál
    // na první z nich, pokud uživatel ještě nezvolil žádný komunikační kanál.
    useEffect(() => {
        if(!currentChannel && channels?.length > 0) {
            setCurrentChannel(channels[0]);
        }
    }, [channels]);

    return (
        <List>
            {channels && channels.map((ch, i) => (
                <>
                    <CommunicationChannelPreview channel={ch} />

                    {i < channels.length - 1 &&
                        <Divider />
                    }
                </>
            ))}
        </List>
    );
}

export default CommunicationChannelList;