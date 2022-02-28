import EventsDashboard, { EventsDashboardMode } from '@components/events/dashboard/events-dashboard.component';
import Header from '@components/header/header.component';
import { CalendarEvent, CalendarEventTypeGraphType, useGetJoinedCalendarEventsQuery } from 'graphql/graphql-types';
import { FC, useState } from 'react';

const JoinedEventsPage: FC = () => {
    const { data, refetch: refetch } = useGetJoinedCalendarEventsQuery({
        variables: {
            pageNumber: 1,
            pageSize: 1000
        }
    });

    const events = (data?.calendarEvents?.getJoined?.data || []) as CalendarEvent[];

    return (
        <>
            <Header />
            <EventsDashboard events={events} refetchEvents={refetch} mode={EventsDashboardMode.Joined} />
        </>
    )
};

export default JoinedEventsPage;