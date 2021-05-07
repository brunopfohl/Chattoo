
import React from 'react';
import AppStateProvider from '../../components/app-state-provider.component';
import App from '../_app';

const AppWithProvider: React.FC<any> = (props) => {
    return (
    <AppStateProvider>
        <App {...props} />
    </AppStateProvider>
    );
};

export default AppWithProvider;