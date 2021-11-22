import { gql } from "@apollo/client";

/**
 * GraphQL dotaz na API pro získání konkrétní kalendářní události dle jejího Id.
 */
export const GET_CHANNEL_CALENDAR_EVENT = gql`
    query GetChannelCalendarEvent($id: ID!) {
        communicationChannelCalendarEvents {
            get(id: $id) {
                id,
                startsAt,
                endsAt,
                name,
                description,
                authorId,
                authorName,
                createdAt,
                modifiedAt
            }
        }
    }
`;