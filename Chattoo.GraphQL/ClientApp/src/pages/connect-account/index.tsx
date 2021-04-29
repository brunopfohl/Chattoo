import Head from 'next/head'
import React from 'react';
import styled from 'styled-components'
import ConnectAccount from '../../components/connect-account/connect-account.component';
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

const ConnectAccountPage = () => {
    return (
        <div>
            <Head>
                <title>Propojit účet - Chattoo</title>
                <link rel="icon" href="/favicon.ico" />
            </Head>
            <RadientMain>
                <ConnectAccount/>
            </RadientMain>
        </div>
    )
};

export default ConnectAccountPage;