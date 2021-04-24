import React from 'react'
import styled from 'styled-components';
import Button from '../button/button.component';

const Container = styled.div`
    display: flex;
    padding-top: 1em;
    padding-bottom: 1em;
`;

const Header: React.FC<any> = (props: any) => {

    return (
        <Container>
            <Button text="Chattoo" radius="60px" backgroundColor="#C227C2" />
            <Button text="fsfd" radius="50%" backgroundColor="#C227C2" />
        </Container>
    );
}

export default Header;