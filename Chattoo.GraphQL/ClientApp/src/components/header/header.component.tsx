import { Button } from '@mui/material';
import { useRouter } from 'next/router';
import React, { useContext } from 'react'
import authService from '../api-authorization/AuthorizeService';
import { AppStateContext } from '../app-state-provider.component';

const Header: React.FC<any> = (props: any) => {
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    const router = useRouter();

    let onLogout = () => {
        authService.signOut(router.asPath);
    };

    return (
        <div>
            <div>
                <span>{user && user.userName}</span>
            </div>
            <div>
                <Button onClick={onLogout}>
                    Odhlásit se
                </Button>
            </div>
        </div>
    );
}

export default Header;