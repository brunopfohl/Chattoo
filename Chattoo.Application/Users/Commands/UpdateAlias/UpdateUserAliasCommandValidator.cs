using FluentValidation;

namespace Chattoo.Application.Users.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateUserAliasCommand"/>.
    /// </summary>
    public class UpdateUserAliasCommandValidator : AbstractValidator<UpdateUserAliasCommand>
    {
        public UpdateUserAliasCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutno určit Id uživatelské přezdívky.");
            
            RuleFor(v => v.Alias)
                .MaximumLength(100).WithMessage("Přezdívka uživatele nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Přezdívku uživatele je nutné vyplnit.");
        }
    }
}