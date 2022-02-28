import { gql } from "@apollo/client";

/**
 * GraphQL dotaz na API pro získání kalendářních událostí přihlášeného uživatele (do kterých je zapojen).
 */
export const GET_CALENDAR_EVENTS = gql`
    query GetJoinedCalendarEvents($pageNumber: Int!, $pageSize: Int!) {
        calendarEvents {
            getJoined(pageNumber: $pageNumber, pageSize: $pageSize) {
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