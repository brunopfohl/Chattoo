using FluentValidation;

namespace Chattoo.Application.UserAliases.Commands.Delete
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