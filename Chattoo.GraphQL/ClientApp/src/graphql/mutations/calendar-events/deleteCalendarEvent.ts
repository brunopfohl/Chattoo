import { gql } from "@apollo/client";

export const DELETE_CALENDAR_EVENT = gql`
    mutation DeleteCalendarEvent($id: ID!) {
        communicationChannelCalendarEvents {
            delete(id: $id)
        }
    }
`;