import EventsDashboard, { EventsDashboardMode } from '@components/events/dashboard/events-dashboard.component';
import Header from '@components/header/header.component';
import { CalendarEvent, useGetCalendarEventsQuery } from 'graphql/graphql-types';
import { FC } from 'react';

const EventsPage: FC = () => {
    const { data, refetch: refetch } = useGetCalendarEventsQuery({
        variables: {
            pageNumber: 1,
            pageSize: 1000
        }
    });

    const events = (data?.calendarEvents?.getVisible?.data || []) as CalendarEvent[];

    return (
        <>
            <Header />
            <EventsDashboard events={events} refetchEvents={refetch} mode={EventsDashboardMode.All} />
        </>
    )
};

export default EventsPage;