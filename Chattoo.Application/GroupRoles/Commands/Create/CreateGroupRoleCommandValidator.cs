using FluentValidation;

namespace Chattoo.Application.GroupRoles.Commands.Create
{
    /// <summary>
    /// Validátor příkazu <see cref="CreateGroupRoleCommand"/>.
    /// </summary>
    public class CreateGroupRoleCommandValidator : AbstractValidator<CreateGroupRoleCommand>
    {
        public CreateGroupRoleCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název uživatelské role nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název skupiny je nutné vyplnit.");
        }
    }
}