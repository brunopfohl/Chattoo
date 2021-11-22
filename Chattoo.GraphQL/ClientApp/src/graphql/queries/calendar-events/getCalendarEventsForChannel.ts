import { gql } from "@apollo/client";

/**
 * GraphQL dotaz na API pro získání kalendářních události pro daný komunikační kanál.
 */
export const GET_CALENDAR_EVENTS_FOR_CHANNEL = gql`
    query GetCalendarEventsForChannel($channelId: String!) {
        communicationChannelCalendarEvents {
            getForCommunicationChannel(channelId: $channelId) {
                data {
                    id,
                    startsAt,
                    endsAt,
                    name,
                    description,
                    authorId,
                    authorName,
                    createdAt,
                    modifiedAt
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