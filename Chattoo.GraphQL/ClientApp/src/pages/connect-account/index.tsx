import Head from 'next/head'
import React, { FC } from 'react';
import ConnectAccount from '../../components/connect-account/connect-account.component';

/**
 * Stránka - úvodní stránka bez přihlášeného uživatele.
*/
const ConnectAccountPage: FC = () => (
    <div>
        <Head>
            <title>Propojit účet - Chattoo</title>
        </Head>
        <div>
            <ConnectAccount />
        </div>
    </div>
);

ConnectAccountPage.displayName = "ConnectAccountPage";
export default ConnectAccountPage;