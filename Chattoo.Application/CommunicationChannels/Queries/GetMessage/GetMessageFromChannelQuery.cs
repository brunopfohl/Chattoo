using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Queries
{
    /// <summary>
    /// Dotaz na zprávy z komunikačního kanálu.
    /// </summary>
    public class GetMessageFromChannelQuery : IRequest<CommunicationChannelMessageDto>
    {
        public string ChannelId { get; set; }
        
        public string MessageId { get; set; }
    }

    public class GetMessageFromChannelQueryHandler : IRequestHandler<GetMessageFromChannelQuery, CommunicationChannelMessageDto>
    {
        private readonly IMapper _mapper;
        private readonly ChannelManager _channelManager;

        public GetMessageFromChannelQueryHandler(IMapper mapper, ChannelManager channelManager)
        {
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public async Task<CommunicationChannelMessageDto> Handle(GetMessageFromChannelQuery request, CancellationToken cancellationToken)
        {
            // Pokusím se načíst kanál.
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            var message = _channelManager.GetMessageOrThrow(channel, request.MessageId);

            return _mapper.Map<CommunicationChannelMessageDto>(message);
        }
    }
}