using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Queries
{
    public class GetCommunicationChannelRoleByIdQueryValidator : AbstractValidator<GetCommunicationChannelRoleByIdQuery>
    {
        public GetCommunicationChannelRoleByIdQueryValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id komunikačního kanálu.");

            RuleFor(v => v.RoleId)
                .NotEmpty()
                    .WithMessage("Id role nebylo vyplněno.");
        }
    }
}