using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.AddMessage
{
    /// <summary>
    /// Příkaz pro vytvoření zprávy v komunikačním kanálu.
    /// </summary>
    public class AddChannelMessageCommand : IRequest<CommunicationChannelMessageDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele (autora) této zprávy.
        /// </summary>
        public string UserId { get; set; }
        
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
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public AddChannelMessageCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, IMapper mapper, GetByIdUserSafeService getByIdUserSafeService, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _mapper = mapper;
            _getByIdUserSafeService = getByIdUserSafeService;
            _currentUserService = currentUserService;
        }

        public async Task<CommunicationChannelMessageDto> Handle(AddChannelMessageCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);

            var message = channel.AddMessage(_currentUserService.User?.Id, request.Content, request.Type);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CommunicationChannelMessageDto>(message);
        }
    }
}