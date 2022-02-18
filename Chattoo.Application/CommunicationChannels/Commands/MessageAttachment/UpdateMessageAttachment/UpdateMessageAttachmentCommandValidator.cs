using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateMessageAttachmentCommand"/>.
    /// </summary>
    public class UpdateMessageAttachmentCommandValidator : AbstractValidator<UpdateMessageAttachmentCommand>
    {
        public UpdateMessageAttachmentCommandValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.MessageId)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id zprávy z komunikačního kanálu.");

            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutné vyplnit Id přílohy.");
            
            RuleFor(v => v.Name)
                .MaximumLength(200).WithMessage("Název přílohy nesmí být delší než 200 znaků.")
                .NotEmpty().WithMessage("Je nutné vyplnit název přílohy.");
        }
    }
}