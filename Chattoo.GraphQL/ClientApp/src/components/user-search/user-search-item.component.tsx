import React, { useState } from 'react'
import styled from 'styled-components'
import { Checkbox } from 'styled-icons/boxicons-regular';
import { Check } from 'styled-icons/boxicons-regular';
import { CheckSquare } from 'styled-icons/boxicons-regular';
import { AppUser } from '../../common/interfaces/app-user.interface';
import Button from '../button/button.component';
import ProfilePicture from '../profile-picture/profile-picture.component';
import { UserSearchMode } from './user-search-popup.component';

export interface UserSearchItemProps {
    user: AppUser;
    selectMode: UserSearchMode;
}

const Container = styled.div`
    display: flex;
    flex-direction: row;
    color: white;
    border-bottom: 1px solid #ADADAD;

    &:hover {
        cursor: pointer;
        filter: brightness(150%);
    }
`;

const UserInfo = styled.div`
    display: flex;
    flex-direction: row;
    flex-grow: 1;
    justify-content: space-between;
`;

const Left = styled.div`
    display: flex;
    flex-direction: row;
    flex-grow: 1;
`;

const Right = styled.div`
    display: flex;
    align-items: center;
    flex-direction: row-reverse;
`;

const UserName = styled.span`
    font-size: 14pt;
    height: 1em;
    align-self: center;
`;

const UserSearchItem: React.FC<UserSearchItemProps> = (props: UserSearchItemProps) => {
    const { selectMode } = props;
    const { userName } = props.user;

    const [isSelected, setIsSelected] = useState(false);

    const renderButton = () => {
        switch(selectMode) {
            case UserSearchMode.multiSelect:
                return (<Button onClick={() => setIsSelected(!isSelected)} icon={isSelected ? CheckSquare : Checkbox} />);
            case UserSearchMode.normal:
            default:
                return (<Button onClick={() => setIsSelected(!isSelected)} icon={Check} />);
        }
    }

    return (
        <Container>
            <ProfilePicture />
            <UserInfo>
                <Left>
                    <UserName>{userName}</UserName>
                </Left>
                <Right>
                    {renderButton()}
                </Right>
            </UserInfo>
        </Container>
    );
}

export default UserSearchItem;