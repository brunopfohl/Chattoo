import { gql, useMutation } from "@apollo/client";
import { AddUserToCommunicationChannelVariables } from "../../../common/interfaces/schema-types";

export interface AddUserToCommunicationChannelInput {
    variables: AddUserToCommunicationChannelVariables
}

export const ADD_USER_TO_COMMUNICATION_CHANNEL = gql`
    mutation AddUserToCommunicationChannel($userId: String!, $channelId: String!) {
        communicationChannels {
            addUser(userId: $userId, channelId: $channelId)
        }
    }
`;

export const useAddUserToCommunicationChannel = (): ((
    input: AddUserToCommunicationChannelInput,
) => any) => {
    const [addUserToCommunicationChannel] = useMutation(ADD_USER_TO_COMMUNICATION_CHANNEL, {
        update(cache, { data }) {
            // const cacheId = cache.identify(data.communicationChannels.create);
            
            // cache.modify({
            //     fields: {
            //         communicationChannels: (existingFieldData, { toReference }) => {
            //             return existingFieldData;
            //         }
            //     }
            // });
        }
    });

    return addUserToCommunicationChannel;
}