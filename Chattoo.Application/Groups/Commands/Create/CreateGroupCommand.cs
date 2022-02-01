using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Application.GroupRoles.Commands.Create;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.Groups.Commands.Create
{
    /// <summary>
    /// Příkaz pro vytvoření skupiny uživatelů.
    /// </summary>
    public class CreateGroupCommand : IRequest<string>
    {
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
    }

    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGroupRepository _groupRepository;

        public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IGroupRepository groupRepository)
        {
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
        }

        public async Task<string> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            // // Vytvořím entitu naplněnou daty z příkazu.
            // var entity = new Group()
            // {
            //     Name = request.Name
            // };
            //
            // // Přidám záznam do datového zdroje a uložím.`
            // await _groupRepository.AddOrUpdateAsync(entity, cancellationToken);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);
            //
            // // Vrátím Id vytvořeného záznamu.
            // return entity.Id;

            return null;
        }
    }
}