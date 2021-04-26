import { useRouter } from 'next/router';
import React from 'react'
import authService, { AuthenticationResult } from '../../components/api-authorization/AuthorizeService';


const LogoutCallback: React.FC = () => {
    let callbackUrl = window.location.href;

    authService.completeSignOut(callbackUrl).then((result: AuthenticationResult) => {
        console.log(result.message)
    });

    return (
        <div>
            Processing logout
        </div>
    );
}

export default LogoutCallback;