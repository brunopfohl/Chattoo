using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.GroupRoles.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.GroupRoles.Queries.GetById
{
    /// <summary>
    /// Dotaz na uživatelskou roli s daným Id.
    /// </summary>
    public class GetGroupRoleByIdQuery : IRequest<GroupRoleDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů.
        /// </summary>
        public string Id { get; set; }
    }
    
    public class GetGroupRoleByIdQueryHandler : IRequestHandler<GetGroupRoleByIdQuery, GroupRoleDto>
    {
        private readonly IMapper _mapper;

        public GetGroupRoleByIdQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<GroupRoleDto> Handle(GetGroupRoleByIdQuery request, CancellationToken cancellationToken)
        {
            // // Načtu uživatelskou roli z datového zdroje (vyhodím výjimku, pokud se mi ji nepodaří dohledat).
            // var groupRole = await _groupRoleRepository.GetByIdAsync<GroupRoleDto>(request.Id, true);
            // return groupRole;
            return null;
        }
    }
}