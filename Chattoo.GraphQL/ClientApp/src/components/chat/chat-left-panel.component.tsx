import React from 'react'
import styled from 'styled-components';
import ProfilePicture from '../profile-picture/profile-picture.component';

const Container = styled.div`
    background-color: #545454;
    flex-grow: 1;
`;

const Heading = styled.div`
    display: flex;
    flex-direction: row;
`;

const ChatLeftPanel: React.FC<any> = (props: any) => {

    return (
        <Container>
            <Heading>
                <ProfilePicture/>
            </Heading>
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