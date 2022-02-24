import EventDetail from '@components/events/event-detail.component';
import Header from '@components/header/header.component';
import { Typography } from '@mui/material';
import { CalendarEvent, useGetCalendarEventQuery, useGetUsersForCalendarEventQuery, User } from 'graphql/graphql-types';
import { useRouter } from 'next/router';
import { FC, useMemo } from 'react';

const EventPage: FC = () => {
    const router = useRouter();
    const { id } = router.query;

    const { data: eventQueryData, error: eventQueryError } = useGetCalendarEventQuery({
        variables: {
            id: id as string
        }
    });

    const { data: usersQueryData, error: usersQueryError } = useGetUsersForCalendarEventQuery({
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

    return (
        <>
            <Header />

            {event
                ? <EventDetail calendarEvent={event} participants={users} />
                : <Typography>Událost neexistuje</Typography>
            }
        </>
    )
};

export default EventPage;