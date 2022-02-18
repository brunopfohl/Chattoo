using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="AddGroupRoleCommand"/>.
    /// </summary>
    public class AddGroupRoleCommandValidator : AbstractValidator<AddGroupRoleCommand>
    {
        public AddGroupRoleCommandValidator()
        {
            RuleFor(v => v.GroupId)
                .NotEmpty()
                    .WithMessage("Je nutno určit Id skupiny.");

            RuleFor(v => v.Name)
                .MaximumLength(100)
                    .WithMessage("Název uživatelské role nesmí být delší než 100 znaků.")
                .NotEmpty()
                    .WithMessage("Název uživatelské role je nutné vyplnit.");
        }
    }
}