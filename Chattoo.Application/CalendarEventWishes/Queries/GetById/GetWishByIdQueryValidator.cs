using FluentValidation;

namespace Chattoo.Application.CalendarEventWishes.Queries
{
    public class GetWishByIdQueryValidator : AbstractValidator<GetWishByIdQuery>
    {
        public GetWishByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty()
                .WithMessage("Id přání musí být specifikováno.");
        }
    }
}