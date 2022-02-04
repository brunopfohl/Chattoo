using FluentValidation;

namespace Chattoo.Application.Users.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="AddUserAliasCommand"/>.
    /// </summary>
    public class AddUserAliasCommandValidator : AbstractValidator<AddUserAliasCommand>
    {
        public AddUserAliasCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("Id uživatele je nutné vyplnit.");
            
            RuleFor(v => v.Alias)
                .MaximumLength(100).WithMessage("Název skupiny nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název skupiny je nutné vyplnit.");
        }
    }
}