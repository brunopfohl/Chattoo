using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.UpdateMessage
{
    /// <summary>
    /// Příkaz pro upravení již existující zprávy z komunikačního kanálu.
    /// </summary>
    public class UpdateChannelMessageCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje obsah zprávy.
        /// </summary>
        public string Content { get; set; }
    }
    
    public class UpdateChannelMessageCommandHandler : IRequestHandler<UpdateChannelMessageCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public UpdateChannelMessageCommandHandler(IUnitOfWork unitOfWork, GetByIdUserSafeService getByIdUserSafeService, ICommunicationChannelRepository communicationChannelRepository)
        {
            _unitOfWork = unitOfWork;
            _getByIdUserSafeService = getByIdUserSafeService;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<Unit> Handle(UpdateChannelMessageCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);

            channel.UpdateMessage(request.Id, request.Content);
            
           await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}