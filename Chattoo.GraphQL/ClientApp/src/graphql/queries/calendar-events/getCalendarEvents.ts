import { gql } from "@apollo/client";

/**
 * GraphQL dotaz na API pro získání kalendářních událostí přihlášeného uživatele.
 */
export const GET_CALENDAR_EVENTS = gql`
    query GetCalendarEvents($pageNumber: Int!, $pageSize: Int!) {
        calendarEvents {
            getVisible(pageNumber: $pageNumber, pageSize: $pageSize) {
                data {
                    id,
                    startsAt,
                    endsAt,
                    name,
                    description,
                    maximalParticipantsCount,
                    participantsCount,
                    authorId,
                    communicationChannelId,
                    createdAt,
                    modifiedAt,
                    type
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