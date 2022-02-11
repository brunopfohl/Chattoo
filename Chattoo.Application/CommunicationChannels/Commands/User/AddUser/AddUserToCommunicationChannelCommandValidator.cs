using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands.AddUser
{
    public class AddUserToCommunicationChannelCommandValidator : AbstractValidator<AddUserToCommunicationChannelCommand>
    {
        public AddUserToCommunicationChannelCommandValidator(ChannelValidationService channelValidation,
            UserValidationService userValidation)
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.UserId)
                .NotEmpty()
                    .WithMessage("Id uživatele musí být zadáno")
                .Must(userValidation.Found)
                    .WithMessage("Uživatel neexistuje.");
        }
    }
}