import React from 'react'
import authService from '../../components/api-authorization/AuthorizeService';

interface LoginRedirectProps {
    origin: string
}

const Login: React.FC<LoginRedirectProps> = (props: LoginRedirectProps) => {
    authService.signIn(props.origin);
    return (
        <div>
            Redirecting to login
        </div>
    );
}

export default Login;