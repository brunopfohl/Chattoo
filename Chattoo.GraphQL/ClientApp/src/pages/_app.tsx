import { ApolloClient, ApolloProvider, InMemoryCache } from '@apollo/client'
import { useRouter } from 'next/router';
import React, { useEffect, useState } from 'react';
import '../../styles/globals.css'
import { ApplicationPaths } from '../components/api-authorization/ApiAuthorizationConstants';
import authService from '../components/api-authorization/AuthorizeService';
import Loading from '../components/loading/loading.component';

// Instance Apollo klienta pro dotazování na API.
const client = new ApolloClient({
  uri: 'http://localhost:5000/graphql',
  cache: new InMemoryCache(),
  connectToDevTools: true
});

const App: React.FC<any> = ({ Component, pageProps}) => {
  let [isLoading, setIsLoading] = useState<boolean>(true);

  let isProcessingAuth = () => router.pathname === ApplicationPaths.Login || router.pathname === ApplicationPaths.LoginCallback;

  let router = useRouter();

  useEffect(() => {
    setIsLoading(false);
  }, [])

  authService.isAuthenticated().then((isAuthenticated) => {
    if (isAuthenticated) {
      setIsLoading(false);
    }
    else if(!isProcessingAuth()){
      router.push(ApplicationPaths.Login);
      setIsLoading(false);
    }
  });

  return (
    <ApolloProvider client={client}>
        {isLoading
          ? <Loading detail="Chattoo"/>
          : <Component {...pageProps} />
        }
    </ApolloProvider>
  );
};

export default App;
