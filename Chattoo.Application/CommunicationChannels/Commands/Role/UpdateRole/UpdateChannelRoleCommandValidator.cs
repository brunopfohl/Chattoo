using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateChannelRoleCommand"/>.
    /// </summary>
    public class UpdateChannelRoleCommandValidator : AbstractValidator<UpdateChannelRoleCommand>
    {
        public UpdateChannelRoleCommandValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                .WithMessage("Je nutné vyplnit Id komunikačního kanálu.");

            RuleFor(v => v.Id)
                .NotEmpty()
                .WithMessage("Je nutné vyplnit Id uživatelské role z komunikačního kanálu.");
            
            
            RuleFor(v => v.Name)
                .MaximumLength(100)
                    .WithMessage("Název uživatelské role v komunikačním kanálu nesmí být delší než 100 znaků.")
                .NotEmpty()
                    .WithMessage("Název uživatelské role v komunikačním kanálu je nutné vyplnit.");
        }
    }
}