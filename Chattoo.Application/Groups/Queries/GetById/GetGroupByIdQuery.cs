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
            // // Načtu skupinu z datového zdroje (vyhodím výjimku, pokud se mi skupinu nepodaří dohledat).
            // var group = await _groupRepository.GetByIdAsync<GroupDto>(request.Id, true);
            // return group;
            return null;
        }
    }
}