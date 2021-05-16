import { gql, QueryResult, useQuery } from "@apollo/client";
import { AppUser } from "../../../common/interfaces/app-user.interface";
import { PaginatedList } from "../../../common/interfaces/paginated-list";
import { GetUsers, GetUsersVariables } from "../../../common/interfaces/schema-types";

export const GET_USERS = gql`
    query GetUsers($searchTerm: String!, $excludeUsersFromChannelWithId: String) {
        users {
            get(searchTerm: $searchTerm, excludeUsersFromChannelWithId: $excludeUsersFromChannelWithId) {
                data {
                    id
                    userName
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

export const useGetUsers = (variables: GetUsersVariables): [PaginatedList<AppUser> | undefined, QueryResult<GetUsers, GetUsersVariables>] => {
    const query = useQuery<GetUsers, GetUsersVariables>(GET_USERS, {
        variables: variables
    });

    return [query.data?.users.get, query];
}