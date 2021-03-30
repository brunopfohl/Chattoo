using FluentValidation;

namespace Chattoo.Application.Users.Commands.Delete
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteUserCommand"/>.
    /// </summary>
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutno určit Id uživatele.");
        }
    }
}