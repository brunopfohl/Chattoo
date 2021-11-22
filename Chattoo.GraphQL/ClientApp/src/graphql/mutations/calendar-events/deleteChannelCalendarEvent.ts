import { gql } from "@apollo/client";

/**
 * GraphQL mutace pro smazání kalendářní události skrz API.
 */
export const DELETE_CHANNEL_CALENDAR_EVENT = gql`
    mutation DeleteChannelCalendarEvent($id: String!) {
        communicationChannelCalendarEvents {
            delete(id: $id)
        }
    }
`;