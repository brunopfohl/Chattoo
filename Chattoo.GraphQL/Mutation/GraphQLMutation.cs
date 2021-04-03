using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class GraphQLMutation : ObjectGraphType
    {
        public GraphQLMutation()
        {
            Name = "Mutation";

            Field<UserMutation>("users", resolve: context => new { });
            Field<UserAliasMutation>("userAliases", resolve: context => new { });
            Field<GroupMutation>("groups", resolve: context => new { });
            Field<GroupRoleMutation>("groupRoles", resolve: context => new { });
            Field<CommunicationChannelMutation>("communicationChannels", resolve: context => new { });
            Field<CommunicationChannelMessageMutation>("communicationChannelMessages", resolve: context => new { });
            Field<CommunicationChannelRoleMutation>("communicationChannelRoles", resolve: context => new { });
            Field<CommunicationChannelMessageAttachmentMutation>("communicationChannelMessageAttachments", resolve: context => new { });
        }
    }
}
