import Header from '@components/header/header.component';
import WishesDashboard from '@components/wishes/wishes-dashboard.component';
import { FC } from 'react';

const WishesPage: FC = () => {
    return (
        <>
            <Header />
            <WishesDashboard />
        </>
    )
};

export default WishesPage;