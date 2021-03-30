using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelMessageAttachments.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání role z komunikačního kanálu.
    /// </summary>
    public class DeleteCommunicationChannelMessageAttachmentCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy v komunikačním kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteCommunicationChannelMessageAttachmentCommandHandler : IRequestHandler<DeleteCommunicationChannelMessageAttachmentCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommunicationChannelMessageAttachmentRepository _communicationChannelMessageAttachmentRepository;

        public DeleteCommunicationChannelMessageAttachmentCommandHandler(IUnitOfWork unitOfWork, ICommunicationChannelMessageAttachmentRepository communicationChannelMessageAttachmentRepository)
        {
            _unitOfWork = unitOfWork;
            _communicationChannelMessageAttachmentRepository = communicationChannelMessageAttachmentRepository;
        }

        public async Task<Unit> Handle(DeleteCommunicationChannelMessageAttachmentCommand request, CancellationToken cancellationToken)
        {
            // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se ho nepodaří dohledat).
            var entity = await _communicationChannelMessageAttachmentRepository.GetByIdAsync(request.Id, true);
            
            // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            _communicationChannelMessageAttachmentRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}