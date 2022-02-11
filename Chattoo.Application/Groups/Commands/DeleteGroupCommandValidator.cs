using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteGroupCommand"/>.
    /// </summary>
    public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
    {
        public DeleteGroupCommandValidator(GroupValidationService groupValidation)
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutno určit Id skupiny.")
                .Must(groupValidation.Found)
                    .WithMessage("Skupina nebyla nalezena")
                .Must(groupValidation.ReadPermissionGranted)
                    .WithMessage("Uživatel nemá právo pro smazání skupiny.");
        }
    }
}