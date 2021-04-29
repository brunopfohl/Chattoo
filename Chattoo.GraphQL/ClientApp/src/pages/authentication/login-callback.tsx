import { useRouter } from 'next/router';
import React from 'react'
import authService, { AuthenticationResult, AuthenticationResultStatus } from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';


const LoginCallback: React.FC = () => {
    const router = useRouter();
    let callbackUrl = router.asPath;


    authService.completeSignIn(callbackUrl).then((result: AuthenticationResult) => {
        console.log(result);
        (result.status === AuthenticationResultStatus.Success) && router.push("/");
    });

    return (
        <Loading detail="Přihlašuji"/>
    );
}

export default LoginCallback;