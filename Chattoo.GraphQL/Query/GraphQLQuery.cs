using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class GraphQLQuery : ObjectGraphType<object>
    {
        public GraphQLQuery()
        {
            Name = "Query";
            
            Field<UserQuery>("users", resolve: context => new { });
            Field<UserAliasQuery>("userAliases", resolve: context => new { });
            Field<GroupQuery>("groups", resolve: context => new { });
            Field<GroupRoleQuery>("groupRoles", resolve: context => new { });
            Field<CommunicationChannelQuery>("communicationChannels", resolve: context => new { });
            Field<CommunicationChannelMessageQuery>("communicationChannelMessages", resolve: context => new { });
            Field<CommunicationChannelRoleQuery>("communicationChannelRoles", resolve: context => new { });
            Field<CalendarEventQuery>("calendarEvents", resolve: context => new { });
        }
    }
}
