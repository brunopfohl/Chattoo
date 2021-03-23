using FluentValidation;

namespace Chattoo.Application.GroupRoles.Commands.Delete
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteGroupRoleCommand"/>.
    /// </summary>
    public class DeleteGroupRoleCommandValidator : AbstractValidator<DeleteGroupRoleCommand>
    {
        public DeleteGroupRoleCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné zadat Id uživatelské role v rámci skupiny.");
        }
    }
}