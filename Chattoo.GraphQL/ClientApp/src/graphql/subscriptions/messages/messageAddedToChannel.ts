import { gql } from "@apollo/client";

/**
 * GraphQL API subscription, který informuje klienta, pokud do jeho komunikačního kanálu přibyla nová zpráva.
 */
export const MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION = gql`
    subscription MessageAddedToChannel($channelId: String!) {
        communicationChannelMessageAddedToChannel(channelId: $channelId) {
            id,
            createdAt,
            modifiedAt,
            content,
            channelId,
            userId,
            type
        }
    }
`;