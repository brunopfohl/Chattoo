import React, { useContext } from 'react'
import styled from 'styled-components';
import { ChatStateContext } from './chat-state-provider.component';
import CommunicationChannelPreview from './communication-channel-preview.component';

export interface CommunicationChannelListProps {
    
}

const Container = styled.div`
    display: flex;
    flex-direction: column;
    flex-grow: 1;
    overflow-y: auto;
`;

const CommunicationChannelList: React.FC<any> = (props: CommunicationChannelListProps) => {
    const { channels } = useContext(ChatStateContext);

    return (
        <Container>
            {channels?.data && channels.data.map((ch) => (
                <CommunicationChannelPreview channel={ch} key={ch.id}/>
            ))}
        </Container>
    );
}

export default CommunicationChannelList;