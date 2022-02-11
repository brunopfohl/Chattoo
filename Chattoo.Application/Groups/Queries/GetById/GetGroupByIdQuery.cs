using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Groups.DTOs;
using Chattoo.Domain.Services;
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
        private readonly GroupManager _groupManager;

        public GetGroupByIdQueryHandler(IMapper mapper, GroupManager groupManager)
        {
            _mapper = mapper;
            _groupManager = groupManager;
        }

        public async Task<GroupDto> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupManager.GetGroupOrThrow(request.Id);

            return _mapper.Map<GroupDto>(group);
        }
    }
}