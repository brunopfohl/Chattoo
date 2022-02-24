import { MoreHoriz, People, Person, Place } from '@mui/icons-material';
import { Button, Card, CardContent, CardMedia, Container, Divider, Icon, List, ListItem, ListItemIcon, ListItemText, Paper, Stack, Typography } from "@mui/material";
import { CalendarEvent, User } from "graphql/graphql-types";
import moment from "moment";
import { FC, useMemo } from "react";

export interface EventDetailProps {
    calendarEvent: CalendarEvent
    participants: User[]
}

const EventDetail: FC<EventDetailProps> = (props) => {
    const { calendarEvent, participants } = props;

    const dateText = useMemo(() => {
        let result = moment(calendarEvent.startsAt).format("d. MM. YYYY - hh:mm");

        if (calendarEvent.endsAt) {
            const endsAt = moment(calendarEvent.endsAt).format("d. MM. YYYY - hh:mm");
            result = `Od ${result} do ${endsAt}`;
        }

        return result;
    }, [calendarEvent]);

    return (
        <>
            <Paper sx={{ mt: 2, p: 2 }}>
                <Container maxWidth="md">
                    <CardMedia
                        component="img"
                        alt="green iguana"
                        height="140"
                        image="/event.png"
                        sx={{ objectFit: "cover", borderRadius: "5px" }}
                    />
                    <Typography variant="h6" color="secondary">
                        {dateText}
                    </Typography>
                    <Typography variant="h2">
                        {calendarEvent.name}
                    </Typography>
                    <Divider />
                    <Stack direction="row-reverse" spacing={1} sx={{ pt: 1 }}>
                        <Button variant="outlined">
                            <Icon>
                                <MoreHoriz />
                            </Icon>
                        </Button>
                        <Button variant="outlined">Zúčastnit se</Button>
                        <Button variant="outlined">Zrušit účast</Button>
                    </Stack>
                </Container>
            </Paper>

            <Container maxWidth="md" sx={{ mt: 1 }}>
                <Card>
                    <CardContent>
                        <Typography gutterBottom variant="h5" component="div">
                            Podrobnosti
                        </Typography>
                        <List>
                            <ListItem disablePadding sx={{ mt: 1 }}>
                                <ListItemIcon>
                                    <People />
                                </ListItemIcon>
                                <ListItemText>
                                    Počet lidí: 9/10
                                </ListItemText>
                            </ListItem>
                            <ListItem disablePadding sx={{ mt: 1, mb: 1 }}>
                                <ListItemIcon>
                                    <Place />
                                </ListItemIcon>
                                <ListItemText>
                                    Liberec, Rochlice 12345
                                </ListItemText>
                            </ListItem>
                        </List>
                        <Typography gutterBottom variant="body2">
                            {calendarEvent.description}
                        </Typography>
                    </CardContent>
                </Card>
            </Container>

            <Container maxWidth="md" sx={{ mt: 1 }}>
                <Card>
                    <CardContent>
                        <Typography gutterBottom variant="h5" component="div">
                            Seznam uživatelů
                        </Typography>
                        <List>
                            {participants.map(p => (
                                <ListItem disablePadding key={p.id}>
                                    <ListItemIcon>
                                        <Person />
                                    </ListItemIcon>
                                    <ListItemText>
                                        {p.userName}
                                    </ListItemText>
                                </ListItem>
                            ))}
                        </List>
                    </CardContent>
                </Card>
            </Container>
        </>
    );
};

EventDetail.displayName = "EventDetailComponent";
export default EventDetail;