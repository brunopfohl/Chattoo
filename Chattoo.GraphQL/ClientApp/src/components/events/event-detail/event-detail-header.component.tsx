import ConfirmDialog from "@components/confirm-dialog.component";
import { useCalendarEventTypeIconRenderer } from "@hooks/useCalendarTypeIcon";
import { useSetter } from "@hooks/useSetter";
import { Delete } from "@mui/icons-material";
import { Box, Button, CardMedia, Container, Divider, Paper, Stack, Typography } from "@mui/material";
import { CalendarEvent } from "graphql/graphql-types";
import moment from "moment";
import { FC, useCallback, useMemo, useState } from "react";

interface EventDetailHeaderProps {
    calendarEvent: CalendarEvent
    canEdit: boolean;
    isJoined: boolean;
    join: () => void;
    leave: () => void;
    processDelete: () => void;
}

const EventDetailHeader: FC<EventDetailHeaderProps> = (props) => {
    const { calendarEvent, canEdit, isJoined, join, leave, processDelete } = props;

    const [showDeleteDialog, setShowDeleteDialog] = useState<boolean>(false);

    const dateText = useMemo(() => {
        let result = moment(calendarEvent.startsAt).format("D. MM. YYYY - hh:mm");

        if (calendarEvent.endsAt) {
            const endsAt = moment(calendarEvent.endsAt).format("D. MM. YYYY - hh:mm");
            result = `Od ${result} do ${endsAt}`;
        }

        return result;
    }, [calendarEvent]);

    const onEventDeleteButtonClicked = useSetter(setShowDeleteDialog, true);
    const hideEventDeleteDialog = useSetter(setShowDeleteDialog, false);

    const onDeleteConfirmed = useCallback(() => {
        processDelete();
    }, [setShowDeleteDialog]);

    const renderTypeIcon = useCalendarEventTypeIconRenderer();

    return (
        <>
            <ConfirmDialog open={showDeleteDialog} title="Zrušit" description="Opravdu si přejete zrušit událost?" onClose={hideEventDeleteDialog} onSuccess={onDeleteConfirmed} />
            <Paper sx={{ mt: 2, p: 2 }}>
                <Container maxWidth="md">
                    <Box sx={{ position: "relative" }}>
                        <Box sx={{ bottom: "10px", left: "10px", borderRadius: "5px", p: 1, backgroundColor: "white", position: "absolute" }}>
                            {renderTypeIcon(calendarEvent.type)}
                        </Box>
                        <CardMedia
                            component="img"
                            alt="green iguana"
                            height="140"
                            image="/event.png"
                            sx={{ objectFit: "cover", borderRadius: "5px" }}
                        />
                    </Box>
                    <Typography variant="h6" color="secondary">
                        {dateText}
                    </Typography>
                    <Typography variant="h2">
                        {calendarEvent.name}
                    </Typography>
                    <Divider />
                    <Stack direction="row-reverse" spacing={1} sx={{ pt: 1 }}>
                        {canEdit
                            ?
                            <Button variant="outlined" startIcon={<Delete />} onClick={onEventDeleteButtonClicked}>
                                Zrušit událost
                            </Button>
                            :
                            <>
                                {isJoined
                                    ? <Button variant="outlined" onClick={leave}>Zrušit účast</Button>
                                    : <Button variant="outlined" onClick={join}>Zúčastnit se</Button>
                                }
                            </>
                        }
                    </Stack>
                </Container>
            </Paper>
        </>
    );
}

export default EventDetailHeader;