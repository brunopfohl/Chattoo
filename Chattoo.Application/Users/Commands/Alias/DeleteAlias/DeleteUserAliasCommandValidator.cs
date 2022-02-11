using FluentValidation;

namespace Chattoo.Application.Users.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteUserAliasCommand"/>.
    /// </summary>
    public class DeleteUserAliasCommandValidator : AbstractValidator<DeleteUserAliasCommand>
    {
        public DeleteUserAliasCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutno určit Id uživatelské přezdívky.");
        }
    }
}