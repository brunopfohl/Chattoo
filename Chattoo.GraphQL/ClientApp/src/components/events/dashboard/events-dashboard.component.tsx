import { useCalendarEventTypeIconRenderer } from "@hooks/useCalendarTypeIcon";
import { useSetter } from "@hooks/useSetter";
import { Add, AddCircle, AllInclusive, Circle, Create, Event, EventAvailable } from "@mui/icons-material";
import { Box, Grid, Paper, Typography, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Button, Divider, Container, Stack, IconButton, Icon } from "@mui/material";
import { AutocompleteItem } from "common/interfaces/autocomplete-item.interface";
import { getKeysWithValues } from "common/utils/enum.utils";
import { CalendarEventTypeGraphType, CalendarEvent } from "graphql/graphql-types";
import { useRouter } from "next/router";
import { FC, useState, useMemo, useCallback } from "react";
import EventCreatePopup from "../create-event-form.component";
import EventCard from "../event-card.component";

export enum EventsDashboardMode {
    All,
    Joined,
    Wishes
}

interface EventsDashboardProps {
    events: CalendarEvent[]
    refetchEvents: () => void;
    mode: EventsDashboardMode;
}

const EventsDashboard: FC<EventsDashboardProps> = (props) => {
    const { events, refetchEvents, mode } = props;

    const router = useRouter();

    const [isEventCreatePopupOpenned, setEventCreatePopupOpenned] = useState<boolean>(false);

    const onEventCreatePopupClosed = useSetter(setEventCreatePopupOpenned, false);
    const showEventCreatePopup = useSetter(setEventCreatePopupOpenned, true);

    const [typeFilter, setTypeFilter] = useState<CalendarEventTypeGraphType | null>(null);

    const disableTypeFilter = useCallback(() => {
        setTypeFilter(null);
    }, [setTypeFilter]);

    const showJoinedEvents = useCallback(() => {
        router.push("/events/joined");
    }, [router]);

    const showVisibleEvents = useCallback(() => {
        router.push("/events");
    }, [router]);

    const showWishes = useCallback(() => {

        router.push("/wishes");
    }, [router]);

    const eventTypes: AutocompleteItem<CalendarEventTypeGraphType>[] = useMemo(() => {
        return getKeysWithValues(CalendarEventTypeGraphType);
    }, []);

    const renderIcon = useCalendarEventTypeIconRenderer();

    return (
        <>
            <EventCreatePopup onSubmit={refetchEvents} onClose={onEventCreatePopupClosed} open={isEventCreatePopupOpenned} types={eventTypes} />
            <Box sx={{ height: "100%", width: "100%", pt: 1 }}>
                <Grid container sx={{ width: "100%", height: "100%" }}>
                    <Grid item xs={2}>
                        <Paper elevation={0} sx={{ borderRadius: 0, height: "100%", p: 2, boxShadow: "5px 0 5px -2px #cccccc" }}>
                            <Typography variant="h4" sx={{ display: { xs: "none", sm: "none", md: "block" } }}>
                                Události
                            </Typography>
                            <List>
                                <ListItem disablePadding selected={mode === EventsDashboardMode.All} onClick={showVisibleEvents}>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <Event />
                                        </ListItemIcon>
                                        <ListItemText sx={{
                                            display: {
                                                xs: "none", sm: "none", md: "block"
                                            }
                                        }}>
                                            Dostupné události
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                                <ListItem disablePadding selected={mode === EventsDashboardMode.Joined} onClick={showJoinedEvents}>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <EventAvailable />
                                        </ListItemIcon>
                                        <ListItemText sx={{
                                            display: {
                                                xs: "none", sm: "none", md: "block"
                                            }
                                        }}>
                                            Vaše události
                                        </ListItemText>
                                    </ListItemButton>
                                </ListItem>
                            </List>

                            <Button fullWidth variant="outlined" sx={{ mb: 1, display: { xs: "none", sm: "none", md: "block" } }} onClick={showEventCreatePopup}>Vytvořit událost</Button>
                            <IconButton sx={{ p: 0 }} onClick={showEventCreatePopup}>
                                <Icon>
                                    <AddCircle color="primary" sx={{ display: { xs: "block", sm: "block", md: "none" } }} />
                                </Icon>
                            </IconButton>

                            <Divider variant="middle" sx={{ mb: 2, mt: 2, ml: 0, width: "100%" }} />

                            <Typography variant="h6" sx={{ display: { xs: "none", sm: "none", md: "block" } }}>
                                Kategorie
                            </Typography>
                            <List>
                                <ListItem disablePadding selected={typeFilter === null} onClick={disableTypeFilter}>
                                    <ListItemButton sx={{ pl: 0 }}>
                                        <ListItemIcon>
                                            <AllInclusive />
                                        </ListItemIcon>
                                        <ListItemText sx={{ display: { xs: "none", sm: "none", md: "block" } }}>Vše</ListItemText>
                                    </ListItemButton>
                                </ListItem>
                                {eventTypes.map(t => (
                                    <ListItem disablePadding key={t.value} selected={t.value === typeFilter} onClick={() => setTypeFilter(t.value)}>
                                        <ListItemButton sx={{ pl: 0 }}>
                                            <ListItemIcon>
                                                {renderIcon(t.value)}
                                            </ListItemIcon>
                                            <ListItemText sx={{ display: { xs: "none", sm: "none", md: "block" } }}>
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
                                    {events && events.length > 0 && events.filter(e => typeFilter === null || e.type == typeFilter).map(e =>
                                        <EventCard calendarEvent={e as CalendarEvent} key={e.id} />
                                    )}
                                </Stack>
                            </Paper>
                        </Container>
                    </Grid>
                </Grid>
            </Box >
        </>
    );
};

EventsDashboard.displayName = "EventsDashboardComponent";
export default EventsDashboard;