import { useRouter } from 'next/router';
import React from 'react'
import authService, { AuthenticationResult, AuthenticationResultStatus } from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';


const LoginCallback: React.FC = () => {
    let callbackUrl = window.location.href;

    let router = useRouter();

    authService.completeSignIn(callbackUrl).then((result: AuthenticationResult) => {
        console.log(result);
        if (result.state === AuthenticationResultStatus.Success) {
            router.push("/");
        }
    });

    return (
        <Loading detail="Přihlašuji"/>
    );
}

export default LoginCallback;