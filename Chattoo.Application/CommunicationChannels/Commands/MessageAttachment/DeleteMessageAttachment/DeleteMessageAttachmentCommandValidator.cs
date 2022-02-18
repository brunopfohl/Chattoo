using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteMessageAttachmentCommand"/>.
    /// </summary>
    public class DeleteMessageAttachmentCommandValidator : AbstractValidator<DeleteMessageAttachmentCommand>
    {
        public DeleteMessageAttachmentCommandValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.MessageId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id zprávy.");

            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id přílohy.");
        }
    }
}