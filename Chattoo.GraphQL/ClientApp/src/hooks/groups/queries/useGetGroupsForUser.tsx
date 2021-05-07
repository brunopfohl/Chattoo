// import { gql, useQuery } from "@apollo/client";
// import { GetGroupsForUser, GetGroupsForUserVariables } from "../../../common/interfaces/schema-types";

// const GET_GROUPS_FOR_USER = gql`
//     query GetGroupsForUser($userId: String!) {
//         groups {
//             getForUser(userId: $userId) {
//                 data {
//                     id
//                     name
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

// export const useGetGroup = (variables: GetGroupsForUserVariables): GetGroupsForUser | undefined => {
//     const { data } = useQuery<GetGroupsForUser>(GET_GROUPS_FOR_USER, { variables });
//     return data;
// }