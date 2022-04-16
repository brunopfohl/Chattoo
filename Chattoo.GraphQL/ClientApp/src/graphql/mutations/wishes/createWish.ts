import { gql } from "@apollo/client";

export const CREATE_WISH = gql`
    mutation CreateWish($channelId: String!, $name: String!, $type: String!, $minimalParticipantsCount: Int!, $minimalLengthInMinutes: Long!, $dateIntervals: [DateIntervalInput!]!) {
        wishes {
            create(channelId: $channelId, name: $name, type: $type, minimalParticipantsCount: $minimalParticipantsCount, minimalLengthInMinutes: $minimalLengthInMinutes, dateIntervals: $dateIntervals) {
                id,
                name,
                minimalParticipantsCount,
                minimalLengthInMinutes,
                dateIntervals {
                    startsAt,
                    endsAt
                }
                createdAt,
                modifiedAt
            }
        }
    }
`;