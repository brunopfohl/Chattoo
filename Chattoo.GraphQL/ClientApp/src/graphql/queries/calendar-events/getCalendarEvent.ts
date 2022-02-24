import { gql } from "@apollo/client";

export const GET_CALENDAR_EVENT = gql`
    query GetCalendarEvent($id: ID!) {
        calendarEvents {
            get(id: $id) {
                id,
                startsAt,
                endsAt,
                name,
                description,
                maximalParticipantsCount,
                authorId,
                communicationChannelId,
                createdAt,
                modifiedAt,
                createdBy,
                deletedBy,
                modifiedBy,
                type
            }
        }
    }
`;