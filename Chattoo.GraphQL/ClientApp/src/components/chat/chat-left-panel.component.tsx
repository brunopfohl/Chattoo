import React, { useEffect } from 'react'
import styled from 'styled-components';
import ProfilePicture from '../profile-picture/profile-picture.component';
import CommunicationChannelPreview from './communication-channel-preview.component';
import SearchChannels from './search-channels.component';

const Container = styled.div`
    background-color: #545454;
    flex-grow: 1;
`;

const Heading = styled.div`
    padding-left: 1em;
    display: flex;
    flex-direction: row;
    color: white;
`;


const ChatLeftPanel: React.FC<any> = (props: any) => {

    useEffect(() => {
        
    }, []);

    return (
        <Container>
            <Heading>
                <h2>Chaty</h2>
            </Heading>
            <SearchChannels/>
            <CommunicationChannelPreview channelName="Bruno Pfohl" channelDetail ="Já jsem mu říkal..."/>
            <CommunicationChannelPreview channelName="Lucie Ječmeňová" channelDetail ="Já jsem mu říkal..."/>
            <CommunicationChannelPreview channelName="Dan Právník" channelDetail ="Já jsem mu říkal..."/>
            {/* 
                1 Header
                    1.a ProfileIcon
                    2.a text
                2 SearchBox
                3 CommunicationChannelList
                    3.a CommunicationChannelListItem
            */}
        </Container>
    );
}

export default ChatLeftPanel;