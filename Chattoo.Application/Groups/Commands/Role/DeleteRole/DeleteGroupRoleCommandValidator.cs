using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteGroupRoleCommand"/>.
    /// </summary>
    public class DeleteGroupRoleCommandValidator : AbstractValidator<DeleteGroupRoleCommand>
    {
        public DeleteGroupRoleCommandValidator(GroupValidationService groupValidation)
        {
            RuleFor(v => v.GroupId)
                .NotEmpty()
                    .WithMessage("Je nutno určit Id skupiny.")
                .Must(groupValidation.Found)
                    .WithMessage("Skupina nebyla nalezena")
                .Must(groupValidation.ReadPermissionGranted)
                    .WithMessage("Uživatel nemá právo pro smazání role z této skupiny.");
            
            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutné zadat Id uživatelské role v rámci skupiny.")
                .Must(groupValidation.RoleFound)
                    .WithMessage("Role nebyla nalezena.");
        }
    }
}