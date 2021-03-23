using FluentValidation;

namespace Chattoo.Application.UserAliases.Commands.Create
{
    /// <summary>
    /// Validátor příkazu <see cref="CreateUserAliasCommand"/>.
    /// </summary>
    public class CreateUserAliasCommandValidator : AbstractValidator<CreateUserAliasCommand>
    {
        public CreateUserAliasCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("Id uživatele je nutné vyplnit.");
            
            RuleFor(v => v.Alias)
                .MaximumLength(100).WithMessage("Název skupiny nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název skupiny je nutné vyplnit.");
        }
    }
}