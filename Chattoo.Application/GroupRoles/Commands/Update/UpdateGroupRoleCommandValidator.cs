using FluentValidation;

namespace Chattoo.Application.GroupRoles.Commands.Update
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateGroupRoleCommand"/>.
    /// </summary>
    public class UpdateGroupRoleCommandValidator : AbstractValidator<UpdateGroupRoleCommand>
    {
        public UpdateGroupRoleCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutno určit Id uživatelské role v rámci skupiny.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název uživatelské role nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název uživatelské role je nutné vyplnit.");
        }
    }
}