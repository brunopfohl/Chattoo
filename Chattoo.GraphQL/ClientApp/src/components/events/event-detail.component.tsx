import { AppStateContext } from '@components/app-state-provider.component';
import ConfirmDialog from '@components/confirm-dialog.component';
import UsersManagePopup from '@components/users/users-manage.popup.component';
import { Delete, People, Person, Place } from '@mui/icons-material';
import { Button, Card, CardContent, CardMedia, Container, Divider, List, ListItem, ListItemIcon, ListItemText, Paper, Stack, Typography } from "@mui/material";
import { CalendarEvent, useAddUserToCalendarEventMutation, User, useRemoveUserFromCalendarEventMutation } from "graphql/graphql-types";
import moment from "moment";
import { FC, useCallback, useContext, useMemo, useState } from "react";

export interface EventDetailProps {
    calendarEvent: CalendarEvent;
    participants: User[];
    canEdit: boolean;
    refetchParticipants: () => void;
}

const EventDetail: FC<EventDetailProps> = (props) => {
    const { calendarEvent, participants, refetchParticipants, canEdit } = props;
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    const [showUsersManagePopup, setShowUsersManagePopup] = useState<boolean>(false);

    const [showDeleteDialog, setShowDeleteDialog] = useState<boolean>(false);

    const dateText = useMemo(() => {
        let result = moment(calendarEvent.startsAt).format("d. MM. YYYY - hh:mm");

        if (calendarEvent.endsAt) {
            const endsAt = moment(calendarEvent.endsAt).format("d. MM. YYYY - hh:mm");
            result = `Od ${result} do ${endsAt}`;
        }

        return result;
    }, [calendarEvent]);

    const isJoined = useMemo(() => {
        return participants.some(p => p.id === user?.id)
    }, [participants]);

    const onEventDeleteButtonClicked = useCallback(() => {
        setShowDeleteDialog(true);
    }, [setShowDeleteDialog]);

    const hideEventDeleteDialog = useCallback(() => {
        setShowDeleteDialog(false);
    }, [setShowDeleteDialog]);

    const onDeleteConfirmed = useCallback(() => {
        // TODO: Smazání eventu.

        hideEventDeleteDialog();
    }, []);

    const [addUserToCalendarEvent] = useAddUserToCalendarEventMutation();
    const onUserAdd = (user: User) => {
        addUserToCalendarEvent({
            variables: {
                userId: user.id,
                eventId: calendarEvent.id
            }
        });
    };

    const [removeUserFromCalendarEvent] = useRemoveUserFromCalendarEventMutation();
    const onUserRemoved = useCallback((user: User) => {
        removeUserFromCalendarEvent({
            variables: {
                userId: user.id,
                eventId: calendarEvent.id
            }
        }).then(() => {
            refetchParticipants();
        });
    }, [calendarEvent, refetchParticipants]);

    const onUsersManageSubmit = useCallback((users: User[]) => {
        users.forEach(onUserAdd);
        refetchParticipants();
    }, [refetchParticipants]);

    const openUsersManage = useCallback(() => {
        setShowUsersManagePopup(true);
    }, [setShowUsersManagePopup]);

    const closeUsersManage = useCallback(() => {
        setShowUsersManagePopup(false);
    }, [setShowUsersManagePopup]);

    return (
        <>
            <UsersManagePopup open={showUsersManagePopup} onClose={closeUsersManage} users={participants} onUserRemoved={onUserRemoved} onSubmit={onUsersManageSubmit} />
            <ConfirmDialog open={showDeleteDialog} title="Zrušit" description="Opravdu si přejete zrušit událost?" onClose={hideEventDeleteDialog} onSuccess={onDeleteConfirmed} />
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
                        {canEdit &&
                            <Button variant="outlined" startIcon={<Delete />} onClick={onEventDeleteButtonClicked}>
                                Zrušit událost
                            </Button>
                        }
                        {isJoined
                            ? <Button variant="outlined">Zrušit účast</Button>
                            : <Button variant="outlined">Zúčastnit se</Button>
                        }
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
                                    {!!calendarEvent.maximalParticipantsCount
                                        ? `${calendarEvent.participantsCount} / ${calendarEvent.maximalParticipantsCount}`
                                        : `${calendarEvent.participantsCount}`
                                    }
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
                        <Stack direction="row" sx={{ justifyContent: "space-between" }}>
                            <Typography gutterBottom variant="h5" component="div">
                                Seznam uživatelů
                            </Typography>
                            <Button onClick={openUsersManage}>Spravovat účastníky</Button>
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
};

EventDetail.displayName = "EventDetailComponent";
export default EventDetail;