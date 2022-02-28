import { gql } from "@apollo/client";

export const GET_CHANNEL = gql`
    query GetChannel($id: ID!) {
        communicationChannels {
            get(id: $id) {
                id,
                name,
                description,
                createdAt,
                modifiedAt,
                createdBy,
                deletedBy,
                modifiedBy,
            }
        }
    }
`;