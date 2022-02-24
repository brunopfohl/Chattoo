import EventsDashboard from '@components/events/events-dashboard.component';
import Header from '@components/header/header.component';
import { FC } from 'react';

const EventsPage: FC = () => {
    return (
        <>
            <Header />
            <EventsDashboard />
        </>
    )
};

export default EventsPage;