import { gql } from "@apollo/client";

/**
 * GraphQL API dotaz pro získání zpráv z daného komunikačního kanálu.
 */
export const GET_MESSAGES_FOR_CHANNEL = gql`
    query GetMessagesForChannel($channelId: ID!, $pageNumber: Int!, $pageSize: Int!) {
        communicationChannelMessages {
            getForChannel(channelId: $channelId, pageNumber: $pageNumber, pageSize: $pageSize) {
            data {
                id,
                content,
                userName,
                type,
                userId,
                channelId,
                createdAt,
                modifiedAt,
            },
            hasNextPage,
            hasPreviousPage,
            pageIndex,
            totalCount,
            totalPages
            }
        }
    }
`;