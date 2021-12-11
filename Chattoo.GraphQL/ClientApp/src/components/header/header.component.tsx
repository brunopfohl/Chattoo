import { MenuOutlined } from '@mui/icons-material';
import { AppBar, Button, IconButton, Stack, Toolbar, Typography, } from '@mui/material';
import { useRouter } from 'next/router';
import { FC, useCallback, useContext } from 'react'
import authService from '../api-authorization/AuthorizeService';
import { AppStateContext } from '../app-state-provider.component';

/**
 * Komponenta sloužící jako hlavička stránky
 */
const Header: FC = () => {
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    // Router pro zjištění aktuálně navštívené stránky.
    const router = useRouter();

    /** Callback vyvolávaný po kliknutí na "Odhlásit" */
    let onLogout = useCallback(() => {
        authService.signOut(router.asPath);
    }, []);

    return (
        <>
            {/* Navigační lišta */}
            <AppBar position="fixed" sx={{ color: "white" }}>
                <Toolbar variant="dense">

                    {/* Hamburger menu */}
                    <IconButton edge="start" color="inherit" aria-label="menu" sx={{ mr: 2 }}>
                        <MenuOutlined />
                    </IconButton>

                    {/* Nadpis */}
                    <Typography variant="h6" color="inherit" component="div">
                        The chattoo
                    </Typography>

                    <Stack direction="row" sx={{ flexGrow: 1, justifyContent: "flex-end" }}>
                        {/* Jméno přihlášeného uživatele */}
                        <Typography variant="subtitle1" color="inherit" align="center" mr={1}>
                            {user && user.userName}
                        </Typography>

                        {/* Tlačítko pro odhlášení */}
                        <Button size="small" variant="contained" color="secondary" onClick={onLogout}>Odhlásit se</Button>
                    </Stack>
                </Toolbar>
            </AppBar>
        </>
    );
}

Header.displayName = "HeaderComponent";
export default Header;