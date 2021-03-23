using FluentValidation;

namespace Chattoo.Application.Groups.Commands.Update
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateGroupCommand"/>.
    /// </summary>
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutno určit Id skupiny.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název skupiny nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název skupiny je nutné vyplnit.");
        }
    }
}