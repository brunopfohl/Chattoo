using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.DeleteMessage
{
    /// <summary>
    /// Příkaz pro smazání role z komunikačního kanálu.
    /// </summary>
    public class DeleteChannelMessageCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy v komunikačním kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteChannelMessageCommandHandler : IRequestHandler<DeleteChannelMessageCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public DeleteChannelMessageCommandHandler(IUnitOfWork unitOfWork, GetByIdUserSafeService getByIdUserSafeService, ICommunicationChannelRepository communicationChannelRepository)
        {
            _unitOfWork = unitOfWork;
            _getByIdUserSafeService = getByIdUserSafeService;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<Unit> Handle(DeleteChannelMessageCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);

            channel.DeleteMessage(request.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}