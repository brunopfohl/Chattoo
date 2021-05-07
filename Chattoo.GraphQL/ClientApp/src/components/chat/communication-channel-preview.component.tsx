import React, { useContext } from 'react'
import styled from 'styled-components';
import { CommunicationChannel } from '../../common/interfaces/communication-channel.interface';
import ProfilePicture from '../profile-picture/profile-picture.component';
import { ChatStateContext } from './chat-state-provider.component';

interface CommunicationChannelPreviewProps {
    avatarUrl: string,
    channel: CommunicationChannel,
};

const Container = styled.div`
    cursor: pointer;
    color: white;
    display: flex;
    flex-direction: row;
    padding: 1em;
    border-bottom: 1px solid #69747C;

    &:hover {
        background-color: #69747C;
        cursor: pointer;
    }
`;

const RightSide = styled.div`
    display: flex;
    flex-direction: column;
`;

const ChannelName = styled.h3`
    margin: 0 0 0.5em 0;
`;

const ChannelDetail = styled.span`
    font-weight: lighter;
    color: #ADADAD;
    line-height: 1em;
`;

const CommunicationChannelPreview: React.FC<any> = (props: CommunicationChannelPreviewProps) => {
    const { channel } = props;
    const { setCurrentChannel } = useContext(ChatStateContext);

    // Po kliknutí zvolím tento komunikační kanál.
    const onClickHandler = () => {
        setCurrentChannel(channel);
    }

    return (
        <Container onClick={onClickHandler}>
            <ProfilePicture/>
            <RightSide>
                <ChannelName>{channel.name}</ChannelName>
                <ChannelDetail>{channel.description}</ChannelDetail>
            </RightSide>
        </Container>
    );
}

export default CommunicationChannelPreview;