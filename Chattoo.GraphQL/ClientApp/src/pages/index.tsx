import Chat from '@components/chat/chat.component';
import EventsDashboard from '@components/events/events-dashboard.component';
import Header from '@components/header/header.component';
import ProfileSettings from '@components/profile/profile-settings.compoment';
import { Page } from 'common/enums/page.enum';
import { FC, useMemo, useState } from 'react';

/**
 * Komponenta - hlavní stránka.
 */
const Home: FC = () => {
  const [page, setPage] = useState<Page>(Page.Chat);

  const content = useMemo(() => {
    switch (page) {
      // Komponenta s chatem
      case Page.Chat:
        return <Chat />;
      // Komponenta s událostmi
      case Page.Events:
        return <EventsDashboard />;
      // Komponenta s nastavením účtu
      case Page.Profile:
        return <ProfileSettings />;
    }
  }, [page]);

  return (
    <>
      {/* Komponenta s hlavičkou */}
      <Header setPage={setPage} />

      {content}
    </>
  )
};

Home.displayName = "HomePage";
export default Home;