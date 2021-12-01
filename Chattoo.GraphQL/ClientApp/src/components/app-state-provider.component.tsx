import { ApolloProvider, ApolloClient, HttpLink, InMemoryCache, split } from "@apollo/client";
import { WebSocketLink } from '@apollo/client/link/ws';
import { getMainDefinition } from "@apollo/client/utilities";
import { User } from "oidc-client";
import React, { createContext, ReactNode, useEffect, useState } from "react";
import { AppUser } from "../common/interfaces/app-user.interface";
import { API_URL, API_URL_WITHOUT_PROCOTOL } from "./api-authorization/ApiAuthorizationConstants";
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

    const httpLink = new HttpLink({
        uri: API_URL + "/graphql",
        credentials: "include",
        headers: {
            authorization: `Bearer ${authToken}`
        }
    });

    const wsLink = process.browser ? new WebSocketLink({
        uri: "wss://" + API_URL_WITHOUT_PROCOTOL + "/graphql",
        options: {
            reconnect: true
        }
    }) : null;

    const splitLink = wsLink ? split(
        ({query}) => {
            const definition = getMainDefinition(query);
            return (
                definition.kind === "OperationDefinition" &&
                definition.operation === "subscription"
            );
        },
        wsLink,
        httpLink
    ) : null;

    // Apollo client pro vylovávání GraphQL dotazů.
    const client = new ApolloClient({
        link: splitLink,
        cache,
        connectToDevTools: true,
        defaultOptions: {
            watchQuery: {
                fetchPolicy: "cache-first"
            }
        }
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