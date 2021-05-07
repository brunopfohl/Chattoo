import { useRouter } from 'next/router';
import React, { useContext, useEffect, useState } from 'react';
import { ApplicationPaths } from '../components/api-authorization/ApiAuthorizationConstants';
import { AppStateContext } from '../components/app-state-provider.component';
import Loading from '../components/loading/loading.component';

const ComponentWrapper: React.FC<any> = ({ Component, pageProps}) => {
  const router = useRouter();
  const { appState } = useContext(AppStateContext);
  const { loggedIn, processingAuth } = appState;

  const [isReady, setIsReady] = useState<boolean>(false);


  const isProcessingAuth = () => {
    let pn = router.pathname;
    return pn === ApplicationPaths.Login || pn === ApplicationPaths.LoginCallback || pn === "/connect-account";
  };

  useEffect(() => {
    if(loggedIn || isProcessingAuth()) {
      setIsReady(true);
    }
    else if(loggedIn === false && !processingAuth) {
      setIsReady(false);
      router.push("connect-account").then(() => {
        setIsReady(true);
      });
    }
  }, [isProcessingAuth]);

  return (
    <>
      { isReady
        ? <Component {...pageProps} />
        : <Loading detail="Načítání" />
      }
    </>
  );
};

export default ComponentWrapper;