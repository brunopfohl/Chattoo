import { useRouter } from 'next/router';
import React, { useEffect } from 'react'
import authService, { AuthenticationResult, AuthenticationResultStatus } from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';


const LogoutCallback: React.FC = () => {
    const router = useRouter();
    let callbackUrl = router.asPath;

    useEffect(() => {
        authService.completeSignOut(callbackUrl).then((result: AuthenticationResult) => {
            (result.status === AuthenticationResultStatus.Success) && router.push("/connect-account");
        });
    }, []);

    return (
        <Loading detail="Odhlašuji"/>
    );
}

export default LogoutCallback;