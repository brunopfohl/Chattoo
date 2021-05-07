import { useRouter } from 'next/router';
import React from 'react'
import styled from 'styled-components';
import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';
import Button, { ButtonTheme } from '../button/button.component';

const Container = styled.div`
    display: flex;
    width: 30vw;
    padding: 2em;
    background-color: #545454;
    border-radius: 30px;
`;

const ConnectAccount: React.FC<any> = () => {
    let router = useRouter();

    let onLogin = () => {
        router.push({
            pathname: ApplicationPaths.Login,
            query: { origin: router.asPath }
        });
    };

    return (
        <Container>
            <Button text="Přihlásit se" onClick={onLogin} stretch={true} theme={ButtonTheme.green}/>
        </Container>
    );
}

export default ConnectAccount;