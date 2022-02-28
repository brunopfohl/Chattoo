import { Group, People, Place } from "@mui/icons-material";
import { Card, CardContent, Container, List, ListItem, ListItemIcon, ListItemText, Typography } from "@mui/material";
import { CalendarEvent, CommunicationChannel } from "graphql/graphql-types";
import { FC } from "react";

interface EventDetailInfoProps {
    calendarEvent: CalendarEvent;
    channel?: CommunicationChannel | null;
}

const EventDetailInfo: FC<EventDetailInfoProps> = (props) => {
    const { calendarEvent, channel } = props;

    return (
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
                                {!!calendarEvent.maximalParticipantsCount
                                    ? `${calendarEvent.participantsCount} / ${calendarEvent.maximalParticipantsCount}`
                                    : `${calendarEvent.participantsCount}`
                                }
                            </ListItemText>
                        </ListItem>
                        {channel &&
                            <ListItem disablePadding sx={{ mt: 1 }}>
                                <ListItemIcon>
                                    <Group />
                                </ListItemIcon>
                                <ListItemText>
                                    {channel.name}
                                </ListItemText>
                            </ListItem>
                        }
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
    );
}

export default EventDetailInfo;