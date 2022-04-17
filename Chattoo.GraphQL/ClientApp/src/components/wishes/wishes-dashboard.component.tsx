import ConfirmDialog from '@components/confirm-dialog.component';
import { useSetter } from '@hooks/useSetter';
import { AccessTime, AddCircle, Clear, Delete, Event, Person, Remove } from '@mui/icons-material';
import { Container, Paper, Typography, Stack, Divider, IconButton, List, ListItem, ListItemText, ListItemIcon, Chip } from "@mui/material";
import { AutocompleteItem } from 'common/interfaces/autocomplete-item.interface';
import { getKeysWithValues } from 'common/utils/enum.utils';
import { CalendarEventTypeGraphType, useDeleteWishMutation, useGetActiveWishesQuery } from 'graphql/graphql-types';
import moment from 'moment';
import { FC, useCallback, useMemo, useState } from "react";
import WishCreatePopup from './create-wish-form.component';

interface WishesDashboardProps {
}

const WishesDashboard: FC<WishesDashboardProps> = (props) => {
    const [showCreatePopup, setShowCreatePopup] = useState(false);

    const [pageNumber, setPageNumger] = useState(1);
    const [pageSize, setPageSize] = useState(1000);

    const { data, refetch } = useGetActiveWishesQuery({
        variables: {
            pageNumber: pageNumber,
            pageSize: pageSize
        }
    });

    const queryResult = data?.wishes?.getActive;
    const wishes = queryResult?.data;
    const hasNextPage = queryResult?.hasNextPage;
    const hasPreviousPage = queryResult?.hasPreviousPage;
    const numberOfPages = queryResult?.totalPages;
    const totalCount = queryResult?.totalCount;

    const onWishCreatePopupClosed = useSetter(setShowCreatePopup, false);
    const showEventCreatePopup = useSetter(setShowCreatePopup, true);

    const eventTypes: AutocompleteItem<CalendarEventTypeGraphType>[] = useMemo(() => {
        return getKeysWithValues(CalendarEventTypeGraphType);
    }, []);

    const [deleteWish] = useDeleteWishMutation();

    const [wishToDeleteId, setWishToDeleteId] = useState<string>();

    const [showDeleteDialog, setShowDeleteDialog] = useState<boolean>(false);
    const hideDeleteDialog = useCallback(() => {
        setShowDeleteDialog(false);
    }, [setShowDeleteDialog]);

    const invokeDeleteDialog = useCallback((wishId) => {
        setWishToDeleteId(wishId);
        setShowDeleteDialog(true);
    }, [setWishToDeleteId, setShowDeleteDialog])

    const onDeleteConfirmed = useCallback(() => {
        setShowDeleteDialog(false);

        deleteWish({
            variables: {
                id: wishToDeleteId
            }
        }).then(() => {
            setWishToDeleteId(undefined);
            refetch();
        })

    }, [wishToDeleteId, setShowDeleteDialog, setWishToDeleteId]);

    return (
        <>
            <ConfirmDialog open={showDeleteDialog} title="Smazat" description="Opravdu si přejete smazat přání?" onClose={hideDeleteDialog} onSuccess={onDeleteConfirmed} />
            <WishCreatePopup onSubmit={refetch} onClose={onWishCreatePopupClosed} open={showCreatePopup} types={eventTypes} />
            <Container maxWidth="md" sx={{ pt: 2 }}>
                <Paper elevation={3} sx={{ p: 2 }}>
                    <Stack direction="row" sx={{ justifyContent: "space-between" }}>
                        <Typography variant="h5">
                            Vaše přání
                        </Typography>
                        {/* Přidání přání */}
                        <IconButton color="primary" onClick={showEventCreatePopup}>
                            <AddCircle />
                        </IconButton>
                    </Stack>
                    <Divider sx={{ mt: 1, mb: 1 }} />
                    {/* Seznam přání */}
                    <List>
                        {wishes && wishes.map(wish => {
                            return (
                                <>
                                    <Stack direction="row" sx={{ justifyContent: "space-between", width: "100%" }}>
                                        <ListItem key={wish.id}>
                                            <Stack>
                                                <ListItemText>
                                                    {wish.name}
                                                    <Chip size="small" sx={{ ml: 1 }} label={`${wish?.minimalLengthInMinutes}m`} icon={<AccessTime />} />
                                                    <Chip size="small" sx={{ ml: 1 }} label={wish?.minimalParticipantsCount} icon={<Person />} />
                                                </ListItemText>
                                                <List>
                                                    {wish?.dateIntervals?.map((dateInterval, i) => (
                                                        <ListItem key={`wish_interval_${wish.id}_${i}`}>
                                                            <ListItemIcon>
                                                                <Event />
                                                            </ListItemIcon>
                                                            <Typography>
                                                                {moment(dateInterval.startsAt).format('D. M. YYYY hh:mm')} - {moment(dateInterval.endsAt).format('D. M. YYYY hh:mm')}
                                                            </Typography>
                                                        </ListItem>
                                                    ))}
                                                </List>
                                            </Stack>
                                        </ListItem>
                                        <IconButton onClick={() => invokeDeleteDialog(wish.id)}>
                                            <Delete color="warning" />
                                        </IconButton>
                                    </Stack>
                                    <Divider />
                                </>
                            )
                        })}
                    </List>
                </Paper>
            </Container >
        </>
    );
};

WishesDashboard.displayName = "WishedDashboardComponent";
export default WishesDashboard;