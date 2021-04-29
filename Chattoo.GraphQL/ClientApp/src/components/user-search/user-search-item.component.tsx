import React, { useEffect, useMemo, useState } from 'react'
import styled from 'styled-components'
import { User } from '../../common/interfaces/user.interface';
import ProfilePicture from '../profile-picture/profile-picture.component';
import { UserSearchMode } from './user-search.component';

export interface UserSearchItemProps {
    user: User;
    selectMode: UserSearchMode;
}

const Container = styled.div`
    display: flex;
    flex-direction: row;
    color: white;
`;

const RightSideContainer = styled.div`
    display: flex;
    flex-direction: column;
    justify-content: center;
`;

const UserName = styled.span`
    font-size: 14pt;
`;

const UserSearchItem: React.FC<UserSearchItemProps> = (props: UserSearchItemProps) => {
    const { selectMode } = props;
    const { userName } = props.user;

    const [isSelected, setIsSelected] = useState(false);

    return (
        <Container>
            <ProfilePicture />
            <RightSideContainer>
                <UserName>{userName}</UserName>
            </RightSideContainer>
        </Container>
    );
}

export default UserSearchItem;