import React from 'react'
import authService from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';

interface LoginRedirectProps {
    origin: string
}

const Login: React.FC<LoginRedirectProps> = (props: LoginRedirectProps) => {
    authService.signIn(props.origin);
    return (
        <Loading detail="Přihlašování"/>
    );
}

export default Login;