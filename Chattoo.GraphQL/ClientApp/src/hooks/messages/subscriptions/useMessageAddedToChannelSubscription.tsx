import { gql, useSubscription } from "@apollo/client";
import { Message } from "../../../common/interfaces/message.interface";
import { MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables } from "../../../common/interfaces/schema-types";

export const MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION = gql`
    subscription MessageAddedToChannelSubscription($channelId: String!) {
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

// TODO: Hook vrací instanci subscription jako any, což by chtělo nějak vylepšit.
export const useMessageAddedToChannelSubscription = (variables: MessageAddedToChannelSubscriptionVariables): [Message | undefined, any] => {
    const subscription = useSubscription<MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables>(MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION, {
        variables: variables
    });

    return [subscription.data?.communicationChannelMessageAddedToChannel, subscription.error];
}