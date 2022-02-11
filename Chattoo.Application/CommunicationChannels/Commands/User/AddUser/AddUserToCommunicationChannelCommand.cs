using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Services;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.AddUser
{
    /// <summary>
    /// Příkaz pro přidání uživatele do komunikačního kanálu.
    /// </summary>
    public class AddUserToCommunicationChannelCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele, který se má přidat do skupiny.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, do kterého se má přidat uživatel.
        /// </summary>
        public string ChannelId { get; set; }
    }

    public class AddUserToCommunicationChannelCommandHandler : IRequestHandler<AddUserToCommunicationChannelCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChannelManager _channelManager;

        public AddUserToCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _channelManager = channelManager;
        }

        public async Task<Unit> Handle(AddUserToCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _channelManager.GetChannelOrThrow(request.ChannelId);
            
            await _channelManager.AddParticipantToChannel(channel, request.UserId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}