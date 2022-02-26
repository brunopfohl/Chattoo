import { gql } from "@apollo/client";

export const GET_USERS = gql`
    query GetUsers($searchTerm: String!, $excludedUserIds: [String]) {
        users {
            get(searchTerm: $searchTerm, excludedUserIds: $excludedUserIds) {
                data {
                    id
                    userName
                    createdAt,
                    modifiedAt,
                    createdBy,
                    deletedBy,
                    modifiedBy
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