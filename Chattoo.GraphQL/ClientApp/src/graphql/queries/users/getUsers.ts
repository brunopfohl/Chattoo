import { gql } from "@apollo/client";

export const GET_USERS = gql`
    query GetUsers($searchTerm: String!, $excludeUsersFromChannelWithId: String) {
        users {
            get(searchTerm: $searchTerm, excludeUsersFromChannelWithId: $excludeUsersFromChannelWithId) {
                data {
                    id
                    userName
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