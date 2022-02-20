import { gql } from "@apollo/client";

/**
 * GraphQL mutace pro vytvoření kalendářní události skrz API.
 */
export const CREATE_CHANNEL_CALENDAR_EVENT = gql`
    mutation CreateChannelCalendarEvent($name: String!, $desc: String!, $startsAt: Date!, $endsAt: Date, $channelId: String, $groupId: String, $eventTypeId: String!, $maximalParticipantsCount: Int) {
        communicationChannelCalendarEvents {
            create(name: $name, desc: $desc, startsAt: $startsAt, endsAt: $endsAt, channelId: $channelId, groupId: $groupId, eventTypeId: $eventTypeId, maximalParticipantsCount: $maximalParticipantsCount) {
                id,
                startsAt,
                endsAt,
                name,
                description,
                authorId,
                createdAt,
                modifiedAt
            }
        }
    }
`;