import { ApolloClient, ApolloLink, ApolloProvider, concat, from, HttpLink, InMemoryCache } from '@apollo/client'
import { useRouter } from 'next/router';
import { Profile } from 'oidc-client';
import React, { useEffect, useState } from 'react';
import '../../styles/globals.css'
import { ApplicationPaths } from '../components/api-authorization/ApiAuthorizationConstants';
import authService from '../components/api-authorization/AuthorizeService';
import Loading from '../components/loading/loading.component';

const httpLink = new HttpLink({ uri: 'http://localhost:5000/graphql' });

const authMiddleware = new ApolloLink((operation, forward) => {
  // add the authorization to the headers
  operation.setContext(({ headers = {} }) => ({
    headers: {
      ...headers,
      authorization: authService.getUnsafeAccessToken() || null,
    }
  }));

  return forward(operation);
})

// Instance Apollo klienta pro dotazování na API.
const client = new ApolloClient({
  cache: new InMemoryCache(),
  link: concat(authMiddleware, httpLink),
  connectToDevTools: true
});

const App: React.FC<any> = ({ Component, pageProps}) => {
  const [isLoading, setIsLoading] = useState<boolean>(true);
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);

  const router = useRouter();

  const isProcessingAuth = () => {
    let pn = router.pathname;
    return pn === ApplicationPaths.Login || pn === ApplicationPaths.LoginCallback || pn === "/connect-account";
  };

  useEffect(() => {
    authService.isAuthenticated().then((auth) => {
      setIsAuthenticated(auth);

      if (!auth && !isProcessingAuth()) {
        router.push("/connect-account");
      }

      setIsLoading(false);
    });
  }, []);

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