import React, { useContext } from 'react'
import styled from 'styled-components';

export interface MessageComponentProps {
    isFromCurrentUser: boolean,
    isStartOfBatch: boolean,
    isEndOfBatch: boolean,
    content: string,
    userName: string,
    createdAt: Date
};


const MessageComponent: React.FC<any> = (props: MessageComponentProps) => {
    const { isFromCurrentUser, isStartOfBatch, isEndOfBatch, content, userName, createdAt } = props;

    const Container = styled.div`
        display: flex;
        flex-direction: ${isFromCurrentUser ? "row-reverse" : "row"};
    `;

    const MessageContainer = styled.div`
        color: white;
        display: flex;
        flex-direction: column;
        flex-direction: ${isFromCurrentUser ? "flex-end" : "flex-start"};
        margin: 1px;
    `;

    const ContentContainer = styled.div`
        padding: 0.4em 1em;
        color: ${isFromCurrentUser ? "white" : "black"};
        background-color: ${isFromCurrentUser ? "rgba(2, 184, 147, 1)" : "lightgray"};
        border-top-left-radius: ${!isFromCurrentUser && !isStartOfBatch ? "3px" : "1em"};
        border-top-right-radius: ${isFromCurrentUser && !isStartOfBatch ? "3px" : "1em"};
        border-bottom-left-radius: ${!isFromCurrentUser && !isEndOfBatch ? "3px" : "1em"};
        border-bottom-right-radius: ${isFromCurrentUser && !isEndOfBatch ? "3px" : "1em"};
    `;

    return (
        <Container>
            <MessageContainer>
                {isStartOfBatch && userName}
                <ContentContainer>
                    {content}
                </ContentContainer>
            </MessageContainer>
        </Container>
    );
}

export default MessageComponent;