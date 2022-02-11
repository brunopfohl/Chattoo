using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Queries
{
    public class GetCommunicationChannelMessagesForChannelQueryValidator
        : AbstractValidator<GetCommunicationChannelMessagesForChannelQuery>
    {
        public GetCommunicationChannelMessagesForChannelQueryValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id komunikačního kanálu.");
        }
    }
}