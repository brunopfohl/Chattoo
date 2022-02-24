import { gql } from "@apollo/client";

export const GET_USERS_FOR_CALENDAR_EVENT = gql`
    query GetUsersForCalendarEvent($calendarEventId: ID!) {
        users {
            getForCalendarEvent(calendarEventId: $calendarEventId) {
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