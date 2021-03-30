using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

        public GetCommunicationChannelByIdQueryHandler(IMapper mapper, ICommunicationChannelRepository communicationChannelRepository)
        {
            _mapper = mapper;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<CommunicationChannelDto> Handle(GetCommunicationChannelByIdQuery request, CancellationToken cancellationToken)
        {
            // Načtu komunikační kanál z datového zdroje (vyhodím výjimku, pokud se mi to nepodaří).
            var channel = await _communicationChannelRepository.GetByIdAsync<CommunicationChannelDto>(request.Id);
            return channel;
        }
    }
}