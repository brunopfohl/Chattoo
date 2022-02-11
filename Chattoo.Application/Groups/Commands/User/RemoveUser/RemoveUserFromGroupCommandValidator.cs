using FluentValidation;

namespace Chattoo.Application.Groups.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="RemoveUserFromGroupCommand"/>.
    /// </summary>
    public class RemoveUserFromGroupCommandValidator : AbstractValidator<RemoveUserFromGroupCommand>
    {
        public RemoveUserFromGroupCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("Je nutno určit Id uživatele.");
            
            RuleFor(v => v.GroupId)
                .NotEmpty().WithMessage("Je nutno určit Id skupiny.");
        }
    }
}