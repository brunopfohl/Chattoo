using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.Create
{
    /// <summary>
    /// Příkaz pro vytvoření komunikačního kanálu.
    /// </summary>
    public class CreateCommunicationChannelCommand : IRequest<CommunicationChannelDto>
    {
        /// <summary>
        /// Vrací nebo nastavuje název komunikačního kanálu.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popis komunikačního kanálu.
        /// </summary>
        public string Description { get; set; }
    }

    public class CreateCommunicationChannelCommandHandler : IRequestHandler<CreateCommunicationChannelCommand, CommunicationChannelDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public CreateCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<CommunicationChannelDto> Handle(CreateCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = CommunicationChannel.Create(request.Name, request.Description);
            
            channel.AddParticipant(_currentUserService.User.Id);

            await _communicationChannelRepository.AddOrUpdateAsync(channel, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CommunicationChannelDto>(channel);
        }
    }
}