using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Queries.GetById
{
    /// <summary>
    /// Dotaz na skupiny s daným Id.
    /// </summary>
    public class GetGroupByIdQuery : IRequest<GroupDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny uživatelů.
        /// </summary>
        public string Id { get; set; }
    }
    
    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GroupDto>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;

        public GetGroupByIdQueryHandler(IGroupRepository groupRepository, IUserRepository userRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<GroupDto> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            // Načtu skupinu z datového zdroje.
            var group = await _groupRepository.GetByIdAsync(request.Id);

            // Pokud se skupinu s daným Id nepodařilo dohledat, vracím chybu.
            if (group is null)
            {
                throw new NotFoundException(nameof(Group), request.Id);
            }

            // Převedu entitu na dto.
            var result = _mapper.Map<GroupDto>(group);

            return result;
        }
    }
}