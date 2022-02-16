using FluentValidation;

namespace Chattoo.Application.CalendarEventWishes.Commands
{
    public class DeleteCalendarEventWishCommandValidator : AbstractValidator<DeleteCalendarEventWishCommand>
    {
        public DeleteCalendarEventWishCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty()
                .WithMessage("Id musí být specifikováno.");
        }
    }
}