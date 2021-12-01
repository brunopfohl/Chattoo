import React from 'react';
import AppStateProvider from '../components/app-state-provider.component';
import ComponentWrapper from './_component-wrapper';
import '../../styles/globals.css'
import Layout from '@components/layout/layout.component';

const App: React.FC<any> = ({Component, pageProps}) => {
    return (
    <AppStateProvider>
        <Layout>
            <ComponentWrapper Component={Component} pageProps={pageProps} />
        </Layout>
    </AppStateProvider>
    );
};

export default App;