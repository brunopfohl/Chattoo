import { gql } from "@apollo/client";

export const GET_ACTIVE_WISHES = gql`
    query GetActiveWishes($pageNumber: Int!, $pageSize: Int!) {
        wishes {
            getActive(pageNumber: $pageNumber, pageSize: $pageSize) {
                data {
                    id,
                    name,
                    dateIntervals {
                        startsAt,
                        endsAt
                    },
                    minimalParticipantsCount,
                    minimalLengthInMinutes
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