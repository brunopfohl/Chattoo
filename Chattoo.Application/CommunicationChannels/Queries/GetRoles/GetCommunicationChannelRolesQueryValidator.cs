using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Queries.GetRoles
{
    public class GetCommunicationChannelRolesQueryValidator : AbstractValidator<GetCommunicationChannelRolesQuery>
    {
        public GetCommunicationChannelRolesQueryValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id komunikačního kanálu.");
        }
    }
}