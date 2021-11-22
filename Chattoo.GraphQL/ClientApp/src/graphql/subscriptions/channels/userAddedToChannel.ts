import { gql } from "@apollo/client";

/**
 * Subscription k GraphQL API, který informuje klienta o komunikačních kanálech, do kterých byl přihlášený uživatel nově přidán.
 */
export const USER_ADDED_TO_COMMUNICATION_CHANNEL_SUBSCRIPTION = gql`
    subscription UserAddedToChannel($userId: String!) {
        communicationChannelAddedForUser(userId: $userId) {
            id,
            name,
            description,
            createdAt,
            modifiedAt
        }
    }
`;