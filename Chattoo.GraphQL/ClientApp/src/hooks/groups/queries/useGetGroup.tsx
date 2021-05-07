// import { gql, useQuery } from "@apollo/client";
// import { GetGroup, GetGroupVariables } from "../../../common/interfaces/schema-types";

// const GET_GROUP = gql`
//     query GetGroup($id: String!) {
//         groups {
//             get(id: $id) {
//                 id
//                 createdAt
//                 modifiedAt
//                 name
//             }
//         }
//     }
// `;

// export const useGetGroups = (variables: GetGroupVariables): GetGroup | undefined => {
//     const { data } = useQuery<GetGroup>(GET_GROUP, { variables });
//     return data;
// }