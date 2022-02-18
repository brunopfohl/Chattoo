using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class GraphQLMutation : ObjectGraphType
    {
        public GraphQLMutation()
        {
            Name = "Mutation";

            Field<UserAliasMutation>("userAliases", resolve: context => new { });
            Field<GroupMutation>("groups", resolve: context => new { });
            Field<GroupRoleMutation>("groupRoles", resolve: context => new { });
            Field<CommunicationChannelMutation>("communicationChannels", resolve: context => new { });
            Field<CommunicationChannelMessageMutation>("communicationChannelMessages", resolve: context => new { });
            Field<CommunicationChannelRoleMutation>("communicationChannelRoles", resolve: context => new { });
            Field<CommunicationChannelMessageAttachmentMutation>("communicationChannelMessageAttachments", resolve: context => new { });
            Field<CalendarEventMutation>("communicationChannelCalendarEvents", resolve: context => new { });
        }
    }
}
