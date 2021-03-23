using FluentValidation;

namespace Chattoo.Application.CommunicationChannelRoles.Commands.Update
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateCommunicationChannelRoleCommand"/>.
    /// </summary>
    public class UpdateCommunicationChannelRoleCommandValidator : AbstractValidator<UpdateCommunicationChannelRoleCommand>
    {
        public UpdateCommunicationChannelRoleCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné vyplnit Id uživatelské role z komunikačního kanálu.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název uživatelské role v komunikačním kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název uživatelské role v komunikačním kanálu je nutné vyplnit.");
        }
    }
}