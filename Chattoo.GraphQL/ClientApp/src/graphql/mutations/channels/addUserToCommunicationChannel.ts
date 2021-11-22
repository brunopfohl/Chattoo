import { gql } from "@apollo/client";

/**
 * GraphQL mutace pro přidání uživatele do komunikačního kanálu skrz API.
 */
export const ADD_USER_TO_COMMUNICATION_CHANNEL = gql`
    mutation AddUserToCommunicationChannel($userId: String!, $channelId: String!) {
        communicationChannels {
            addUser(userId: $userId, channelId: $channelId)
        }
    }
`;