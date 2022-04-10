import { useSetter } from '@hooks/useSetter';
import { AddCircle } from '@mui/icons-material';
import { Container, Paper, Typography, Stack, Divider, IconButton } from "@mui/material";
import { AutocompleteItem } from 'common/interfaces/autocomplete-item.interface';
import { getKeysWithValues } from 'common/utils/enum.utils';
import { CalendarEventTypeGraphType, useGetActiveWishesQuery } from 'graphql/graphql-types';
import { FC, useMemo, useState } from "react";
import WishCreatePopup from './create-wish-form.component';

interface WishesDashboardProps {
}

const WishesDashboard: FC<WishesDashboardProps> = (props) => {
    const [showCreatePopup, setShowCreatePopup] = useState(false);

    const [pageNumber, setPageNumger] = useState(1);
    const [pageSize, setPageSize] = useState(10);

    const { data, refetch } = useGetActiveWishesQuery({});

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

    return (
        <>
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
                    <Stack direction="row" sx={{ flexWrap: "wrap" }}>
                    </Stack>
                </Paper>
            </Container>
        </>
    );
};

WishesDashboard.displayName = "WishedDashboardComponent";
export default WishesDashboard;