using Chattoo.Application.Common.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public abstract class AuditableObjectGraphType<TDto> : ObjectGraphType<TDto> where TDto : AuditableDto
    {
        public AuditableObjectGraphType()
        {
            Field(o => o.Id);
            Field(o => o.CreatedAt);
            Field(o => o.CreatedBy);
            Field(o => o.DeletedAt, true);
            Field(o => o.DeletedBy);
            Field(o => o.ModifiedAt, true);
            Field(o => o.ModifiedBy);
        }
    }
}