import { gql } from "@apollo/client";

export const GET_USERS_FOR_CHANNEL = gql`
    query GetUsersForChannel($channelId: ID!) {
        users {
            getForCommunicationChannel(channelId: $channelId) {
                data {
                    id
                    userName
                    createdAt
                    modifiedAt
                }
                hasNextPage
                hasPreviousPage
                pageIndex
                totalCount
                totalPages
            }
        }
    }
`;