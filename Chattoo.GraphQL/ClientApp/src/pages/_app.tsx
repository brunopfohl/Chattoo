import { ApolloClient, ApolloProvider, InMemoryCache } from '@apollo/client'
import { useRouter } from 'next/router';
import { useState } from 'react';
import '../../styles/globals.css'
import { ApplicationPaths } from '../components/api-authorization/ApiAuthorizationConstants';
import authService from '../components/api-authorization/AuthorizeService';

// Instance Apollo klienta pro dotazování na API.
const client = new ApolloClient({
  uri: 'http://localhost:5000/graphql',
  cache: new InMemoryCache(),
  connectToDevTools: true
});

const App: React.FC<any> = ({ Component, pageProps}) => {
  let [isReady, setIsReady] = useState<boolean>(false);

  let router = useRouter();

  authService.isAuthenticated().then((isAuthenticated) => {
    console.log(isAuthenticated);
    // Pokud client není přihlášený, přesměruji ho na login.
    if(!isAuthenticated && router.pathname !== ApplicationPaths.Login && router.pathname !== ApplicationPaths.LoginCallback) {
      router.push(ApplicationPaths.Login).then(() => {
        setIsReady(true);
      });
    }
  })

  return (
    <ApolloProvider client={client}>
        <Component {...pageProps} />
    </ApolloProvider>
  );
};

export default App;
