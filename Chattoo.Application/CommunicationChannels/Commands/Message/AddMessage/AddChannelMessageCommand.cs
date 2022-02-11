using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro vytvoření zprávy v komunikačním kanálu.
    /// </summary>
    public class AddChannelMessageCommand : IRequest<CommunicationChannelMessageDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, pod který spadá tato zpráva.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje obsah zprávy.
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje typ zprávy.
        /// </summary>
        public CommunicationChannelMessageType Type { get; set; }
    }

    public class AddChannelMessageCommandHandler : IRequestHandler<AddChannelMessageCommand, CommunicationChannelMessageDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ChannelManager _channelManager;
        private readonly ICurrentUserService _currentUserService;

        public AddChannelMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ChannelManager channelManager, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _channelManager = channelManager;
            _currentUserService = currentUserService;
        }

        public async Task<CommunicationChannelMessageDto> Handle(AddChannelMessageCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);

            var message = channel.AddMessage(_currentUserService.User.Id, request.Content, request.Type);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CommunicationChannelMessageDto>(message);
        }
    }
}