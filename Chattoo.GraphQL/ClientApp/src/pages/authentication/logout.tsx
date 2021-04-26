import React from 'react'
import authService from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';

interface LogoutRedirectProps {
    origin: string
}

const Logout: React.FC<LogoutRedirectProps> = (props: LogoutRedirectProps) => {
    authService.signOut(props.origin);
    return (
        <Loading detail="Odhlašování"/>
    );
}

export default Logout;