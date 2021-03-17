using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Models;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Application.Groups.Queries.GetForUser;
using MediatR;

namespace Chattoo.Application.Common.Queries
{
    /// <summary>
    /// Bázová třída dotazu s podporou stránkování.
    /// </summary>
    public abstract class PaginatedQuery<TDto> : IRequest<PaginatedList<TDto>>
    {
        /// <summary>
        /// Vrací nebo nastavuje číslo stránky záznamů, na kterou je dotazováno.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Vrací nebo nastavuje počet záznamů na stránku.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
    
    public class PaginatedQueryHandler<TQuery, TDto> : IRequestHandler<TQuery, PaginatedList<TDto>> where TQuery : IRequest<PaginatedList<TDto>>
    {
        public virtual async Task<PaginatedList<TDto>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}