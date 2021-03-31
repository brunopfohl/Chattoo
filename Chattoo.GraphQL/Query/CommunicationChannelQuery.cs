using Chattoo.Application.CommunicationChannels.Queries.GetById;
using Chattoo.GraphQL.Arguments;
using Chattoo.GraphQL.Extensions;
using Chattoo.GraphQL.Types;
using GraphQL;
using GraphQL.Types;

namespace Chattoo.GraphQL.Query
{
    public class CommunicationChannelQuery : ObjectGraphType
    {
        public CommunicationChannelQuery()
        {
            Name = "CommunicationChannelQuery";
            this.FieldAsyncWithScope<ListGraphType<CommunicationChannelType>, object, object>(
                "get",
                arguments: 
                new QueryArgumentsWithPagination
                (
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ),
                resolve: async (context, mediator) =>
                {
                    var query = new GetCommunicationChannelByIdQuery()
                    {
                        Id = context.GetArgument<string>("id")
                    };

                    return await mediator.Send(query);
                }
            );

        }
    }
}