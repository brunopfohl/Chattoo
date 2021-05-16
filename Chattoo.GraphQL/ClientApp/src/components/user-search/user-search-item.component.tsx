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
    isSelected: boolean;
    selectMode: UserSearchMode;
    addUser: (user: AppUser) => void;
    removeUser: (user: AppUser) => void;
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
    // Vytáhnu hodnoty z props.
    const { user, selectMode, addUser, removeUser, isSelected } = props;
    const { userName } = user;

    // Metoda pro vybrání/odvybrání uživatele.
    const toggleIsSelected = () => {
        // Pokud je uživatel vybraný, přidám ho do pole vybraných uživatelů.
        if(!isSelected) {
            addUser(user);
        }
        else { // Pokud uživatel není vybraný, odeberu ho z pole vybraných uživatelů.
            removeUser(user);
        }
    };

    // Metoda pro vyrenderování tlačítka na vybrání/odvybrání uživatele.
    const renderButton = () => {
        switch(selectMode) {
            case UserSearchMode.multiSelect:
                return (<Button onClick={toggleIsSelected} icon={isSelected ? CheckSquare : Checkbox} />);
            case UserSearchMode.normal:
            default:
                return (<Button onClick={toggleIsSelected} icon={Check} />);
        }
    }

    return (
        <Container onClick={toggleIsSelected}>
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