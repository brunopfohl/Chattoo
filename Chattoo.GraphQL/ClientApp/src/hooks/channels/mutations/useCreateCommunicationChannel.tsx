import { gql, useMutation } from "@apollo/client";
import { CreateCommunicationChannelVariables } from "../../../common/interfaces/schema-types";

export interface CreateCommunicationChannelInput {
    variables: CreateCommunicationChannelVariables
}

export const CREATE_COMMUNICATION_CHANNEL = gql`
    mutation CreateCommunicationChannel($name: String!, $desc: String!) {
        communicationChannels {
            create(name: $name, desc: $desc) {
                id
            }
        }
    }
`;

export const useCreateCommunicationChannel = (): ((
    input: CreateCommunicationChannelInput,
) => any) => {
    const [createCommunicationChannel] = useMutation(CREATE_COMMUNICATION_CHANNEL, {
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

    return createCommunicationChannel;
}