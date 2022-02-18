import { gql } from "@apollo/client";

/**
 * GraphQL API mutace pro vytvoření zprávy v komunikačním kanálu.
 */
export const CREATE_MESSAGE = gql`
    mutation CreateMessage($channelId: String!, $content: String!) {
        communicationChannelMessages {
            create(channelId: $channelId, content: $content, type: 1) {
                id
                content
                userName
                type
                channelId
                createdAt
                modifiedAt
            }
        }
    }
`;