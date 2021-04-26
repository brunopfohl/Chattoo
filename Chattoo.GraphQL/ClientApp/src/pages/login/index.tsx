import Head from 'next/head'
import React from 'react';
import styled from 'styled-components'
import Login from '../../components/login.component';

// Lineární gradient pozadí.
const RadientMain = styled.main`
    background: rgb(2,184,147);
    background: linear-gradient(149deg, rgba(2,184,147,1) 0%, rgba(39,160,221,1) 50%, rgba(194,39,194,1) 100%);
    height: 100vh;
    padding: 5vh 10vw 10vh 5vw;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
`;

const LoginPage = () => {
    return (
        <div>
            <Head>
                <title>Chattoo</title>
                <link rel="icon" href="/favicon.ico" />
            </Head>
            <RadientMain>
                <Login/>
            </RadientMain>
        </div>
    )
};

export default LoginPage;