using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Queries.GetRoles
{
    public class GetCommunicationChannelRolesQueryValidator : AbstractValidator<GetCommunicationChannelRolesQuery>
    {
        public GetCommunicationChannelRolesQueryValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id komunikačního kanálu.");
        }
    }
}