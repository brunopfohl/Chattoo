using Chattoo.Application.Common.Services;
using FluentValidation;

namespace Chattoo.Application.CommunicationChannels.Queries.GetById
{
    public class GetCommunicationChannelByIdQueryValidator : AbstractValidator<GetCommunicationChannelByIdQuery>
    {
        public GetCommunicationChannelByIdQueryValidator(ChannelValidationService channelValidation)
        {
            RuleFor(v => v.Id)
                .NotEmpty()
                    .WithMessage("Nebylo určeno Id komunikačního kanálu.");
        }
    }
}