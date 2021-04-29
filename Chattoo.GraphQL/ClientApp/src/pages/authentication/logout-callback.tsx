import { useRouter } from 'next/router';
import React from 'react'
import authService, { AuthenticationResult } from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';


const LogoutCallback: React.FC = () => {
    const router = useRouter();
    let callbackUrl = router.asPath;

    authService.completeSignOut(callbackUrl).then((result: AuthenticationResult) => {
        
    });

    return (
        <Loading detail="Odhlašuji"/>
    );
}

export default LogoutCallback;