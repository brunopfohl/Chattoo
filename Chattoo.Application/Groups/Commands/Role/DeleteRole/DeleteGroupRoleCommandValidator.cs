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
                    .WithMessage("Je nutno určit Id skupiny.");

            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutné zadat Id uživatelské role v rámci skupiny.");
        }
    }
}