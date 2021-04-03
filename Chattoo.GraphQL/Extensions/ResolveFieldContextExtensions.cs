using GraphQL;

namespace Chattoo.GraphQL.Extensions
{
    public static class ResolveFieldContextExtensions
    {
        public static string GetString(this IResolveFieldContext ctx, string argumentName)
        {
            return ctx.GetArgument<string>(argumentName);
        }
        
        public static int GetInt(this IResolveFieldContext ctx, string argumentName)
        {
            return ctx.GetArgument<int>(argumentName);
        }
    }
}