import { gql } from "@apollo/client";

export const CREATE_WISH = gql`
    mutation CreateWish($channelId: String!, $type: String!, $minimalParticipantsCount: Int!, $dateIntervals: [DateIntervalInput!]!) {
        wishes {
            create(channelId: $channelId, type: $type, minimalParticipantsCount: $minimalParticipantsCount, dateIntervals: $dateIntervals) {
                id,
                authorId,
                authorName,
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