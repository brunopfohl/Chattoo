import { FC } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';

interface LogoutRedirectProps {
    origin: string
}

/**
 * Stránka - přesměrování na Identity server pro ohdlášení.
 */
const Logout: FC<LogoutRedirectProps> = (props) => {
    authService.signOut(props.origin);
    return (
        <Loading detail="Odhlašování" />
    );
}

Logout.displayName = "LogoutPage";
export default Logout;