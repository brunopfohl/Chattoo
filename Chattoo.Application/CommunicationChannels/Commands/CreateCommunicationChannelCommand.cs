using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CommunicationChannels.DTOs;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
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
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly ChannelManager _channelManager;

        public CreateCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, IMapper mapper, ChannelManager channelManager)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _mapper = mapper;
            _channelManager = channelManager;
        }

        public async Task<CommunicationChannelDto> Handle(CreateCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = _channelManager.Create(request.Name, request.Description);
            
            await _communicationChannelRepository.AddOrUpdateAsync(channel, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<CommunicationChannelDto>(channel);
        }
    }
}