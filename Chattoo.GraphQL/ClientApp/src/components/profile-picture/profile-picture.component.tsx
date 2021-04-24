import React from 'react'
import styled from 'styled-components';

interface ProfilePictureProps {
    img: string
}

const ProfilePicture: React.FC<any> = (props: ProfilePictureProps) => {

    return (
        <img width="30px" src={'C:\Users\Bruno Pfohl\OneDrive\Desktop\chattoo\Chattoo.GraphQL\ClientApp\public\vercel.svg'} />
    );
}

export default ProfilePicture;