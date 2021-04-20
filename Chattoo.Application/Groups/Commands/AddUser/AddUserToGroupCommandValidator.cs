﻿using FluentValidation;

namespace Chattoo.Application.Groups.Commands.AddUser
{
    /// <summary>
    /// Validátor příkazu <see cref="AddUserToGroupCommand"/>.
    /// </summary>
    public class AddUserToGroupCommandValidator : AbstractValidator<AddUserToGroupCommand>
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