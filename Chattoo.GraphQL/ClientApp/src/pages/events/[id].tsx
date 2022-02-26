import { AppStateContext } from '@components/app-state-provider.component';
import EventDetail from '@components/events/event-detail.component';
import Header from '@components/header/header.component';
import { Typography } from '@mui/material';
import { useGetCalendarEventQuery, useGetUsersForCalendarEventQuery, User } from 'graphql/graphql-types';
import { useRouter } from 'next/router';
import { FC, useContext, useMemo } from 'react';

const EventPage: FC = () => {
    const { appState } = useContext(AppStateContext);
    const { user } = appState;

    const router = useRouter();
    const { id } = router.query;

    const { data: eventQueryData, error: eventQueryError } = useGetCalendarEventQuery({
        variables: {
            id: id as string
        }
    });

    const { data: usersQueryData, error: usersQueryError, refetch: refetchParticipants } = useGetUsersForCalendarEventQuery({
        variables: {
            calendarEventId: id as string
        }
    });

    const event = useMemo(() => {
        return eventQueryData?.calendarEvents?.get;
    }, [eventQueryData]);

    const users = useMemo(() => {
        return (usersQueryData?.users?.getForCalendarEvent?.data || []) as User[];
    }, [usersQueryData]);

    const canEdit = useMemo(() => {
        return event && user.id == event?.authorId || false;
    }, [user, event]);

    return (
        <>
            <Header />

            {event
                ? <EventDetail calendarEvent={event} participants={users} canEdit={canEdit} refetchParticipants={refetchParticipants} />
                : <Typography>Událost neexistuje</Typography>
            }
        </>
    )
};

export default EventPage;