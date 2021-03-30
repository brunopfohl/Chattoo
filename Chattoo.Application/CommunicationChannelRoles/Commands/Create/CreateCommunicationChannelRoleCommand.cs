using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelRoles.Commands.Create
{
    /// <summary>
    /// Příkaz pro vytvoření uživatelské role v komunikačním kanálu.
    /// </summary>
    public class CreateCommunicationChannelRoleCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, pod který spadá tato role.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název role.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje práva uživatele, který disponuje touto rolí.
        /// </summary>
        public CommunicationChannelPermission Permission { get; set; }
    }

    public class CreateCommunicationChannelRoleCommandHandler : IRequestHandler<CreateCommunicationChannelRoleCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelRepository _communicationChannelRepository;
        private readonly ICommunicationChannelRoleRepository _communicationChannelRoleRepository;

        public CreateCommunicationChannelRoleCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelRepository communicationChannelRepository, ICommunicationChannelRoleRepository communicationChannelRoleRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelRepository = communicationChannelRepository;
            _communicationChannelRoleRepository = communicationChannelRoleRepository;
        }

        public async Task<string> Handle(CreateCommunicationChannelRoleCommand request, CancellationToken cancellationToken)
        {
            // Zjistím, zda-li komunikační kanál skutečně existuje.
            _communicationChannelRepository.ThrowIfNotExists(request.ChannelId);
            
            // Vytvořím entitu naplněnou daty z příkazu.
            var entity = new CommunicationChannelRole()
            {
                ChannelId = request.ChannelId,
                Name = request.Name
            };

            // Přidám záznam do datového zdroje a uložím.
            await _communicationChannelRoleRepository.AddOrUpdateAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Vrátím Id vytvořeného záznamu.
            return entity.Id;
        }
    }
}