using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelMessageAttachments.Commands.Update
{
    /// <summary>
    /// Příkaz pro upravení již existující přílohy zprávy z komunikačního kanálu.
    /// </summary>
    public class UpdateCommunicationChannelMessageAttachmentCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název přílohy.
        /// </summary>
        public string Name { get; set; }
    }
    
    public class UpdateCommunicationChannelMessageAttachmentCommandHandler : IRequestHandler<UpdateCommunicationChannelMessageAttachmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCommunicationChannelMessageAttachmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCommunicationChannelMessageAttachmentCommand request, CancellationToken cancellationToken)
        {
            // // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se ho nepodaří dohledat).
            // var entity = await _communicationChannelMessageAttachmentRepository.GetByIdAsync(request.Id, true);
            //
            // // Naplním entitu daty z příkazu.
            // entity.Name = request.Name;
            //
            // // Upravím záznam a uložím.
            // await _communicationChannelMessageAttachmentRepository.AddOrUpdateAsync(entity, cancellationToken);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);
            //
            return Unit.Value;
        }
    }
}