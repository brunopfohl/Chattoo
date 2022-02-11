using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Queries
{
    public class GetCalendarEventsForCommunicationChannelQueryValidator : AbstractValidator<GetCalendarEventsForCommunicationChannelQuery>
    {
        public GetCalendarEventsForCommunicationChannelQueryValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id komunikačního kanálu.");
        }
    }
}