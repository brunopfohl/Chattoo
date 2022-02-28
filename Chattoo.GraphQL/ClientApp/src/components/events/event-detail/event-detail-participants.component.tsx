import UsersManagePopup from "@components/users/users-manage.popup.component";
import { useSetter } from "@hooks/useSetter";
import { Person } from "@mui/icons-material";
import { Container, Card, CardContent, Stack, Typography, Button, List, ListItem, ListItemIcon, ListItemText } from "@mui/material";
import { CalendarEvent, User } from "graphql/graphql-types";
import { FunctionComponent, useState } from "react";

interface EventDetailParticipantsProps {
    calendarEvent: CalendarEvent;
    canEdit: boolean;
    participants: User[];
    saveUsers: (users: User[]) => void;
    removeUser: (user: User) => void;
}

const EventDetailParticipants: FunctionComponent<EventDetailParticipantsProps> = (props) => {
    const { calendarEvent, canEdit, participants, saveUsers, removeUser } = props;

    const [showUsersManagePopup, setShowUsersManagePopup] = useState<boolean>(false);

    const openUsersManage = useSetter(setShowUsersManagePopup, true);
    const closeUsersManage = useSetter(setShowUsersManagePopup, false);

    return (
        <>
            <UsersManagePopup open={showUsersManagePopup} onClose={closeUsersManage} users={participants} onUserRemoved={removeUser} onSubmit={saveUsers} channelId={calendarEvent.communicationChannelId} groupId={calendarEvent.groupId} />
            <Container maxWidth="md" sx={{ mt: 1 }}>
                <Card>
                    <CardContent>
                        <Stack direction="row" sx={{ justifyContent: "space-between" }}>
                            <Typography gutterBottom variant="h5" component="div">
                                Seznam uživatelů
                            </Typography>
                            {canEdit &&
                                <Button onClick={openUsersManage}>Spravovat účastníky</Button>
                            }
                        </Stack>
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
}

export default EventDetailParticipants;