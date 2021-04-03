using System;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Chattoo.GraphQL.Extensions
{
    public static class ObjectGraphTypeExtensions
    {
        public static FieldType FieldAsyncWithScope<TGraphType, TReturnType, TSourceType>(this ObjectGraphType<TSourceType> o,
            string name,
            string description = null,
            QueryArguments arguments = null,
            Func<IResolveFieldContext<TSourceType>, ISender, Task<TReturnType>> resolve = null,
            string deprecationReason = null)
            where TGraphType : IGraphType
        {
            Func<IResolveFieldContext<TSourceType>, Task<TReturnType>> finalResolver = null;
            
            if (resolve is not null)
            {
                finalResolver = async context =>
                {
                    using (var scope = context.RequestServices.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetService<ISender>();

                        return await resolve(context, mediator);
                    }
                };
            }

            return o.FieldAsync<TGraphType, TReturnType>(name, description, arguments, finalResolver, deprecationReason);
        }

        public static FieldType FieldAsyncWithScope<TGraphType, TReturnType>(this ObjectGraphType o,
            string name,
            string description = null,
            QueryArguments arguments = null,
            Func<IResolveFieldContext, ISender, Task<TReturnType>> resolve = null,
            string deprecationReason = null)
            where TGraphType : IGraphType
        {
            return FieldAsyncWithScope<TGraphType, TReturnType, object>(o, name, description, arguments, resolve,
                deprecationReason);
        }
    }
}