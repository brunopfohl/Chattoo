import AppStateProvider from '../components/app-state-provider.component';
import ComponentWrapper from './_component-wrapper';
import '../../styles/globals.css'
import Layout from '@components/layout/layout.component';
import { FC } from 'react';

/** Komponenta - root aplikace */
const App: FC<any> = ({ Component, pageProps }) => (
    <AppStateProvider>
        <Layout>
            <ComponentWrapper Component={Component} pageProps={pageProps} />
        </Layout>
    </AppStateProvider>
);

App.displayName = "AppComponent";
export default App;