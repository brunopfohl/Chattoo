import { Event, QuestionMark, SportsBar, RecordVoiceOver, Celebration } from '@mui/icons-material';
import { Box, Button, Container, Divider, Grid, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Paper, Stack, Typography } from "@mui/material";
import { AutocompleteItem } from 'common/interfaces/autocomplete-item.interface';
import { getKeysWithValues } from 'common/utils/enum.utils';
import { CalendarEvent, CalendarEventTypeGraphType, useGetCalendarEventsQuery } from 'graphql/graphql-types';
import { FC, useCallback, useMemo, useState } from "react";
import EventCreatePopup from './create-event-form.component';
import EventCard from "./event-card.component";

const EventsDashboard: FC = () => {
    const [isEventCreatePopupOpenned, setEventCreatePopupOpenned] = useState<boolean>(false);

    const onEventCreatePopupClosed = useCallback(() => {
        setEventCreatePopupOpenned(false);
    }, [setEventCreatePopupOpenned]);

    const showEventCreatePopup = useCallback(() => {
        setEventCreatePopupOpenned(true);
    }, [setEventCreatePopupOpenned]);

    const { data: joinedEventsQuery, refetch: refetchJoinedEvents } = useGetCalendarEventsQuery({
        variables: {
            pageNumber: 1,
            pageSize: 1000
        }
    });

    const joinedEvents = joinedEventsQuery?.calendarEvents?.getJoined?.data;

    const renderTypeIcon = (type: CalendarEventTypeGraphType) => {
        switch (type) {
            case CalendarEventTypeGraphType.Drink: {
                return <SportsBar />
            }
            case CalendarEventTypeGraphType.Celebration: {
                return <Celebration />
            }
            case CalendarEventTypeGraphType.Brainstorming: {
                return <RecordVoiceOver />
            }
            default: {
                <QuestionMark />
            }
        }
    };

    const eventTypes: AutocompleteItem<CalendarEventTypeGraphType>[] = useMemo(() => {
        return getKeysWithValues(CalendarEventTypeGraphType);
    }, []);

    return (
        <>
            <EventCreatePopup onSubmit={refetchJoinedEvents} onClose={onEventCreatePopupClosed} open={isEventCreatePopupOpenned} types={eventTypes} />
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
                                {eventTypes.map(t => (
                                    <ListItem disablePadding key={t.value}>
                                        <ListItemButton sx={{ pl: 0 }}>
                                            <ListItemIcon>
                                                {renderTypeIcon(t.value)}
                                            </ListItemIcon>
                                            <ListItemText>
                                                {t.key}
                                            </ListItemText>
                                        </ListItemButton>
                                    </ListItem>
                                ))}
                            </List>
                        </Paper>
                    </Grid>
                    <Grid item xs={10}>
                        <Container maxWidth="xl" sx={{ pt: 2 }}>
                            <Paper elevation={3} sx={{ p: 2 }}>
                                <Typography variant="h5">
                                    Vaše události
                                </Typography>
                                <Stack direction="row" sx={{ flexWrap: "wrap" }}>
                                    {joinedEvents && joinedEvents.length > 0 && joinedEvents.map(e =>
                                        <EventCard calendarEvent={e as CalendarEvent} key={e.id} />
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