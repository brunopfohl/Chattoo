import { useRouter } from 'next/router';
import { FC, useEffect } from 'react'
import authService, { AuthenticationResult, AuthenticationResultStatus } from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';

/**
 * Stránka - přesměrování zpět z Identity serveru po odhlášení.
 */
const LogoutCallback: FC = () => {
    const router = useRouter();
    let callbackUrl = router.asPath;

    useEffect(() => {
        authService.completeSignOut(callbackUrl).then((result: AuthenticationResult) => {
            (result.status === AuthenticationResultStatus.Success) && router.push("/connect-account");
        });
    }, []);

    return (
        <Loading detail="Odhlašuji" />
    );
}

LogoutCallback.displayName = "LogoutCallbackPage";
export default LogoutCallback;