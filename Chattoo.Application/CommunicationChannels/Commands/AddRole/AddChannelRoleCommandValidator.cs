using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="AddChannelRoleCommand"/>.
    /// </summary>
    public class AddChannelRoleCommandValidator : AbstractValidator<AddChannelRoleCommand>
    {
        public AddChannelRoleCommandValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty().WithMessage("Je nutné vyplnit Id komunikačního kanálu.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název uživatelské role v komunikačním kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název uživatelské role v komunikačním kanálu je nutné vyplnit.");
        }
    }
}