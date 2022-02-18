using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="AddMessageAttachmentCommand"/>.
    /// </summary>
    public class CreateCommunicationChannelMessageAttachmentCommandValidator : AbstractValidator<AddMessageAttachmentCommand>
    {
        public CreateCommunicationChannelMessageAttachmentCommandValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.MessageId)
                .NotEmpty()
                .WithMessage("Je nutné vyplnit Id zprávy z komunikačního kanálu.");
            
            RuleFor(v => v.Name)
                .MaximumLength(200).WithMessage("Název přílohy nesmí být delší než 200 znaků.")
                .NotEmpty().WithMessage("Je nutné vyplnit název přílohy.");
            
            RuleFor(v => v.Content)
                .NotEmpty().WithMessage("Data přílohy nemohou být prázdná.");
            
            RuleFor(v => v.Type)
                .NotEmpty().WithMessage("Typ přílohy je nutné určit.");
        }
    }
}