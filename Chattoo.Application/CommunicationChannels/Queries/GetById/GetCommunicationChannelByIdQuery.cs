using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Services;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
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
        private readonly ChannelManager _channelManager;

        public GetCommunicationChannelByIdQueryHandler(IMapper mapper, ChannelManager channelManager)
        {
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public async Task<CommunicationChannelDto> Handle(GetCommunicationChannelByIdQuery request, CancellationToken cancellationToken)
        {
            // Načtu komunikační kanál.
            var channel = await _channelManager.GetChannelOrThrow(request.Id);
            
            return _mapper.Map<CommunicationChannelDto>(channel);
        }
    }
}