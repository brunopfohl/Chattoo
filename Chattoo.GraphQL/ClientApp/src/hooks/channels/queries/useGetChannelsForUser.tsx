import { gql, QueryResult, useQuery } from "@apollo/client";
import { CommunicationChannel } from "../../../common/interfaces/communication-channel.interface";
import { PaginatedList } from "../../../common/interfaces/paginated-list";
import { GetChannelsForUser, GetChannelsForUserVariables } from "../../../common/interfaces/schema-types";

export const GET_CHANNELS_FOR_USER = gql`
    query GetChannelsForUser($userId: String!, $pageNumber: Int!, $pageSize: Int!) {
        communicationChannels {
            getForUser(userId: $userId, pageNumber: $pageNumber, pageSize: $pageSize) {
                data {
                    id,
                    name,
                    description,
                    createdAt,
                    modifiedAt
                }
                hasNextPage
                hasPreviousPage
                pageIndex
                totalCount
                totalPages
            }
        }
    }
`;

export const useGetCommunicationChannelsForUser = (variables: GetChannelsForUserVariables): [PaginatedList<CommunicationChannel> | undefined, QueryResult<GetChannelsForUser, GetChannelsForUserVariables>] => {
    const query = useQuery<GetChannelsForUser, GetChannelsForUserVariables>(GET_CHANNELS_FOR_USER, {
        variables: variables
    });

    return [query.data?.communicationChannels.getForUser, query];
}