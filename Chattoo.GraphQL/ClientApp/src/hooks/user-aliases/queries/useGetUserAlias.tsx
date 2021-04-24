import { gql, useQuery } from "@apollo/client";
import { GetUserAlias, GetUserAliasVariables } from "../../../common/interfaces/schema-types";

const GET_USER_ALIAS = gql`
    query GetUserAlias($id: String) {
        userAliases {
            get(id: $id) {
                id
                userId
                alias
                createdAt
                modifiedAt
            }
        }
    }
`;

export const useGetUserAlias = (variables: GetUserAliasVariables): GetUserAlias | undefined => {
    const { data } = useQuery<GetUserAlias>(GET_USER_ALIAS, { variables });
    return data;
}
