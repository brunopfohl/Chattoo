import Loading from '@components/loading/loading.component';
import Head from 'next/head'
import React from 'react';
import ConnectAccount from '../../components/connect-account/connect-account.component';

const ConnectAccountPage = () => {
    return (
        <div>
            <Head>
                <title>Propojit účet - Chattoo</title>
            </Head>
            {/* background */}
            <div>
                <ConnectAccount/>
            </div>
        </div>
    )
};

export default ConnectAccountPage;