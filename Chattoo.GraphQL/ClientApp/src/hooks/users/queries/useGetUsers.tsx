import { DocumentNode, gql, QueryResult, useQuery } from "@apollo/client";
import { stringify } from "node:querystring";
import React, { useEffect } from "react";
import { CustomQueryResult } from "../../../common/classes/CustomQueryResult";
import { GetQueryProps } from "../../../common/interfaces/get-query-props.interface";
import { PaginatedList } from "../../../common/interfaces/paginated-list";
import { GetUsers, GetUsersVariables } from "../../../common/interfaces/schema-types";
import { User } from "../../../common/interfaces/user.interface";

export const GET_USERS = gql`
    query GetUsers($searchTerm: String!) {
        users {
            get(searchTerm: $searchTerm) {
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


// export interface GetUsersQueryProps extends GetQueryProps<PaginatedList<User>, GetUsersVariables, GetUsers> {

// }

// export const useGetUsers = (props: GetUsersQueryProps) => {
//     props.result = useQuery<GetUsers, GetUsersVariables>(GET_USERS, { variables: props.variables });
//     props.data = props.result.data.users.get;

//     return props.result;
// };

export const useGetUsers = (variables: GetUsersVariables): [PaginatedList<User> | undefined, QueryResult<GetUsers, GetUsersVariables>] => {
    const query = useQuery<GetUsers, GetUsersVariables>(GET_USERS, {
        variables: variables
    });

    return [query.data?.users.get, query];
}