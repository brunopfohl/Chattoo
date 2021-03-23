using Chattoo.Application.CommunicationChannels.Commands.Delete;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannelRoles.Commands.Delete
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteCommunicationChannelRoleCommand"/>.
    /// </summary>
    public class DeleteCommunicationChannelRoleCommandValidator : AbstractValidator<DeleteCommunicationChannelRoleCommand>
    {
        public DeleteCommunicationChannelRoleCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné vyplnit Id uživatelské role z komunikačního kanálu.");
        }
    }
}