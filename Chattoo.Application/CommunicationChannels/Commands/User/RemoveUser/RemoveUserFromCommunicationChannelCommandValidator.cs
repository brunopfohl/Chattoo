using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    public class RemoveUserFromCommunicationChannelCommandValidator : AbstractValidator<RemoveUserFromCommunicationChannelCommand>
    {
        public RemoveUserFromCommunicationChannelCommandValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("Id uživatele musí být zadáno");
        }
    }
}