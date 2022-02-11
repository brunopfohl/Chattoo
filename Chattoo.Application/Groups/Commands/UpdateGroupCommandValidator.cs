using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateGroupCommand"/>.
    /// </summary>
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator(GroupValidationService groupValidation)
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutno určit Id skupiny.")
                .Must(groupValidation.Found)
                    .WithMessage("Skupina nebyla nalezena")
                .Must(groupValidation.ReadPermissionGranted)
                    .WithMessage("Uživatel nemá právo pro upravování této skupiny.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název skupiny nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název skupiny je nutné vyplnit.");
        }
    }
}