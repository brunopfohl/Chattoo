import { gql, useMutation } from "@apollo/client";
import { DeleteChannelCalendarEventVariables } from "../../../common/interfaces/schema-types";

export interface DeleteChannelCalendarEventInput {
    variables: DeleteChannelCalendarEventVariables
}

export const DELETE_CHANNEL_CALENDAR_EVENT = gql`
    mutation DeleteChannelCalendarEvent($id: String!) {
        communicationChannelCalendarEvents {
            delete(id: $id)
        }
    }
`;

export const useDeleteChannelCalendarEvent = (): ((
    input: DeleteChannelCalendarEventInput,
) => any) => {
    const [deleteChannelCalendarEvent] = useMutation(DELETE_CHANNEL_CALENDAR_EVENT, {
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

    return deleteChannelCalendarEvent;
}