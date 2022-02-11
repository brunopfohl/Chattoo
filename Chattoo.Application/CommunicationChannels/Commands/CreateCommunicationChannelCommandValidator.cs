using Chattoo.Domain.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="CreateCommunicationChannelCommand"/>.
    /// </summary>
    public class CreateCommunicationChannelCommandValidator : AbstractValidator<CreateCommunicationChannelCommand>
    {
        public CreateCommunicationChannelCommandValidator(ChannelManager channelManager)
        {
            RuleFor(v => v.Name)
                .MaximumLength(100).WithMessage("Název komunikačního kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Název komunikačního kanálu je nutné vyplnit.");
                
            
            RuleFor(v => v.Description)
                .MaximumLength(255).WithMessage("Popis komunikačního kanálu nesmí být delší než 100 znaků.")
                .NotEmpty().WithMessage("Popis komunikačního kanálu je nutné vyplnit.");
        }
    }
}