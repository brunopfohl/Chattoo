import { gql } from "@apollo/client";

/**
 * GraphQL mutace pro odebrání uživatele z komunikačního kanálu skrz API.
 */
export const REMOVE_USER_FROM_COMMUNICATION_CHANNEL = gql`
    mutation RemoveUserFromCommunicationChannel($userId: String!, $channelId: String!) {
        communicationChannels {
            removeUser(userId: $userId, channelId: $channelId)
        }
    }
`;