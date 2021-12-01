import { Box, Button, Container, Paper, Stack, SxProps, Typography } from '@mui/material';
import { useRouter } from 'next/router';
import React from 'react'
import { ApplicationPaths } from '../api-authorization/ApiAuthorizationConstants';
import { PersonOutlineOutlined } from '@mui/icons-material';

const ConnectAccount: React.FC<any> = () => {
    let router = useRouter();

    let onLogin = () => {
        router.push({
            pathname: ApplicationPaths.Login,
            query: { origin: router.asPath }
        });
    };

    let onRegister = () => {
        router.push({
            pathname: ApplicationPaths.Register,
            query: { origin: router.asPath }
        });
    };

    return (
        <Container maxWidth="sm">
            <Typography variant="h2" pb={2} align="center">
                Chattoo
            </Typography>
            <Paper elevation={3} sx={{ padding: 2 }}>
                <Stack alignItems="center" spacing={0.5} sx={{verticalAlign: "center"}}>
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

export default ConnectAccount;