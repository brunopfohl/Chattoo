using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.Update
{
    /// <summary>
    /// Příkaz pro upravení již existujícího komunikačního kanálu.
    /// </summary>
    public class UpdateCommunicationChannelCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název komunikačního kanálu.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popis komunikačního kanálu.
        /// </summary>
        public string Description { get; set; }
    }
    
    public class UpdateCommunicationChannelCommandHandler : IRequestHandler<UpdateCommunicationChannelCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public UpdateCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<Unit> Handle(UpdateCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje.
            var entity = await _communicationChannelRepository.GetByIdAsync(request.Id);

            // Pokud se mi záznam nepodařilo najít, vrátím NotFoundException (zdroj nenalezen).
            if (entity is null)
            {
                throw new NotFoundException(nameof(CommunicationChannel), request.Id);
            }

            // Naplním entitu daty z příkazu.
            entity.Name = request.Name;
            entity.Description = request.Description;

            // Přidám záznam do datového zdroje a uložím.`
            await _communicationChannelRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}