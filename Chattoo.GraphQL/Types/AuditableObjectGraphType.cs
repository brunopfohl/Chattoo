using Chattoo.Application.Common.DTOs;
using GraphQL.Types;

namespace Chattoo.GraphQL.Types
{
    public abstract class AuditableObjectGraphType<TDto> : ObjectGraphType<TDto> where TDto : AuditableDto
    {
        public AuditableObjectGraphType()
        {
            Field(o => o.Id);
            Field(o => o.CreatedAt, true);
            Field(o => o.CreatedBy, true);
            Field(o => o.DeletedAt, true);
            Field(o => o.DeletedBy, true);
            Field(o => o.ModifiedAt, true);
            Field(o => o.ModifiedBy, true);
        }
    }
}