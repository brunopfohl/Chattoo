using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání komunikačního kanálu.
    /// </summary>
    public class DeleteCommunicationChannelCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteCommunicationChannelCommandHandler : IRequestHandler<DeleteCommunicationChannelCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;

        public DeleteCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, GetByIdUserSafeService getByIdUserSafeService)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _getByIdUserSafeService = getByIdUserSafeService;
        }

        public async Task<Unit> Handle(DeleteCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.Id);
            
            _communicationChannelRepository.Remove(channel);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}