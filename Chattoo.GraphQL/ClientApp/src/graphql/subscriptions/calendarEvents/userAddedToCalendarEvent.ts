import { gql } from "@apollo/client";

export const USER_ADDED_TO_CALENDAR_EVENT_SUBSCRIPTION = gql`
    subscription UserAddedToEvent() {
        userAddedToEvent {
            id,
            name,
            startsAt,
            endsAt,
            description,
            communicationChannelId,
            createdAt,
            modifiedAt
        }
    }
`;