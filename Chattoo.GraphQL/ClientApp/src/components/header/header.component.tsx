import React, { useEffect, useState } from 'react'
import styled from 'styled-components';
import authService from '../api-authorization/AuthorizeService';
import Button from '../button/button.component';

const Container = styled.div`
    display: flex;
    padding-top: 1em;
    padding-bottom: 1em;
    justify-content: space-between;
`;

const Header: React.FC<any> = (props: any) => {
    let onLogout = () => {
        authService.signOut(window.location.href);
    };

    return (
        <Container>
            <Button text="Chattoo"/>
            <Button text="Odhlásit se" onClick={onLogout}/>
        </Container>
    );
}

export default Header;