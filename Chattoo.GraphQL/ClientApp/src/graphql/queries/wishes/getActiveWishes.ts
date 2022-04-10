import { gql } from "@apollo/client";

export const GET_ACTIVE_WISHES = gql`
    query GetActiveWishes($pageNumber: Int!, $pageSize: Int!) {
        wishes {
            getActive(pageNumber: $pageNumber, pageSize: $pageSize) {
                data {
                    id,
                    authorId,
                    authorName,
                    dateIntervals {
                        startsAt,
                        endsAt
                    },
                    maximalParticipantsCount,
                    minimalParticipantsCount
                },
                hasNextPage,
                hasPreviousPage,
                pageIndex,
                totalCount,
                totalPages
            }
        }
    }
`;