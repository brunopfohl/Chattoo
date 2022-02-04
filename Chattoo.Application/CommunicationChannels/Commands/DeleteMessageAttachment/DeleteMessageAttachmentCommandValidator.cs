using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands.DeleteMessageAttachment
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteMessageAttachmentCommand"/>.
    /// </summary>
    public class DeleteCommunicationChannelMessageAttachmentCommandValidator : AbstractValidator<DeleteMessageAttachmentCommand>
    {
        public DeleteCommunicationChannelMessageAttachmentCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné vyplnit Id přílohy.");
        }
    }
}