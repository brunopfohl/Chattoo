import { Chat, MenuOutlined, AccountBox, Event } from '@mui/icons-material';
import { AppBar, Button, Drawer, IconButton, List, ListItem, ListItemIcon, ListItemText, Stack, Toolbar, Typography, } from '@mui/material';
import { Page } from 'common/enums/page.enum';
import { useRouter } from 'next/router';
import { Dispatch, FC, SetStateAction, useCallback, useContext, useState } from 'react'
import authService from '../api-authorization/AuthorizeService';
import { AppStateContext } from '../app-state-provider.component';

interface HeaderProps {
    setPage: Dispatch<SetStateAction<Page>>;
}

/**
 * Komponenta sloužící jako hlavička stránky
 */
const Header: FC<HeaderProps> = (props) => {
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    const { setPage } = props;

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

    const setPageSafe = useCallback((page: Page) => {
        onDrawerClosed();
        setPage(page);
    }, [setPage]);

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
                            <ListItem button onClick={() => setPageSafe(Page.Chat)}>
                                <ListItemIcon>
                                    <Chat />
                                </ListItemIcon>
                                <ListItemText primary="Chat" />
                            </ListItem>

                            <ListItem button>
                                <ListItemIcon onClick={() => setPageSafe(Page.Events)}>
                                    <Event />
                                </ListItemIcon>
                                <ListItemText primary="Události" />
                            </ListItem>

                            <ListItem button>
                                <ListItemIcon onClick={() => setPageSafe(Page.Profile)}>
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