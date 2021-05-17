import { useQuery, useSubscription } from '@apollo/client';
import React, { useContext } from 'react'
import { useEffect } from 'react';
import styled from 'styled-components';
import { GetChannelsForUser, GetChannelsForUserVariables, UserAddedToChannelSubscription, UserAddedToChannelSubscriptionVariables } from '../../common/interfaces/schema-types';
import { GET_CHANNELS_FOR_USER, useGetCommunicationChannelsForUser } from '../../hooks/channels/queries/useGetChannelsForUser';
import { USER_ADDED_TO_COMMUNICATION_CHANNEL_SUBSCRIPTION } from '../../hooks/channels/subscriptions/useUserAddedtoChannelSubscription';
import { AppStateContext } from '../app-state-provider.component';
import { ChatStateContext } from './chat-state-provider.component';
import CommunicationChannelPreview from './communication-channel-preview.component';

const Container = styled.div`
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    overflow-y: auto;
`;

const CommunicationChannelList: React.FC = () => {
    const { appState } = useContext(AppStateContext);
    const { currentChannel, setCurrentChannel } = useContext(ChatStateContext);
    const { user } = appState;

    const { loading, error, data, refetch, subscribeToMore } = useQuery<GetChannelsForUser, GetChannelsForUserVariables>(
        GET_CHANNELS_FOR_USER,
        {
            variables: {
                userId: user.id,
                pageNumber: 1, 
                pageSize: 20
            }
        }
    );

    const channels = data?.communicationChannels?.getForUser;

    // Po změne pole s dostupnými komunikačními kanály nastavím aktuální komunikační kanál
    // na první z nich, pokud uživatel ještě nezvolil žádný komunikační kanál.
    useEffect(() => {
        if(!currentChannel && channels?.data?.length > 0) {
            setCurrentChannel(channels.data[0]);
        }
    }, [channels]);

    useSubscription<UserAddedToChannelSubscription, UserAddedToChannelSubscriptionVariables>(USER_ADDED_TO_COMMUNICATION_CHANNEL_SUBSCRIPTION, {
        variables: {
            userId: user.id
        },
        onSubscriptionData: () => {
            refetch();
        }
    });

    return (
        <Container>
            {channels?.data && channels.data.map((ch) => (
                <CommunicationChannelPreview channel={ch} key={ch.id}/>
            ))}
        </Container>
    );
}

export default CommunicationChannelList;