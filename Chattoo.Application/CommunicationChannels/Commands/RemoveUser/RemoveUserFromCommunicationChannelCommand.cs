using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Services;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.RemoveUser
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
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly IUserRepository _userRepository;

        public RemoveUserFromCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, IUserRepository userRepository, IMapper mapper, GetByIdUserSafeService getByIdUserSafeService)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<CommunicationChannelDto> Handle(RemoveUserFromCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);
            
            channel.RemoveParticipant(request.UserId);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CommunicationChannelDto>(channel);
        }
    }
}