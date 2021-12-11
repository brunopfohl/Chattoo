import { useRouter } from 'next/router';
import { FC, useEffect } from 'react'
import authService, { AuthenticationResult, AuthenticationResultStatus } from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';

/**
 * Stránka - navrácení zpět z Identity serveru.
 */
const LoginCallback: FC = () => {
    const router = useRouter();
    const callbackUrl = router.asPath;

    useEffect(() => {
        authService.completeSignIn(callbackUrl).then((result: AuthenticationResult) => {
            (result.status === AuthenticationResultStatus.Success) && router.push("/");
        });
    }, []);

    return (
        <Loading detail="Přihlašuji" />
    );
}

LoginCallback.displayName = "LoginCallbackPage";
export default LoginCallback;