import { gql, useSubscription } from "@apollo/client";
import { CommunicationChannel } from "../../../common/interfaces/communication-channel.interface";
import { UserAddedToChannelSubscription, UserAddedToChannelSubscriptionVariables } from "../../../common/interfaces/schema-types";

export const USER_ADDED_TO_COMMUNICATION_CHANNEL_SUBSCRIPTION = gql`
    subscription UserAddedToChannelSubscription($userId: String!) {
        communicationChannelAddedForUser(userId: $userId) {
            id,
            name,
            description,
            createdAt,
            modifiedAt
        }
    }
`;

// TODO: Hook vrací instanci subscription jako any, což by chtělo nějak vylepšit.
export const useUserAddedToCommunicationChannelSubscription = (variables: UserAddedToChannelSubscriptionVariables): [CommunicationChannel | undefined, any] => {
    const subscription = useSubscription<UserAddedToChannelSubscription, UserAddedToChannelSubscriptionVariables>(USER_ADDED_TO_COMMUNICATION_CHANNEL_SUBSCRIPTION, {
        variables: variables
    });

    return [subscription.data?.communicationChannelAddedForUser, subscription.error];
}