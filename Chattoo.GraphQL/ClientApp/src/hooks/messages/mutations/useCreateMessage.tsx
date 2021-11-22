
import { useMutation } from "@apollo/client";
import { CreateMessageVariables, CreateMessage, GetMessagesForChannel, GetMessagesForChannelVariables, GetChannelsForUser, GetChannelsForUserVariables } from "../../../common/interfaces/schema-types";
import { GET_MESSAGES_FOR_CHANNEL } from "graphql/queries/messages/getMessagesForChannel";
import { CREATE_MESSAGE } from "graphql/mutations/messages/createMessage";

export interface CreateMessageInput {
    variables: CreateMessageVariables
}

export const useCreateMessage = (): ((input: CreateMessageInput) => any) => {
    const [createCommunicationChannel] = useMutation<CreateMessage, CreateMessageVariables>(CREATE_MESSAGE, {
        update(cache, { data }) {
            const msg = data.communicationChannelMessages.create;

            const variables = {
                channelId: msg.channelId,
                pageNumber: 1,
                pageSize: 20
            };

            const prevData = cache.readQuery<GetMessagesForChannel, GetMessagesForChannelVariables>({
                query: GET_MESSAGES_FOR_CHANNEL,
                variables
            });

            cache.writeQuery<GetMessagesForChannel>({
                query: GET_MESSAGES_FOR_CHANNEL,
                data: {
                    communicationChannelMessages: {
                        ...prevData.communicationChannelMessages,
                        getForChannel: {
                            ...prevData.communicationChannelMessages.getForChannel,
                            data: [...prevData.communicationChannelMessages.getForChannel.data, msg]
                        }
                    }
                }
            });
        }
    });

    return createCommunicationChannel;
}