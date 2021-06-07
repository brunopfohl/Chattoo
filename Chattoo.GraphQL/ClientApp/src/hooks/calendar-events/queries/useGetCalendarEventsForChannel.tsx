import { gql, QueryResult, useQuery } from "@apollo/client";
import { CalendarEvent } from "../../../common/interfaces/calendar-event";
import { PaginatedList } from "../../../common/interfaces/paginated-list";
import { GetCalendarEventsForChannel, GetCalendarEventsForChannelVariables } from "../../../common/interfaces/schema-types";

export const GET_CALENDAR_EVENTS_FOR_CHANNEL = gql`
    query GetCalendarEventsForChannel($channelId: String!) {
        communicationChannelCalendarEvents {
            getForCommunicationChannel(channelId: $channelId) {
                data {
                    id,
                    startsAt,
                    endsAt,
                    name,
                    description,
                    authorId,
                    authorName,
                    createdAt,
                    modifiedAt
                },
                hasNextPage,
                hasPreviousPage,
                pageIndex,
                totalCount,
                totalPages
            }
        }
    }
`;

export const useGetCalendarEventsForChannel = (variables: GetCalendarEventsForChannelVariables): [PaginatedList<CalendarEvent> | undefined, QueryResult<GetCalendarEventsForChannel, GetCalendarEventsForChannelVariables>] => {
    const query = useQuery<GetCalendarEventsForChannel, GetCalendarEventsForChannelVariables>(GET_CALENDAR_EVENTS_FOR_CHANNEL, {
        variables: variables
    });

    return [query.data?.communicationChannelCalendarEvents.getForCommunicationChannel, query];
}