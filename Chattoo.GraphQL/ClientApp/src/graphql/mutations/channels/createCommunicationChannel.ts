import { gql } from "@apollo/client";

/**
 * GraphQL mutace pro vytvoření komunikačního kanálu skrz API.
 */
export const CREATE_COMMUNICATION_CHANNEL = gql`
    mutation CreateCommunicationChannel($name: String!, $desc: String!) {
        communicationChannels {
            create(name: $name, desc: $desc) {
                id
            }
        }
    }
`;