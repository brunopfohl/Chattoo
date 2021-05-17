import { gql, useMutation } from "@apollo/client";
import { RemoveUserFromCommunicationChannelVariables } from "../../../common/interfaces/schema-types";
import { GET_USERS_FOR_CHANNEL } from "../../users/queries/useGetUsersForChannel";

export interface RemoveUserFromCommunicationChannelInput {
    variables: RemoveUserFromCommunicationChannelVariables
}

export const REMOVE_USER_FROM_COMMUNICATION_CHANNEL = gql`
    mutation RemoveUserFromCommunicationChannel($userId: String!, $channelId: String!) {
        communicationChannels {
            removeUser(userId: $userId, channelId: $channelId)
        }
    }
`;

export const useRemoveUserFromCommunicationChannel = (): ((
    input: RemoveUserFromCommunicationChannelInput,
) => any) => {
    const [removeUserFromCommunicationChannel] = useMutation(REMOVE_USER_FROM_COMMUNICATION_CHANNEL, {
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

    return removeUserFromCommunicationChannel;
}