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
        private readonly IGroupRoleRepository _groupRoleRepository;

        public GetGroupRoleByIdQueryHandler(IMapper mapper, IGroupRoleRepository groupRoleRepository)
        {
            _mapper = mapper;
            _groupRoleRepository = groupRoleRepository;
        }

        public async Task<GroupRoleDto> Handle(GetGroupRoleByIdQuery request, CancellationToken cancellationToken)
        {
            // Načtu uživatelskou roli z datového zdroje.
            var groupRole = await _groupRoleRepository.GetByIdAsync(request.Id);

            // Pokud se uživatelskou roli s daným Id nepodařilo dohledat, vracím chybu.
            if (groupRole is null)
            {
                throw new NotFoundException(nameof(GroupRole), request.Id);
            }

            // Převedu entitu na dto.
            var result = _mapper.Map<GroupRoleDto>(groupRole);

            return result;
        }
    }
}