import { gql, useQuery } from "@apollo/client";
import { GetUsersForGroup, GetUsersForGroupVariables } from "../../../common/interfaces/schema-types";

const GET_USERS_FOR_GROUP = gql`
    query GetUsersForGroup($groupId: String) {
        users {
            getForGroup(groupId: $groupId) {
                data {
                    createdAt
                    id
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

export const useGetUsersForGroup = (variables: GetUsersForGroupVariables): GetUsersForGroup | undefined => {
    const { data } = useQuery<GetUsersForGroup>(GET_USERS_FOR_GROUP, { variables });
    return data;
}