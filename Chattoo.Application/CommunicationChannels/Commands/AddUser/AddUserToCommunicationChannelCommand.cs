using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Services;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.AddUser
{
    /// <summary>
    /// Příkaz pro přidání uživatele do komunikačního kanálu.
    /// </summary>
    public class AddUserToCommunicationChannelCommand : IRequest<CommunicationChannelDto>
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

    public class AddUserToCommunicationChannelCommandHandler : IRequestHandler<AddUserToCommunicationChannelCommand, CommunicationChannelDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly IUserRepository _userRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public AddUserToCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, IUserRepository userRepository, IMapper mapper, GetByIdUserSafeService getByIdUserSafeService)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<CommunicationChannelDto> Handle(AddUserToCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);
            
            var user = await _getByIdUserSafeService.GetAsync(_userRepository, request.UserId);
            
            channel.AddParticipant(user.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CommunicationChannelDto>(channel);
        }
    }
}