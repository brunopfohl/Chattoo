import React, { useEffect, useState } from 'react'
import { useRouter } from 'next/router'
import authService from './AuthorizeService';
import { AuthenticationResultStatus } from './AuthorizeService';
import { LoginActions, QueryParameterNames, ApplicationPaths } from './ApiAuthorizationConstants';

interface LoginProps {
    action: string
}

export const Login: React.FC<LoginProps> = (props: LoginProps) => {
    const router = useRouter();
    const [message, setMessage] = useState<string | undefined>();
    const { action } = props;

    const getReturnUrl = (): string => {
        return router.query.returnUrl as string;
    }

    const redirectToApiAuthorizationPath = (apiAuthorizationPath) => {
        router.replace(`${window.location.origin}/${apiAuthorizationPath}`)
    }

    const redirectToRegister = () => {
        redirectToApiAuthorizationPath(`${ApplicationPaths.IdentityRegisterPath}?${QueryParameterNames.ReturnUrl}=${encodeURI(ApplicationPaths.Login)}`);
    }

    const redirectToProfile = () => {
        redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath);
    }

    const login = async (returnUrl: string) =>{
        const state = { returnUrl };
        const result = await authService.signIn(state);
        switch (result.status) {
            case AuthenticationResultStatus.Redirect:
                break;
            case AuthenticationResultStatus.Success:
                await router.replace(returnUrl);
                break;
            case AuthenticationResultStatus.Fail:
                setMessage(result.message);
                break;
            default:
                throw new Error(`Invalid status result ${result.status}.`);
        }
    }

    const processLoginCallback = async() => {
        const result = await authService.completeSignIn(router.route);

        switch (result.status) {
            case AuthenticationResultStatus.Redirect:
                throw new Error('Should not redirect.');
            case AuthenticationResultStatus.Success:
                await router.replace(getReturnUrl());
                break;
            case AuthenticationResultStatus.Fail:
                setMessage(result.message);
                break;
            default:
                throw new Error(`Invalid authentication result status '${result.status}'.`);
        }
    }

    useEffect(() => {
        switch (action) {
            case LoginActions.Login:
                login(getReturnUrl());
                break;
            case LoginActions.LoginCallback:
                processLoginCallback();
                break;
            case LoginActions.LoginFailed:
                setMessage(router.query.message as string);
                break;
            case LoginActions.Profile:
                redirectToProfile();
                break;
            case LoginActions.Register:
                redirectToRegister();
                break;
            default:
                throw new Error(`Invalid action '${action}'`);
        }
    });

    if (!!message) {
        return <div>{message}</div>
    } else {
        switch (action) {
            case LoginActions.Login:
                return (<div>Processing login</div>);
            case LoginActions.LoginCallback:
                return (<div>Processing login callback</div>);
            case LoginActions.Profile:
            case LoginActions.Register:
                return (<div></div>);
            default:
                throw new Error(`Invalid action '${action}'`);
        }
    }
};