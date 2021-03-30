using FluentValidation;

namespace Chattoo.Application.CommunicationChannelMessageAttachments.Commands.Delete
{
    /// <summary>
    /// Validátor příkazu <see cref="DeleteCommunicationChannelMessageAttachmentCommand"/>.
    /// </summary>
    public class DeleteCommunicationChannelMessageAttachmentCommandValidator : AbstractValidator<DeleteCommunicationChannelMessageAttachmentCommand>
    {
        public DeleteCommunicationChannelMessageAttachmentCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné vyplnit Id přílohy.");
        }
    }
}