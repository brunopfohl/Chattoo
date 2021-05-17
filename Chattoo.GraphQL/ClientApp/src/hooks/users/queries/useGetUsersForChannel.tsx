import { gql, QueryResult, useQuery } from "@apollo/client";
import { AppUser } from "../../../common/interfaces/app-user.interface";
import { PaginatedList } from "../../../common/interfaces/paginated-list";
import { GetUsersForChannel, GetUsersForChannelVariables } from "../../../common/interfaces/schema-types";

export const GET_USERS_FOR_CHANNEL = gql`
    query GetUsersForChannel($channelId: ID!) {
        users {
            getForCommunicationChannel(channelId: $channelId) {
                data {
                    id
                    userName
                    createdAt
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

export const useGetUsersForChannel = (variables: GetUsersForChannelVariables): [PaginatedList<AppUser> | undefined, QueryResult<GetUsersForChannel, GetUsersForChannelVariables>] => {
    const query = useQuery<GetUsersForChannel, GetUsersForChannelVariables>(GET_USERS_FOR_CHANNEL, {
        variables: variables
    });

    return [query.data?.users.getForCommunicationChannel, query];
}