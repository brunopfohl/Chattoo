using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Příkaz pro smazání role z komunikačního kanálu.
    /// </summary>
    public class DeleteChannelRoleCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje Id role y komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteChannelRoleCommandHandler : IRequestHandler<DeleteChannelRoleCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly GetByIdUserSafeService _getByIdUserSafeService;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public DeleteChannelRoleCommandHandler(IUnitOfWork unitOfWork, GetByIdUserSafeService getByIdUserSafeService, ICommunicationChannelRepository communicationChannelRepository)
        {
            _unitOfWork = unitOfWork;
            _getByIdUserSafeService = getByIdUserSafeService;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<Unit> Handle(DeleteChannelRoleCommand request, CancellationToken cancellationToken)
        {
            var channel = await _getByIdUserSafeService.GetAsync(_communicationChannelRepository, request.ChannelId);
            
            channel.DeleteRole(request.Id);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}