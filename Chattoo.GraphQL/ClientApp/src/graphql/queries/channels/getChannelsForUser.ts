import { gql } from "@apollo/client";

/**
 * Dotaz na GraphQL API pro získání uživatelů z daného komunikačního kanálu.
 */
export const GET_CHANNELS_FOR_USER = gql`
    query GetChannelsForUser($userId: String!, $pageNumber: Int!, $pageSize: Int!) {
        communicationChannels {
            getForUser(userId: $userId, pageNumber: $pageNumber, pageSize: $pageSize) {
                data {
                    id,
                    name,
                    description,
                    createdAt,
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