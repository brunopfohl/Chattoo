import { gql } from "@apollo/client";

export const REMOVE_USER_FROM_CALENDAR_EVENT = gql`
    mutation RemoveUserFromCalendarEvent($userId: String!, $eventId: String!) {
        communicationChannelCalendarEvents {
            removeUser(userId: $userId, eventId: $eventId)
        }
    }
`;