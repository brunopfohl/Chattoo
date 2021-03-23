using FluentValidation;

namespace Chattoo.Application.CommunicationChannelRoles.Commands.Create
{
    /// <summary>
    /// Validátor příkazu <see cref="CreateCommunicationChannelRoleCommand"/>.
    /// </summary>
    public class CreateCommunicationChannelRoleCommandValidator : AbstractValidator<CreateCommunicationChannelRoleCommand>
    {
        public CreateCommunicationChannelRoleCommandValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty().WithMessage("Je nutné vyplnit Id komunikačního kanálu.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název uživatelské role v komunikačním kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název uživatelské role v komunikačním kanálu je nutné vyplnit.");
        }
    }
}