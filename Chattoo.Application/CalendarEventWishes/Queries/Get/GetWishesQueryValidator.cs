using FluentValidation;

namespace Chattoo.Application.CalendarEventWishes.Queries
{
    public class GetWishesQueryValidator : AbstractValidator<GetWishesQuery>
    {
        public GetWishesQueryValidator()
        {
        }
    }
}