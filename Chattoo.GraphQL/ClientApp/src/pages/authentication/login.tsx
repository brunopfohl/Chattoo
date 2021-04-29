import { useRouter } from 'next/router';
import React from 'react'
import authService from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';


const Login: React.FC = () => {
    const router = useRouter();
    const { origin } = router.query;

    authService.signIn(origin);

    return (
        <Loading detail="Přihlašování"/>
    );
}

export default Login;