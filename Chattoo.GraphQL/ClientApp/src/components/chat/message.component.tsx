import React from 'react'
import styled from 'styled-components';

interface MessageProps {
    authorName: string,
    content: string
};

const Container = styled.div`
    padding: 0.5em 1em;
    margin: 0.5em;
    background-color: #02B893;
    border-radius: 2em;
    color: white;
    width: auto;
`;

const Message: React.FC<any> = (props: MessageProps) => {
    return (
        <Container>
            {props.content}
        </Container>
    );
}

export default Message;