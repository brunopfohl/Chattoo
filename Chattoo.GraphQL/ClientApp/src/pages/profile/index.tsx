import Header from '@components/header/header.component';
import ProfileSettings from '@components/profile/profile-settings.compoment';
import { FC } from 'react';

/**
 * Komponenta - hlavní stránka.
 */
const EventsPage: FC = () => {
    return (
        <>
            {/* Komponenta s hlavičkou */}
            <Header />
            <ProfileSettings />
        </>
    )
};

export default EventsPage;