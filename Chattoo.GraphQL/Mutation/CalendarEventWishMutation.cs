using GraphQL.Types;

namespace Chattoo.GraphQL.Mutation
{
    public class CalendarEventWishMutation : ObjectGraphType
    {
        
        // TODO: Dodělat. Zatím mě drží zpátky, že potřebuji mít možnost zaslat kolekci časových intervalů.
        public CalendarEventWishMutation()
        {
            Name = "CalendarEventWishMutation";
        }
    }
}