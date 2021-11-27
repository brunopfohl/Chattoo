import { gql } from "@apollo/client";

/**
 * GraphQL API mutace pro vytvoření zprávy v komunikačním kanálu.
 */
export const CREATE_MESSAGE = gql`
    mutation CreateMessage($userId: String!, $channelId: String!, $content: String!) {
        communicationChannelMessages {
            create(userId: $userId, channelId: $channelId, content: $content, type: 1) {
                id
                content
                userName
                type
                userId
                channelId
                createdAt
                modifiedAt
            }
        }
    }
`;