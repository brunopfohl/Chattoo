using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelMessages.Commands.Delete
{
    /// <summary>
    /// Příkaz pro smazání role z komunikačního kanálu.
    /// </summary>
    public class DeleteCommunicationChannelMessageCommand : IRequest<Unit>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy v komunikačním kanálu.
        /// </summary>
        public string Id { get; set; }
    }

    public class DeleteCommunicationChannelMessageCommandHandler : IRequestHandler<DeleteCommunicationChannelMessageCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommunicationChannelMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCommunicationChannelMessageCommand request, CancellationToken cancellationToken)
        {
            // // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se ho nepodaří dohledat).
            // var entity = await _communicationChannelMessageRepository.GetByIdAsync(request.Id, true);
            //
            // // Záznam se podařilo nalézt -> smažu ho a uložím změny.
            // _communicationChannelMessageRepository.Remove(entity);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}