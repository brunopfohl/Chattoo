import { gql } from "@apollo/client";

export const ADD_USER_TO_CALENDAR_EVENT = gql`
    mutation AddUserToCalendarEvent($userId: String!, $eventId: String!) {
        communicationChannelCalendarEvents {
            addUser(userId: $userId, eventId: $eventId)
        }
    }
`;