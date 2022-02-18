using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteChannelRoleCommand"/>.
    /// </summary>
    public class DeleteChannelRoleCommandValidator : AbstractValidator<DeleteChannelRoleCommand>
    {
        public DeleteChannelRoleCommandValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id uživatelské role z komunikačního kanálu.");
        }
    }
}