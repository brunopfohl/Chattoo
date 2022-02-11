using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateCommunicationChannelCommand"/>.
    /// </summary>
    public class UpdateCommunicationChannelCommandValidator : AbstractValidator<UpdateCommunicationChannelCommand>
    {
        public UpdateCommunicationChannelCommandValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Je nutno určit Id komunikačního kanálu.");
            
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název komunikačního kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název komunikačního kanálu je nutné vyplnit.");
            
            RuleFor(v => v.Description)
                .MaximumLength(255).WithMessage("Popis komunikačního kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Popis komunikačního kanálu je nutné vyplnit.");
        }
    }
}