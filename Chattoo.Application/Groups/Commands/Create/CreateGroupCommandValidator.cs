using FluentValidation;

namespace Chattoo.Application.Groups.Commands.Create
{
    /// <summary>
    /// Validátor příkazu <see cref="CreateGroupCommand"/>.
    /// </summary>
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název skupiny nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název skupiny je nutné vyplnit.");
        }
    }
}