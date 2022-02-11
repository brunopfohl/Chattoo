using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Services;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro odebrání uživatele z komunikačního kanálu.
    /// </summary>
    public class RemoveUserFromCommunicationChannelCommand : IRequest<CommunicationChannelDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který se má odebrat ze skupiny.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, ze kterého se má odebrat uživatel.
        /// </summary>
        public string ChannelId { get; set; }
    }

    public class RemoveUserFromCommunicationChannelCommandHandler : IRequestHandler<RemoveUserFromCommunicationChannelCommand, CommunicationChannelDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;

        public RemoveUserFromCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public async Task<CommunicationChannelDto> Handle(RemoveUserFromCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);
            
            channel.RemoveParticipant(request.UserId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CommunicationChannelDto>(channel);
        }
    }
}