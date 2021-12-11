import { useRouter } from 'next/router';
import { FC } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import Loading from '../../components/loading/loading.component';

/**
 * Stránka - přesměrování na Identity server registraci.
 */
const Register: FC = () => {
    const router = useRouter();
    const { origin } = router.query;

    authService.signIn(origin);

    return (
        <Loading detail="Přihlašování" />
    );
}

Register.displayName = "RegisterPage";
export default Register;