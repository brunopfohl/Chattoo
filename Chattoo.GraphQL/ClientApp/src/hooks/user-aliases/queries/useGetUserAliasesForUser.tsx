// import { gql, useQuery } from "@apollo/client";
// import { GetAliasesForUser, GetAliasesForUserVariables, GetGroup, GetGroupVariables } from "../../../common/interfaces/schema-types";

// const GET_ALIASES_FOR_USER = gql`
//     query GetAliasesForUser($userId: String) {
//         userAliases {
//             getForUser(userId: $userId) {
//                 data {
//                     id
//                     userId
//                     alias
//                     createdAt
//                     modifiedAt
//                 }
//                 hasNextPage
//                 hasPreviousPage
//                 pageIndex
//                 totalCount
//                 totalPages
//             }
//         }
//     }
// `;

// export const useGetGroup = (variables: GetAliasesForUserVariables): GetAliasesForUser | undefined => {
//     const { data } = useQuery<GetAliasesForUser>(GET_ALIASES_FOR_USER, { variables });
//     return data;
// }