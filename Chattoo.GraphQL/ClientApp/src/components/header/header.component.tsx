import React, { useEffect, useState } from 'react'
import styled from 'styled-components';
import authService from '../api-authorization/AuthorizeService';
import Button from '../button/button.component';

const Container = styled.div`
    display: flex;
    padding-top: 1em;
    padding-bottom: 1em;
`;

const Header: React.FC<any> = (props: any) => {
    let [token, setToken] = useState<string>();

    useEffect(() => {
        const getToken = async () => {
            setToken(await authService.getAccessToken());
        };

        getToken();
    }, []);

    return (
        <Container>
            <h1>{token}</h1>
            <Button text="Chattoo" radius="60px" backgroundColor="#C227C2" />
        </Container>
    );
}

export default Header;