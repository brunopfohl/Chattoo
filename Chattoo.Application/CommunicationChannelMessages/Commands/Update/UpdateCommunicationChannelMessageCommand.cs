using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Repositories;
using MediatR;

namespace Chattoo.Application.CommunicationChannelMessages.Commands.Update
{
    /// <summary>
    /// Příkaz pro upravení již existující zprávy z komunikačního kanálu.
    /// </summary>
    public class UpdateCommunicationChannelMessageCommand : IRequest
    {
        /// <summary>
        /// Vrací nebo nastavuje Id zprávy z komunikačního kanálu.
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje obsah zprávy.
        /// </summary>
        public string Content { get; set; }
    }
    
    public class UpdateCommunicationChannelMessageCommandHandler : IRequestHandler<UpdateCommunicationChannelMessageCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCommunicationChannelMessageCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCommunicationChannelMessageCommand request, CancellationToken cancellationToken)
        {
            // // Vytáhnu záznam z datového zdroje (vyhodím výjimku, pokud se ho nepodařín dohledat).
            // var entity = await _communicationChannelMessageRepository.GetByIdAsync(request.Id, true);
            //
            // // Naplním entitu daty z příkazu.
            // entity.Content = request.Content;
            //
            // // Upravím záznam a uložím.
            // await _communicationChannelMessageRepository.AddOrUpdateAsync(entity, cancellationToken);
            // await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}