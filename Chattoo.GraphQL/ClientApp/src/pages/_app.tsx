import React from 'react';
import AppStateProvider from '../components/app-state-provider.component';
import ComponentWrapper from './_component-wrapper';
import '../../styles/globals.css'

const App: React.FC<any> = ({Component, pageProps}) => {
    return (
    <AppStateProvider>
        <ComponentWrapper Component={Component} pageProps={pageProps} />
    </AppStateProvider>
    );
};

export default App;