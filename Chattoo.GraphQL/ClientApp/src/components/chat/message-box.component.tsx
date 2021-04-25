import React, { useState } from 'react'
import styled from 'styled-components';

interface MessageBoxProps {
    callback: Function;
}

/* Stylovaný kontejner prvku pro odeslání zprávy */
const Container = styled.div`
    display: flex;
    flex-grow: 1;
    flex-direction: row;
    overflow: hidden;
`;

/* Stylovaný form prvku pro odeslání zpráv */
const Form = styled.form`
    display: flex;
    flex-grow: 1;
`;

/* Stylovaný input pro zadání zprávy */
const MessageInput = styled.input`
    border: none;
    border-radius: 15px;
    flex-grow: 1;
    margin-left: 1em;
    height: 1em;
    font-size: 15pt;
    padding: 1em;
    margin-right: 1em;
`;

const MessageBox: React.FC<any> = (props: MessageBoxProps) => {
    const [text, setText] = useState("");

    const onSubmit = (event :React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        if(text) {
            setText("");
            props.callback(text);
        }
    };

    return (
        <Container>
            <Form onSubmit={onSubmit}>
                <MessageInput placeholder="Zadejte zprávu..." value={text} onChange={event => setText(event.target.value)}/>
            </Form>
        </Container>
    );
}

export default MessageBox;