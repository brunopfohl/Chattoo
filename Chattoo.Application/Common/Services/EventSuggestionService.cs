using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;

namespace Chattoo.Application.Common.Services
{
    public class EventSuggestionService
    {
        private readonly ICalendarEventWishRepository _wishRepository;
        private readonly CalendarEventWishManager _wishManager;
        
        public EventSuggestionService(ICalendarEventWishRepository wishRepository, CalendarEventWishManager wishManager)
        {
            _wishRepository = wishRepository;
            _wishManager = wishManager;
        }
        
        public async Task OnNewWishAdded(CalendarEventWishDto newWishDto)
        {
            // Načtu přání.
            var newWish = await _wishManager.GetWishOrThrow(newWishDto.Id);
            
            // Načtu přání ostatních uživatelů, která by společně s novým přáním mohla vyústit ve vytvoření události.
            var addepts = _wishRepository.GetAddeptsFor(newWish).ToList();
            
            // Odeberu z přání ta, která mají minimální počet uživatelů moc vysoký.
            addepts = addepts
                .Where(a => a.MinimalParticipantsCount <= addepts.Count())
                .ToList();

            foreach (var addept in addepts)
            {
                // Počet prvků které vybírám pro možné kombinace je roven minimálnímu počtu uživatelů.
                int k = newWish.MinimalParticipantsCount > addept.MinimalParticipantsCount
                    ? newWish.MinimalParticipantsCount
                    : addept.MinimalParticipantsCount;

                var wishesForCombinations = addepts.Except(new[] { addept });
                
                // foreach (var combination in Combinations(wishesForCombinations, k - 2))
                // {
                //     if (TryFindDateInterval(combination.Append(newWish).Append(wish).ToList(), out DateInterval foundInterval))
                //     {
                //         Console.WriteLine($"{foundInterval.StartsAt.ToLongDateString()} {foundInterval.StartsAt.ToLongTimeString()} - {foundInterval.EndsAt.ToLongTimeString()} : delka {foundInterval.Length.ToString()}");
                //
                //         foreach (var w in combination.Append(wish))
                //         {
                //             _wishes.Remove(w);
                //         }
                //
                //         return;
                //     }
                // }
            }
        }
        
        // public bool TryFindDateInterval(List<CalendarEventWish> wishes, out DateInterval result)
        // {
        //     // Seřadím přání podle minimální délky události, od nejdelší po nejkratší.
        //     // Zvýším tak šanci, že nebude nalezen žádný průnik časů už v dřívejší iteraci,
        //     // tedy průměrný počet iterací.
        //     var sortedWishes = wishes.OrderByDescending(w => w.MinimalLength).ToList();
        //
        //     // Minimální délka hledaného intervalu je určena prvním přáním z kolekce (to si nárokuje největší min. délku).
        //     TimeSpan minimalLength = sortedWishes[0].Mini;
        //
        //     // List společných průniků. Zpočátku je jedná o časové intervaly z prvního přání.
        //     List<DateInterval> intervals = sortedWishes[0].DateIntervals;
        //
        //     foreach (var wish in sortedWishes.Skip(1))
        //     {
        //         intervals = _dateIntervalService.GetOverlaps(intervals, wish.DateIntervals, minimalLength);
        //
        //         if (intervals.Count == 0)
        //         {
        //             result = null;
        //             return false;
        //         }
        //     }
        //
        //     // Vracím nejdelší interval.
        //     result = intervals.OrderByDescending(i => i.Length).First();
        //     return true;
        // }
        
        private IEnumerable<IEnumerable<T>> Combinations<T>(IEnumerable<T> elements, int k)
        {
            var elem = elements.ToArray();
            var size = elem.Length;

            if (k > size) yield break;

            var numbers = new int[k];

            for (var i = 0; i < k; i++)
                numbers[i] = i;

            do
            {
                yield return numbers.Select(n => elem[n]);
            } while (NextCombination(numbers, size, k));
        }
        
        private bool NextCombination(IList<int> num, int n, int k)
        {
            bool finished;

            var changed = finished = false;

            if (k <= 0) return false;

            for (var i = k - 1; !finished && !changed; i--)
            {
                if (num[i] < n - 1 - (k - 1) + i)
                {
                    num[i]++;

                    if (i < k - 1)
                        for (var j = i + 1; j < k; j++)
                            num[j] = num[j - 1] + 1;
                    changed = true;
                }
                finished = i == 0;
            }

            return changed;
        }
    }
}