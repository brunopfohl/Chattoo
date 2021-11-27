
import { GET_MESSAGES_FOR_CHANNEL } from "graphql/queries/messages/getMessagesForChannel";
import { CreateMessageMutationVariables, GetMessagesForChannelQuery, GetMessagesForChannelQueryVariables, useCreateMessageMutation } from "graphql/graphql-types";
import produce from "immer";

export interface CreateMessageInput {
    variables: CreateMessageMutationVariables
}

export const useCreateMessage = (): ((input: CreateMessageInput) => any) => {
    const [createMessageMutation] = useCreateMessageMutation({
        update(cache, { data }) {
            const msg = data.communicationChannelMessages.create;

            const variables: GetMessagesForChannelQueryVariables = {
                channelId: msg.channelId,
                pageNumber: 1,
                pageSize: 20
            };

            const prevData = cache.readQuery<GetMessagesForChannelQuery, GetMessagesForChannelQueryVariables>({
                query: GET_MESSAGES_FOR_CHANNEL,
                variables
            });

            cache.writeQuery<GetMessagesForChannelQuery>({
                query: GET_MESSAGES_FOR_CHANNEL,
                variables,
                data: produce(prevData, draftState => {
                    let data = draftState.communicationChannelMessages?.getForChannel?.data;

                    if (data) {
                        data.push(msg)
                        data = data.filter((v, i, a) => a.indexOf(v) === i && v.channelId === msg.channelId);
                    }
                })
            });
        }
    });

    return createMessageMutation;
}