using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateChannelMessageCommand"/>.
    /// </summary>
    public class UpdateChannelMessageCommandValidator : AbstractValidator<UpdateChannelMessageCommand>
    {
        public UpdateChannelMessageCommandValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.Content)
                .MaximumLength(1000)
                    .WithMessage("Obsah zprávy v komunikačním kanálu nesmí být delší než 1000 znaků.")
                .NotEmpty()
                    .WithMessage("Obsah zprávy v komunikačním kanálu je nutné vyplnit.");

            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id zprávy z komunikačního kanálu.");
        }
    }
}