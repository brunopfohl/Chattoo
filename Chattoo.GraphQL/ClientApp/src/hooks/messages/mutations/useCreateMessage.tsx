
import { gql, useMutation } from "@apollo/client";
import { CreateMessageVariables } from "../../../common/interfaces/schema-types";

export interface CreateMessageInput {
    variables: CreateMessageVariables
}

export const CREATE_MESSAGE = gql`
    mutation CreateMessage($userId: String!, $channelId: String!, $content: String!) {
        communicationChannelMessages {
            create(userId: $userId, channelId: $channelId, content: $content, type: 1)
        }
    }
`;

export const useCreateMessage = (): ((
    input: CreateMessageInput,
) => any) => {
    const [createCommunicationChannel] = useMutation(CREATE_MESSAGE);
    return createCommunicationChannel;
}