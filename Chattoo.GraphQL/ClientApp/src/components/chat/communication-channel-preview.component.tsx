import React from 'react'
import styled from 'styled-components';
import ProfilePicture from '../profile-picture/profile-picture.component';

interface CommunicationChannelPreviewProps {
    avatarUrl: string,
    channelName: string,
    channelDetail: string
};

const Container = styled.div`
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

    return (
        <Container>
            <ProfilePicture/>
            <RightSide>
                <ChannelName>{props.channelName}</ChannelName>
                <ChannelDetail>{props.channelDetail}</ChannelDetail>
            </RightSide>
        </Container>
    );
}

export default CommunicationChannelPreview;