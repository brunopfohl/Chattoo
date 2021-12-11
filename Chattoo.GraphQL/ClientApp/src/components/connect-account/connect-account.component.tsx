import { Button, Container, Paper, Stack, Typography } from '@mui/material';
import { useRouter } from 'next/router';
import { FC, useCallback } from 'react';
import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';

/**
 * Komponenta pro přihlášení/registraci uživatele.
 */
const ConnectAccount: FC = () => {
    // Router je využíván pro přesměrování na stránku s přihlášením/registrací.
    let router = useRouter();

    /** Callback volaný při kliknutí na "Přihlásit" */
    let onLogin = useCallback(() => {
        router.push({
            pathname: ApplicationPaths.Login,
            query: { origin: router.asPath }
        });
    }, []);

    /** Callback volaný při kliknutí na "Registrovat"*/
    let onRegister = useCallback(() => {
        router.push({
            pathname: ApplicationPaths.Register,
            query: { origin: router.asPath }
        });
    }, []);

    return (
        <Container maxWidth="sm">
            {/* Nadpis */}
            <Typography variant="h2" pb={2} align="center">
                Chattoo
            </Typography>
            {/* Prvek s tlačítky pro přihlášení/registraci */}
            <Paper elevation={3} sx={{ padding: 2 }}>
                <Stack alignItems="center" spacing={0.5} sx={{ verticalAlign: "center" }}>
                    <Button onClick={onLogin} size="large" variant="outlined" color="primary" fullWidth>
                        Přihlásit se
                    </Button>
                    <Button onClick={onRegister} size="large" variant="outlined" color="secondary" fullWidth>
                        Registrovat
                    </Button>
                </Stack>
            </Paper>
        </Container>
    );
}

ConnectAccount.displayName = "ConnectAccountComponent";
export default ConnectAccount;