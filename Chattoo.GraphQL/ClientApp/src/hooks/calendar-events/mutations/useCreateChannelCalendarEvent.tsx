import { gql, useMutation } from "@apollo/client";
import { CreateChannelCalendarEventVariables } from "../../../common/interfaces/schema-types";

export interface CreateChannelCalendarEventInput {
    variables: CreateChannelCalendarEventVariables
}

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
                authorName,
                createdAt,
                modifiedAt
            }
        }
    }
`;

export const useCreateChannelCalendarEvent = (): ((
    input: CreateChannelCalendarEventInput,
) => any) => {
    const [createChannelCalendarEvent] = useMutation(CREATE_CHANNEL_CALENDAR_EVENT, {
        update(cache, { data }) {
            // const cacheId = cache.identify(data.communicationChannels.create);
            
            // cache.modify({
            //     fields: {
            //         communicationChannels: (existingFieldData, { toReference }) => {
            //             return existingFieldData;
            //         }
            //     }
            // });
        }
    });

    return createChannelCalendarEvent;
}