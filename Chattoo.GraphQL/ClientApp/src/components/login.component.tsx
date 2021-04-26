import React, { useEffect, useState } from 'react'
import styled from 'styled-components'
import authService from './api-authorization/AuthorizeService';

const Container = styled.div`
    width: 30vw;
    padding: 2em;
    background-color: #545454;
    border-radius: 30px;
`;

const Heading = styled.h2`
    color: white;
    margin: 0;
    margin-bottom: 1em;
`;

const LoginForm = styled.form`
    display: flex;
    flex-direction: column;
`;

const StyledInput = styled.input`
    padding: 1em;
    border: 2px solid #69747C;
    border-radius: 2em;
    color: #69747C;
    margin: 0.5em 0;
`;

const ConfirmButton = styled.input`
    font-size: 12pt;
    color: white;
    cursor: pointer;
    background-color: #02B893;
    border: none;
    border-radius: 2em;
    padding: 1em;
    margin: 0.5em 0;

    &:hover {
        background: rgb(2,184,147);
        background: linear-gradient(90deg, rgba(2,184,147,1) 25%, rgba(39,160,221,1) 75%);
        transition: all 0.1s;
        color: rgba(255, 255, 255, 1);
        box-shadow: 0 5px 15px rgba(2, 184, 147, 0.4);
    }
`;

interface TestProps {
}

const Login: React.FC<TestProps> = (props: TestProps) => {
    const [token, setToken] = useState<string>();

    const onSubmit = async (event :React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        authService.signIn(window.location);
    };

    useEffect(() => {
        const authAsync = async () => {
            //setToken(await authService.getAccessToken());
        }

        authAsync();
    });

    return (
        <Container>
            <h3>{token}</h3>
            <Heading>Přihlášení</Heading>
            <LoginForm onSubmit={onSubmit}>
                <StyledInput type="text" placeholder="Uživatelské jméno"/>
                <StyledInput type="password" placeholder="Heslo"/>
                <ConfirmButton type="submit"/>
            </LoginForm>
        </Container>
    );
}

export default Login;