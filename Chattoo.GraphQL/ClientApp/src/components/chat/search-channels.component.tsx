import React from 'react'
import styled from 'styled-components';
import ProfilePicture from '../profile-picture/profile-picture.component';

const Container = styled.div`
    display: flex;
    padding: 1em;
    flex-direction: column;
`;

const SearchBox = styled.input`
    padding: 1em;
    color: #bbb;
    border: none;
    border-radius: 0.5em;
`;


const SearchChannels: React.FC<any> = (props: any) => {

    return (
        <Container>
            <SearchBox placeholder="Hledejte mezi kontakty"></SearchBox>
        </Container>
    );
}

export default SearchChannels;