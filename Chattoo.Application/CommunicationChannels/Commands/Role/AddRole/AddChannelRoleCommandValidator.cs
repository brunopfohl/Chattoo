using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="AddChannelRoleCommand"/>.
    /// </summary>
    public class AddChannelRoleCommandValidator : AbstractValidator<AddChannelRoleCommand>
    {
        public AddChannelRoleCommandValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.Name)
                .NotEmpty()
                    .WithMessage("Název uživatelské role v komunikačním kanálu je nutné vyplnit.")
                .MaximumLength(100)
                    .WithMessage("Název uživatelské role v komunikačním kanálu nesmí být delší než 100 znaků.");
            
            RuleFor(v => v.Permission)
                .NotEmpty()
                    .WithMessage("Nebylo speficikováno oprávnění skupiny");
        }
    }
}