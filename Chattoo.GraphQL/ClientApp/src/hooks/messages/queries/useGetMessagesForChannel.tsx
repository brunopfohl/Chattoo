import { gql, QueryResult, useQuery } from "@apollo/client";
import { Message } from "../../../common/interfaces/message.interface";
import { PaginatedList } from "../../../common/interfaces/paginated-list";
import { GetMessagesForChannel, GetMessagesForChannelVariables } from "../../../common/interfaces/schema-types";

export const GET_MESSAGES_FOR_CHANNEL = gql`
    query GetMessagesForChannel($channelId: ID!, $pageNumber: Int!, $pageSize: Int!) {
        communicationChannelMessages {
            getForChannel(channelId: $channelId, pageNumber: $pageNumber, pageSize: $pageSize) {
            data {
                id,
                content,
                type,
                userId,
                channelId,
                createdAt,
                modifiedAt,
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

export const useGetMessagesForChannel = (variables: GetMessagesForChannelVariables): [PaginatedList<Message> | undefined, QueryResult<GetMessagesForChannel, GetMessagesForChannelVariables>] => {
    const query = useQuery<GetMessagesForChannel, GetMessagesForChannelVariables>(GET_MESSAGES_FOR_CHANNEL, {
        variables: variables
    });

    return [query.data?.communicationChannelMessages.getForChannel, query];
}