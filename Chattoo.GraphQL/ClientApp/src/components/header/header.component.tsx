import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react'
import styled from 'styled-components';
import authService from '../api-authorization/AuthorizeService';
import Button from '../button/button.component';

const Container = styled.div`
    display: flex;
    flex-direction: row;
    padding-top: 1em;
    padding-bottom: 1em;
`;

const Left = styled.div`
    display: flex;
    flex-direction: row;
    flex-grow: 1;
`;

const Right = styled.div`
    display: flex;
    flex-direction: row-reverse;
    flex-grow: 1;
`;

const UserName = styled.h2`
    color: white;
`;

const Header: React.FC<any> = (props: any) => {
    const [userName, setUserName] = useState<string>("");

    const router = useRouter();

    let onLogout = () => {
        authService.signOut(router.asPath);
    };

    useEffect(() => {
        authService.getUser().then((user) => {
            user && setUserName(user.name)
        });
    }, []);

    return (
        <Container>
            <Left>
                <UserName>{userName && userName}</UserName>
            </Left>
            <Right>
                <Button text="Odhlásit se" onClick={onLogout}/>
            </Right>
        </Container>
    );
}

export default Header;