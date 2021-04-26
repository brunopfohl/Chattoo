import { useRouter } from 'next/router';
import React from 'react'
import authService, { AuthenticationResult } from '../../components/api-authorization/AuthorizeService';


const LoginCallback: React.FC = () => {
    let callbackUrl = window.location.href;

    authService.completeSignIn(callbackUrl).then((result: AuthenticationResult) => {
        console.log(result.message)
    });

    return (
        <div>
            Processing login
        </div>
    );
}

export default LoginCallback;