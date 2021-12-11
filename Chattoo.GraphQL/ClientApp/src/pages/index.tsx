import Chat from '@components/chat/chat.component';
import Header from '@components/header/header.component';
import { FC } from 'react';

/**
 * Komponenta - hlavní stránka.
 */
const Home: FC = () => {
  return (
    <>
      {/* Komponenta s hlavičkou */}
      <Header />

      {/* Komponenta s chatem */}
      <Chat />
    </>
  )
};

Home.displayName = "HomePage";
export default Home;