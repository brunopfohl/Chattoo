import React from 'react'
import styled from 'styled-components';
import { Person } from 'styled-icons/bootstrap';

interface ProfilePictureProps {
    img: string,
    size: string
}

const Container = styled.div`
    padding-top: 0.5em;
    padding-bottom: 0.5em;
`;

const PersonIcon = styled(Person)`
    margin-right: 0.5em;
`;

const ProfilePicture: React.FC<any> = (props: ProfilePictureProps) => {
    const size = props.size || "50px";

    return (
        <Container>
            <PersonIcon size={size}/>
        </Container>
    );
}

export default ProfilePicture;