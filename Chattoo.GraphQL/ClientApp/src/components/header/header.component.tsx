import { Chat, MenuOutlined, AccountBox, Event } from '@mui/icons-material';
import { AppBar, Button, Drawer, IconButton, List, ListItem, ListItemIcon, ListItemText, Stack, Toolbar, Typography, } from '@mui/material';
import { useRouter } from 'next/router';
import { FC, useCallback, useContext, useState } from 'react'
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

    var [isDrawerOpenned, setIsDrawerOpenned] = useState<boolean>(false);

    const onDrawerOpen = useCallback(() => {
        setIsDrawerOpenned(true);
    }, [setIsDrawerOpenned]);

    const onDrawerClosed = useCallback(() => {
        setIsDrawerOpenned(false);
    }, [setIsDrawerOpenned]);

    return (
        <>
            {/* Navigační lišta */}
            <AppBar position="fixed" sx={{ color: "white" }}>
                <Toolbar variant="dense">

                    {/* Hamburger menu */}
                    <IconButton
                        edge="start"
                        color="inherit"
                        aria-label="menu"
                        sx={{ mr: 2 }}
                        onClick={onDrawerOpen}
                    >
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

                    <Drawer open={isDrawerOpenned} onClose={onDrawerClosed}>
                        <List>
                            <ListItem button>
                                <ListItemIcon>
                                    <Chat />
                                </ListItemIcon>
                                <ListItemText primary="Chat" />
                            </ListItem>

                            <ListItem button>
                                <ListItemIcon>
                                    <Event />
                                </ListItemIcon>
                                <ListItemText primary="Události" />
                            </ListItem>

                            <ListItem button>
                                <ListItemIcon>
                                    <AccountBox />
                                </ListItemIcon>
                                <ListItemText primary="Profil" />
                            </ListItem>
                        </List>
                    </Drawer>
                </Toolbar>
            </AppBar>
        </>
    );
}

Header.displayName = "HeaderComponent";
export default Header;