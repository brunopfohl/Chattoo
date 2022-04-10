import { Chat, MenuOutlined, AccountBox, Event, AutoAwesome } from '@mui/icons-material';
import { AppBar, Button, Drawer, IconButton, List, ListItem, ListItemIcon, ListItemText, Stack, Toolbar, Typography, } from '@mui/material';
import { useRouter } from 'next/router';
import { FC, useCallback, useContext, useState } from 'react'
import authService from '../api-authorization/AuthorizeService';
import { AppStateContext } from '../app-state-provider.component';
import NextLink from "next/link";

interface HeaderProps {
}

/**
 * Komponenta sloužící jako hlavička stránky
 */
const Header: FC<HeaderProps> = (props) => {
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
            <AppBar position="fixed" sx={{ color: "black", backgroundColor: "white" }}>
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
                        Chattoo
                    </Typography>

                    <Stack direction="row" sx={{ flexGrow: 1, justifyContent: "flex-end" }}>
                        {/* Tlačítko pro odhlášení */}
                        <Button size="small" variant="outlined" color="primary" onClick={onLogout}>Odhlásit se</Button>
                    </Stack>

                    <Drawer open={isDrawerOpenned} onClose={onDrawerClosed}>
                        <List>
                            <NextLink href="/">
                                <ListItem button>
                                    <ListItemIcon>
                                        <Chat />
                                    </ListItemIcon>
                                    <ListItemText primary="Chat" />
                                </ListItem>
                            </NextLink>

                            <NextLink href="/events">
                                <ListItem button>
                                    <ListItemIcon>
                                        <Event />
                                    </ListItemIcon>
                                    <ListItemText primary="Události" />
                                </ListItem>
                            </NextLink>

                            <NextLink href="/wishes">
                                <ListItem button>
                                    <ListItemIcon>
                                        <AutoAwesome />
                                    </ListItemIcon>
                                    <ListItemText primary="Přání" />
                                </ListItem>
                            </NextLink>

                            <NextLink href="/profile">
                                <ListItem button>
                                    <ListItemIcon>
                                        <AccountBox />
                                    </ListItemIcon>
                                    <ListItemText primary="Profil" />
                                </ListItem>
                            </NextLink>
                        </List>
                    </Drawer>
                </Toolbar>
            </AppBar>
        </>
    );
}

Header.displayName = "HeaderComponent";
export default Header;