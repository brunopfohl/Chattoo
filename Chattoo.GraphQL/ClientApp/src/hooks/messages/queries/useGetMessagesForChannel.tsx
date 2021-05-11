import { gql, QueryResult, useQuery } from "@apollo/client";
import { Message } from "../../../common/interfaces/message.interface";
import { PaginatedList } from "../../../common/interfaces/paginated-list";
import { GetMessagesForChannel, GetMessagesForChannelVariables, MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables } from "../../../common/interfaces/schema-types";
import { MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION } from "../subscriptions/useMessageAddedToChannelSubscription";

export const GET_MESSAGES_FOR_CHANNEL = gql`
    query GetMessagesForChannel($channelId: ID!, $pageNumber: Int!, $pageSize: Int!) {
        communicationChannelMessages {
            getForChannel(channelId: $channelId, pageNumber: $pageNumber, pageSize: $pageSize) {
            data {
                id,
                content,
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

export const useGetMessagesForChannel = (variables: GetMessagesForChannelVariables): [PaginatedList<Message> | undefined, QueryResult<GetMessagesForChannel, GetMessagesForChannelVariables>] => {
    console.log("useGetMessagesForChannel");
    const query = useQuery<GetMessagesForChannel, GetMessagesForChannelVariables>(GET_MESSAGES_FOR_CHANNEL, {
        variables: variables
    });

    query.subscribeToMore<MessageAddedToChannelSubscription, MessageAddedToChannelSubscriptionVariables>({
        document: MESSAGE_ADDED_TO_CHANNEL_SUBSCRIPTION,
        variables: {
            channelId: variables.channelId
        },
        updateQuery: (prev, { subscriptionData }) => {
            // Pokud nemám žádná nová data, vrátím původní hodnotu.
            if (!subscriptionData.data) {
                return prev;
            }

            // Vytáhnu si novou zprávu.
            const newMessage = subscriptionData.data.communicationChannelMessageAddedToChannel;

            return Object.assign({}, prev,  {
                communicationChannelMessages: {
                    getForChannel: {
                        data: prev?.communicationChannelMessages?.getForChannel?.data ? [...prev.communicationChannelMessages.getForChannel.data, newMessage ] : [newMessage]
                    }
                }
            });
        }
    });


    return [query.data?.communicationChannelMessages.getForChannel, query];
}