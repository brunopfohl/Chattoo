import { gql, QueryResult, useQuery } from "@apollo/client";
import { CalendarEvent } from "../../../common/interfaces/calendar-event";
import { GetChannelCalendarEvent, GetChannelCalendarEventVariables } from "../../../common/interfaces/schema-types";

export const GET_CHANNEL_CALENDAR_EVENT = gql`
    query GetChannelCalendarEvent($id: ID!) {
        communicationChannelCalendarEvents {
            get(id: $id) {
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

export const useGetChannelCalendarEvent = (variables: GetChannelCalendarEventVariables): [CalendarEvent | undefined, QueryResult<GetChannelCalendarEvent, GetChannelCalendarEventVariables>] => {
    const query = useQuery<GetChannelCalendarEvent, GetChannelCalendarEventVariables>(GET_CHANNEL_CALENDAR_EVENT, {
        variables: variables
    });

    return [query.data?.communicationChannelCalendarEvents.get, query];
}