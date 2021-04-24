import { gql, useQuery } from "@apollo/client";
import { GetUsersForChannel, GetUsersForChannelVariables } from "../../../common/interfaces/schema-types";

const GET_USERS_FOR_CHANNEL = gql`
    query GetUsersForChannel($channelId: String) {
        users {
            getForGroup(channelId: $channelId) {
                data {
                    id
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

export const useGetUsersForChannel = (variables: GetUsersForChannelVariables): GetUsersForChannel | undefined => {
    const { data } = useQuery<GetUsersForChannel>(GET_USERS_FOR_CHANNEL, { variables });
    return data;
}