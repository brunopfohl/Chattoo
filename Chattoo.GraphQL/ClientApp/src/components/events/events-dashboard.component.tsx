import { Event, SportsBar, RecordVoiceOver, Celebration } from '@mui/icons-material';
import { Box, Button, Container, Divider, Grid, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Paper, Stack, Typography } from "@mui/material";
import { FC, useCallback, useState } from "react";
import EventCreatePopup from './create-event-form.component';
import EventCard, { EventCardProps } from "./event-card.component";

const EventsDashboard: FC = () => {
    const [isEventCreatePopupOpenned, setEventCreatePopupOpenned] = useState<boolean>(false);

    const onEventCreatePopupClosed = useCallback(() => {
        setEventCreatePopupOpenned(false);
    }, [setEventCreatePopupOpenned]);

    const showEventCreatePopup = useCallback(() => {
        setEventCreatePopupOpenned(true);
    }, [setEventCreatePopupOpenned]);

    let events: EventCardProps[] = [
        {
            title: "Událost 1",
            description: "Událost je super!",
            startsAt: new Date(),
            participantsCount: 1,
            maximalParticipantsCount: 1
        },
        {
            title: "Událost 2",
            description: "Událost je super!",
            startsAt: new Date(),
            participantsCount: 1,
            maximalParticipantsCount: 1
        },
        {
            title: "Událost 3",
            description: "Událost je super!",
            startsAt: new Date(),
            participantsCount: 1,
            maximalParticipantsCount: 1
        }
    ];

    return (
        <>
            <EventCreatePopup onClose={onEventCreatePopupClosed} open={isEventCreatePopupOpenned} />
            <Box sx={{ height: "100%", width: "100%", pt: 1 }}>
                <Grid container sx={{ width: "100%", height: "100%" }}>
                    <Grid item xs={2}>
                        <Paper elevation={0} sx={{ borderRadius: 0, height: "100%", p: 2, boxShadow: "5px 0 5px -2px #cccccc" }}>
                            <Typography variant="h4">
                                Události
                            </Typography>
                            <List>
                                <ListItem disablePadding>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <Event />
                                        </ListItemIcon>
                                        <ListItemText>
                                            Dostupné události
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                                <ListItem disablePadding>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <Event />
                                        </ListItemIcon>
                                        <ListItemText>
                                            Vaše události
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                                <ListItem disablePadding>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <Event />
                                        </ListItemIcon>
                                        <ListItemText>
                                            Vaše přání
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                            </List>
                            <Button fullWidth variant="outlined" sx={{ mb: 1 }} onClick={showEventCreatePopup}>Vytvořit událost</Button>
                            <Button fullWidth variant="outlined" color="secondary">Vytvořit přání</Button>

                            <Divider variant="middle" sx={{ mb: 2, mt: 2, ml: 0, width: "100%" }} />

                            <Typography variant="h6">
                                Kategorie
                            </Typography>
                            <List>
                                <ListItem disablePadding>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <SportsBar />
                                        </ListItemIcon>
                                        <ListItemText>
                                            Pivo
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                                <ListItem disablePadding>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <Celebration />
                                        </ListItemIcon>
                                        <ListItemText>
                                            Oslava
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                                <ListItem disablePadding>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <RecordVoiceOver />
                                        </ListItemIcon>
                                        <ListItemText>
                                            Pokec
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                            </List>
                        </Paper>
                    </Grid>
                    <Grid item xs={10}>
                        <Container maxWidth="xl" sx={{ pt: 2 }}>
                            <Paper elevation={3} sx={{ p: 2 }}>
                                <Typography variant="h5">
                                    Vaše události
                                </Typography>
                                <Stack direction="row">
                                    {events.length > 0 && events.map(e =>
                                        <EventCard {...e} />
                                    )}
                                </Stack>
                            </Paper>
                        </Container>
                    </Grid>
                </Grid>
            </Box>
        </>
    );
};

EventsDashboard.displayName = "EventsDashboardComponent";
export default EventsDashboard;