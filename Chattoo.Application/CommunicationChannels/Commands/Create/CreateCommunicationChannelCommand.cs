using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannels.Commands.Create
{
    /// <summary>
    /// Příkaz pro vytvoření komunikačního kanálu.
    /// </summary>
    public class CreateCommunicationChannelCommand : IRequest<string>
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

    public class CreateCommunicationChannelCommandHandler : IRequestHandler<CreateCommunicationChannelCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;

        public CreateCommunicationChannelCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
        }

        public async Task<string> Handle(CreateCommunicationChannelCommand request, CancellationToken cancellationToken)
        {
            // Vytvořím entitu naplněnou daty z příkazu.
            var entity = new CommunicationChannel()
            {
                Name = request.Name,
                Description = request.Description
            };

            // Přidám záznam do datového zdroje a uložím.
            await _communicationChannelRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Vrátím Id vytvořeného záznamu.
            return entity.Id;
        }
    }
}