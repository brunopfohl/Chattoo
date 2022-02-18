using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteCommunicationChannelCommand"/>.
    /// </summary>
    public class DeleteCommunicationChannelCommandValidator : AbstractValidator<DeleteCommunicationChannelCommand>
    {
        public DeleteCommunicationChannelCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");
        }
    }
}