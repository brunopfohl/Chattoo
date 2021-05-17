import { useRouter } from 'next/router';
import React, { useContext } from 'react'
import styled from 'styled-components';
import authService from '../api-authorization/AuthorizeService';
import { AppStateContext } from '../app-state-provider.component';
import Button, { ButtonTheme } from '../button/button.component';

const Container = styled.div`
    display: flex;
    flex-direction: row;
    padding-top: 1em;
    padding-bottom: 1em;
`;

const Left = styled.div`
    display: flex;
    flex-direction: row;
    align-items: center;
    flex-grow: 1;
`;

const Right = styled.div`
    display: flex;
    flex-direction: row-reverse;
    align-items: center;
    flex-grow: 1;
`;

const UserName = styled.h2`
    color: white;
    margin-right: 1em;
`;

const Header: React.FC<any> = (props: any) => {
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    const router = useRouter();

    let onLogout = () => {
        authService.signOut(router.asPath);
    };

    return (
        <Container>
            <Left>
                <UserName>{user && user.userName}</UserName>
            </Left>
            <Right>
                <Button text="Odhlásit se" onClick={onLogout} theme={ButtonTheme.green}/>
            </Right>
        </Container>
    );
}

export default Header;