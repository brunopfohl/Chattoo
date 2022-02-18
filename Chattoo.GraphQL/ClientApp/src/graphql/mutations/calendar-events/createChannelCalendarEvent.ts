import { gql } from "@apollo/client";

/**
 * GraphQL mutace pro vytvoření kalendářní události skrz API.
 */
export const CREATE_CHANNEL_CALENDAR_EVENT = gql`
    mutation CreateChannelCalendarEvent($channelId: String!, $name: String!, $desc: String!, $startsAt: Date!, $endsAt: Date!) {
        communicationChannelCalendarEvents {
            create(channelId: $channelId, name: $name, desc: $desc, startsAt: $startsAt, endsAt: $endsAt) {
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