import React from 'react'
import authService from '../../components/api-authorization/AuthorizeService';

interface LogoutRedirectProps {
    origin: string
}

const Logout: React.FC<LogoutRedirectProps> = (props: LogoutRedirectProps) => {
    authService.signOut(props.origin);
    return (
        <div>
            Redirecting to logou
        </div>
    );
}

export default Logout;