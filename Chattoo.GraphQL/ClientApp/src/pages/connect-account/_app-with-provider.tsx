
import { FC } from 'react';
import AppStateProvider from '../../components/app-state-provider.component';
import App from '../_app';

const AppWithProvider: FC<any> = (props) => (
    <AppStateProvider>
        <App {...props} />
    </AppStateProvider>
);

export default AppWithProvider;