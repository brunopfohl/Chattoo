import React, { Fragment, useEffect, useState } from 'react';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

const LoginMenu: React.FC = (props: any) => {
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
    const [userName, setUserName] = useState<string | undefined>();
    let subscribtion: number;

    const populateState = async() => {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        setIsAuthenticated(isAuthenticated);
        setUserName(user.name);
    }

    useEffect(() => {
        subscribtion = authService.subscribe(() => populateState());
        populateState();
        
        return () => {
            authService.unsubscribe(subscribtion);
        }
    });

    const authenticatedView = (userName, profilePath, logoutPath) => {
        return (<Fragment>
            <a href={profilePath}>Hello {userName}</a>
            <a href={logoutPath}>Logout</a>
        </Fragment>);
    }

    const anonymousView = (registerPath, loginPath) => {
        return (<Fragment>
            <a href={registerPath}>Register</a>
            <a href={loginPath}>Login</a>
        </Fragment>);
    }

    if (!isAuthenticated) {
        const registerPath = `${ApplicationPaths.Register}`;
        const loginPath = `${ApplicationPaths.Login}`;
        return anonymousView(registerPath, loginPath);
    } else {
        const profilePath = `${ApplicationPaths.Profile}`;
        const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
        return authenticatedView(userName, profilePath, logoutPath);
    }
};

export default LoginMenu;