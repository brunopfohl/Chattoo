import styled from '@emotion/styled';
import { Box, Typography } from '@mui/material';
import moment from 'moment';
import { FC } from 'react';

export interface MessageComponentProps {
    isFromCurrentUser: boolean,
    isStartOfBatch: boolean,
    isEndOfBatch: boolean,
    content: string,
    userName: string,
    createdAt: Date
};

/**
 * Čas odeslání zprávy.
 */
const TimeContainer = styled.div`
    display: flex;
    justify-content: center;
    padding: 1em 0;
`;

/**
 * Komponenta obalující zprávu (celý řádek).
 */
const Container = styled.div<{ isFromCurrentUser: Boolean }>`
    display: flex;
    flex-direction: ${props => props.isFromCurrentUser ? "row-reverse" : "row"};
`;

/**
 * Komponenta obalující zprávu, pouze samotná zpráva.
 */
const MessageContainer = styled.div<{ isFromCurrentUser: Boolean }>`
    display: flex;
    flex-direction: column;
    flex-direction: ${props => props.isFromCurrentUser ? "flex-end" : "flex-start"};
    margin: 1px;
`;

/**
 * Komponenta obalující obsah zprávy.
 */
const ContentContainer = styled.div<{ isFromCurrentUser: Boolean, isStartOfBatch: Boolean, isEndOfBatch: Boolean }>`
    padding: 0.4em 1em;
    color: ${props => props.isFromCurrentUser ? "white" : "black"};
    background-color: ${props => props.isFromCurrentUser ? "rgba(2, 184, 147, 1)" : "lightgray"};
    border-top-left-radius: ${props => !props.isFromCurrentUser && !props.isEndOfBatch ? "3px" : "1em"};
    border-top-right-radius: ${props => props.isFromCurrentUser && !props.isEndOfBatch ? "3px" : "1em"};
    border-bottom-left-radius: ${props => !props.isFromCurrentUser && !props.isStartOfBatch ? "3px" : "1em"};
    border-bottom-right-radius: ${props => props.isFromCurrentUser && !props.isStartOfBatch ? "3px" : "1em"};
`;

/**
 * Komponenta - zpráva v komunikačním kanálu.
 */
const MessageComponent: FC<MessageComponentProps> = (props) => {
    const { isFromCurrentUser, isStartOfBatch, isEndOfBatch, content, userName, createdAt } = props;

    return (
        <Box>
            {/* Pokud se jedná o poslední zprávu (nejstarší z dávky), zobrazím čas odeslání. */}
            {isEndOfBatch &&
                <TimeContainer>
                    <Typography variant="subtitle2">
                        {moment(createdAt).locale("cs").format("MMM Do YY, h:mm")}
                    </Typography>
                </TimeContainer>
            }
            <Container {...{ isFromCurrentUser }}>
                <MessageContainer {...{ isFromCurrentUser }}>
                    <ContentContainer {...{ isFromCurrentUser, isStartOfBatch, isEndOfBatch }}>
                        {content}
                    </ContentContainer>
                    {/* Pokud se jedná o nejnovější zprávu z aktuální dávky a nepatří přihlášenému uživatelovi, zobrazím jméno autora. */}
                    {!isFromCurrentUser && isStartOfBatch &&
                        <Typography sx={{ fontSize: 10, color: "grey", pl: 1 }} variant="caption">{userName}</Typography>
                    }
                </MessageContainer>
            </Container>
        </Box>
    );
}

export default MessageComponent;