import { ApolloProvider, ApolloClient, ApolloLink, HttpLink, InMemoryCache, Observable } from "@apollo/client";
import { onError } from '@apollo/link-error';
import { Profile, User } from "oidc-client";
import React, { createContext, ReactNode, useEffect, useState } from "react";
import { AppUser } from "../common/interfaces/app-user.interface";
import { API_URL } from "./api-authorization/ApiAuthorizationConstants";
import authService from "./api-authorization/AuthorizeService";

export interface AppStateContext {
    appState: AppState;
    gqlError: GQLError;
}

export interface AppState {
    loggedIn: boolean;
    processingAuth: boolean;
    user?: AppUser;
}

export interface GQLError {
    msg: string;
}

let authToken = authService.getUnsafeAccessToken();

const initial: AppStateContext = {
    appState: { loggedIn: false, processingAuth: true },
    gqlError: { msg: "" }
};

export const AppStateContext = createContext(initial);

const AppStateProvider = ({children}: {children: ReactNode}) => {
    // Aplikační stav
    const [appState, setAppState] = useState<AppState>(initial.appState)
    // Poslední GraphQL error.
    const [gqlError, setGqlError] = useState<GQLError>(initial.gqlError)
    // Aktuálně přihlášený uživatel.
    const [user, setUser] = useState<User>();

    // Přihlášení
    const appSetLogin = (user: User) => {
        // Pokud mám k dispozici přihlášeného uživatele.
        if (!!(user?.profile)) {
            // Nastavím aktuální access token.
            authToken = user.access_token;

            // Nastavím přihlášeného uživatele.
            let currentUser: AppUser = {
                id: user.profile.sub,
                userName: user.profile.name
            };

            // Aktualizuju stav v kontextu.
            setAppState({ ...appState, loggedIn: true, processingAuth: false, user: currentUser });
        }
    };

    // Odhlášení
    const appSetLogout = () => {
        authToken = "";
        setAppState({ ...appState, loggedIn: false, processingAuth: false, user: null });
    };

    // Při změně přihlášení.
    const handleAuth = () => {
        authService.getUser().then((user: User) => {
            if(user) {
                appSetLogin(user);
            }
            else {
                appSetLogout();
            }
        });
    };

    // Při prvním renderu komponenty zařídím, aby handler změny přihlášení naslouchal authService.
    useEffect(() => {
        const handleAuthCallbackId: number = authService.subscribe(handleAuth);
        return () => {
            // Zruším naslouchání.
            authService.unsubscribe(handleAuthCallbackId);
        };

    }, []);

    // Apollo client - cache
    const cache = new InMemoryCache({});

    // Apollo client - vložení autorizačního tokenu do headeru + zpracování připadné error message.
    const requestLink = new ApolloLink((operation, forward) => 
        new Observable(observer => {
            let handle: any;
            Promise.resolve(operation)
                .then(operation => {
                    operation.setContext({ headers: {authorization: `Bearer ${authToken}`}});
                })
                .then(() => {
                    handle = forward(operation).subscribe({
                        next: observer.next.bind(observer),
                        error: observer.error.bind(observer),
                        complete: observer.complete.bind(observer),
                    });
                })
                .catch(observer.error.bind(observer));

            return () => {
                handle && handle.unsubscribe();
            };
        })
    );

    // Apollo client pro vylovávání GraphQL dotazů.
    const client = new ApolloClient({
        link: ApolloLink.from([
            onError(({ graphQLErrors, networkError }) => {
                if(graphQLErrors === undefined || graphQLErrors[0].path === undefined) return;
                if(graphQLErrors[0].path[0] === "refresh") return;
                const err = graphQLErrors[0].message;
                setGqlError({ msg: err});
            }),
            requestLink,
            new HttpLink({
                uri: API_URL + "/graphql",
                credentials: "include"
            })
        ]),
        cache,
        connectToDevTools: true
    });
    
    // Render komponenty (children obalené ApolloClientem a ContextProviderem).
    return (
        <AppStateContext.Provider value={{
            appState,
            gqlError
            }}>
                <ApolloProvider client={client}>
                    {children}
                </ApolloProvider>
        </AppStateContext.Provider>
    );
};

export default AppStateProvider;