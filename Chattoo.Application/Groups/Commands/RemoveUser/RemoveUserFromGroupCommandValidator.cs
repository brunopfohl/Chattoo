using FluentValidation;

namespace Chattoo.Application.Groups.Commands.RemoveUser
{
    /// <summary>
    /// Validátor příkazu <see cref="RemoveUserFromGroupCommand"/>.
    /// </summary>
    public class AddUserToGroupCommandValidator : AbstractValidator<RemoveUserFromGroupCommand>
    {
        public AddUserToGroupCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("Je nutno určit Id uživatele.");
            
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("Je nutno určit Id skupiny.");
        }
    }
}