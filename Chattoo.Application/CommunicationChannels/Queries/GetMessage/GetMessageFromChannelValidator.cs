using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Queries
{
    public class GetMessageFromChannelQueryValidator
        : AbstractValidator<GetMessageFromChannelQuery>
    {
        public GetMessageFromChannelQueryValidator()
        {
            RuleFor(v => v.ChannelId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id komunikačního kanálu.");
            
            RuleFor(v => v.MessageId)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id zprávy.");
        }
    }
}