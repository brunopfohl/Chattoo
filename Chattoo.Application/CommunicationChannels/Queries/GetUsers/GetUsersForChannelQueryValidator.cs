using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Queries.GetUsers
{
    public class GetUsersForChannelQueryValidator : AbstractValidator<GetUsersForChannelQuery>
    {
        public GetUsersForChannelQueryValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id komunikačního kanálu.");
        }
    }
}