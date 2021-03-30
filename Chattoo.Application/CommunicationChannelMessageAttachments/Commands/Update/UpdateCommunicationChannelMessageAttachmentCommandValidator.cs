using FluentValidation;

namespace Chattoo.Application.CommunicationChannelMessageAttachments.Commands.Update
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateCommunicationChannelMessageAttachmentCommand"/>.
    /// </summary>
    public class UpdateCommunicationChannelMessageAttachmentCommandValidator : AbstractValidator<UpdateCommunicationChannelMessageAttachmentCommand>
    {
        public UpdateCommunicationChannelMessageAttachmentCommandValidator()
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Je nutné vyplnit Id přílohy.");
            
            RuleFor(v => v.Name)
                .MaximumLength(200).WithMessage("Název přílohy nesmí být delší než 200 znaků.")
                .NotEmpty().WithMessage("Je nutné vyplnit název přílohy.");
        }
    }
}