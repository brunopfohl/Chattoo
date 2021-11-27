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

const TimeElement = styled.span`
    display: flex;
    justify-content: center;
    padding: 1em 0;
    color: white;
`;
const Container = styled.div<{isFromCurrentUser: Boolean}>`
    display: flex;
    flex-direction: ${props => props.isFromCurrentUser ? "row-reverse" : "row"};
`;

const MessageContainer = styled.div<{isFromCurrentUser: Boolean}>`
    color: white;
    display: flex;
    flex-direction: column;
    flex-direction: ${props => props.isFromCurrentUser ? "flex-end" : "flex-start"};
    margin: 1px;
`;

const ContentContainer = styled.div<{isFromCurrentUser: Boolean, isStartOfBatch: Boolean, isEndOfBatch: Boolean}>`
    padding: 0.4em 1em;
    color: ${props => props.isFromCurrentUser ? "white" : "black"};
    background-color: ${props => props.isFromCurrentUser ? "rgba(2, 184, 147, 1)" : "lightgray"};
    border-top-left-radius: ${props => !props.isFromCurrentUser && !props.isStartOfBatch ? "3px" : "1em"};
    border-top-right-radius: ${props => props.isFromCurrentUser && !props.isStartOfBatch ? "3px" : "1em"};
    border-bottom-left-radius: ${props => !props.isFromCurrentUser && !props.isEndOfBatch ? "3px" : "1em"};
    border-bottom-right-radius: ${props => props.isFromCurrentUser && !props.isEndOfBatch ? "3px" : "1em"};
`;

const MessageComponent: React.FC<any> = (props: MessageComponentProps) => {
    const { isFromCurrentUser, isStartOfBatch, isEndOfBatch, content, userName, createdAt } = props;

    const showTime = isStartOfBatch;

    return (
        <>
            {showTime && <TimeElement>{new Date(createdAt).toUTCString()}</TimeElement> }
            <Container {...{isFromCurrentUser}}>
                <MessageContainer {...{isFromCurrentUser}}>
                    {isStartOfBatch && userName}
                    <ContentContainer {...{isFromCurrentUser, isStartOfBatch, isEndOfBatch}}>
                        {content}
                    </ContentContainer>
                </MessageContainer>
            </Container>
        </>
    );
}

export default MessageComponent;