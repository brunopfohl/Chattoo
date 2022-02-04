using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Services;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Queries.GetById
{
    /// <summary>
    /// Dotaz na komunikační kanál s daným Id.
    /// </summary>
    public class GetCommunicationChannelByIdQuery : IRequest<CommunicationChannelDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }
    
    public class GetCommunicationChannelByIdQueryHandler : IRequestHandler<GetCommunicationChannelByIdQuery, CommunicationChannelDto>
    {
        private readonly IMapper _mapper;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public GetCommunicationChannelByIdQueryHandler(IMapper mapper, ICommunicationChannelRepository communicationChannelRepository, GetByIdUserSafeService getByIdUserSafeService)
        {
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<CommunicationChannelDto> Handle(GetCommunicationChannelByIdQuery request, CancellationToken cancellationToken)
        {
            // Načtu komunikační kanál.
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.Id);
            return _mapper.Map<CommunicationChannelDto>(channel);
        }
    }
}