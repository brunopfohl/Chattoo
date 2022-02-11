using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="AddGroupRoleCommand"/>.
    /// </summary>
    public class AddGroupRoleCommandValidator : AbstractValidator<AddGroupRoleCommand>
    {
        public AddGroupRoleCommandValidator(GroupValidationService groupValidation)
        {
            RuleFor(v => v.GroupId)
                .NotEmpty()
                    .WithMessage("Je nutno určit Id skupiny.")
                .Must(groupValidation.Found)
                    .WithMessage("Skupina nebyla nalezena")
                .Must(groupValidation.ReadPermissionGranted)
                    .WithMessage("Uživatel nemá právo pro přidávání rolí do tohoto kanálu.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100)
                    .WithMessage("Název uživatelské role nesmí být delší než 100 znaků.")
                .NotEmpty()
                    .WithMessage("Název uživatelské role je nutné vyplnit.")
                .Must(groupValidation.RoleCanBeAdded)
                    .WithMessage("Role se stejným názvem již existuje.");
        }
    }
}