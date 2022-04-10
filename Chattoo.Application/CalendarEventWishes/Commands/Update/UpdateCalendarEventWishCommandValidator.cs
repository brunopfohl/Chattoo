using System.Linq;
using Chattoo.Application.Common.Services;
using Chattoo.Domain.ValueObjects;
using FluentValidation;

namespace Chattoo.Application.CalendarEventWishes.Commands
{
    /// <summary>
    /// Validátor příkazu <see cref="UpdateCalendarEventWishCommand"/>.
    /// </summary>
    public class UpdateCalendarEventWishCommandValidator : AbstractValidator<UpdateCalendarEventWishCommand>
    {
        public UpdateCalendarEventWishCommandValidator(DateIntervalService dateIntervalService)
        {
            RuleFor(v => v.Id)
                .NotEmpty().WithMessage("Id přání musí být specifikováno.");
            
            RuleFor(x => x.DateIntervals)
                .Must(x => x.Count > 0).WithMessage("Alespoň 1 časový blok musí být určen.")
                .Must(x =>
                {
                    var intervals = x.Select(DateInterval.Create).ToList();
                    var isOverlapping = dateIntervalService.GetOverlapOfIntervals(intervals);

                    return !isOverlapping;
                }).WithMessage("Časové bloky se překrývají.");

            RuleFor(x => x.MinimalParticipantsCount)
                .GreaterThan(1)
                .WithMessage("Minimální počet účastníků musí být větší než 1.");
        }
    }
}