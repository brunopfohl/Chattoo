using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chattoo.Application.CalendarEvents.DTOs;
using Chattoo.Application.CalendarEventWishes.DTOs;
using Chattoo.Application.Common.Models;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Extensions;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.Services;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Application.Common.Services
{
    public class EventSuggestionService
    {
        private readonly ICalendarEventWishRepository _wishRepository;
        private readonly CalendarEventWishManager _wishManager;
        private readonly DateIntervalService _dateIntervalService;
        private readonly CalendarEventManager _calendarEventManager;
        private readonly ICalendarEventRepository _calendarEventRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public EventSuggestionService(ICalendarEventWishRepository wishRepository, CalendarEventWishManager wishManager, DateIntervalService dateIntervalService, CalendarEventManager calendarEventManager, IUnitOfWork unitOfWork, ICalendarEventRepository calendarEventRepository, IMapper mapper)
        {
            _wishRepository = wishRepository;
            _wishManager = wishManager;
            _dateIntervalService = dateIntervalService;
            _calendarEventManager = calendarEventManager;
            _unitOfWork = unitOfWork;
            _calendarEventRepository = calendarEventRepository;
            _mapper = mapper;
        }
        
        public async Task<CalendarEventSuggestionResult> OnNewWishAdded(CalendarEventWishDto newWishDto)
        {
            // Načtu přání.
            var newWish = await _wishManager.GetWishOrThrow(newWishDto.Id);

            CalendarEvent joinedEvent = null;
            
            // Pokud již existující odpovídající událost.
            if (TryFindCorrespondingEvent(newWish, out joinedEvent))
            {
                joinedEvent.AddParticipant(newWish.AuthorId);
                
                _wishManager.AssignWishToCalendarEvent(newWish, joinedEvent);
                await _wishRepository.AddOrUpdateAsync(newWish);
            }
            // Pokud najdu kombinaci přání pro které lze vytvořit.
            else if (TryFindPossibleCombinationFor(newWish, out var wishes, out var intersectionInterval))
            {
                joinedEvent = await _calendarEventManager.CreateEvent
                (
                    newWish.CommunicationChannelId,
                    newWish.Type,
                    newWish.Name,
                    string.Empty,
                    null,
                    intersectionInterval.StartsAt,
                    intersectionInterval.EndsAt
                );
                
                foreach (var wish in wishes)
                {
                    if (wish.AuthorId != joinedEvent.AuthorId)
                    {
                        joinedEvent.AddParticipant(wish.AuthorId);
                    }
                    
                    _wishManager.AssignWishToCalendarEvent(wish, joinedEvent);
                    await _wishRepository.AddOrUpdateAsync(wish);
                }
            }

            if (joinedEvent is null)
                return null;

            bool eventWasCreated = joinedEvent.Id.IsNotNullOrEmpty();
            
            await _calendarEventRepository.AddOrUpdateAsync(joinedEvent);
            await _unitOfWork.SaveChangesAsync();

            var eventDto = _mapper.Map<CalendarEventDto>(joinedEvent);

            var result = new CalendarEventSuggestionResult(eventDto, eventWasCreated);

            return result;
        }

        private bool TryFindCorrespondingEvent(CalendarEventWish wish, out CalendarEvent correspondingEvent)
        {
            correspondingEvent = null;
            
            var addepts = _calendarEventRepository.GetAddeptsForWish(wish).ToList();

            foreach (var addept in addepts)
            {
                var addeptEndsAt = addept.EndsAt ?? addept.StartsAt.Date.AddDays(1);
                DateInterval addeptDateInterval = DateInterval.Create(addept.StartsAt, addeptEndsAt);

                if(addept.Length.HasValue && addept.Length < wish.MinimalLength)
                    continue;
                
                foreach (var interval in wish.DateIntervals)
                {
                    if(interval.GetOverlap(addeptDateInterval).Length >= wish.MinimalLength)
                    {
                        correspondingEvent = addept;
                        return true;
                    }
                }
            }

            return false;
        }

        private bool TryFindPossibleCombinationFor(CalendarEventWish wish, out List<CalendarEventWish> wishes,
            out DateInterval intersectionInterval)
        {
            wishes = null;
            intersectionInterval = null;
            
            // Načtu přání ostatních uživatelů, která by společně s novým přáním mohla vyústit ve vytvoření události.
            var addepts = _wishRepository.GetAddeptsFor(wish).ToList();

            int addeptsCount = addepts.Count + 1;
            
            // Odeberu z přání ta, která mají minimální počet uživatelů moc vysoký.
            addepts = addepts
                .Where(a => a.MinimalParticipantsCount <= addeptsCount)
                .ToList();

            foreach (var addept in addepts)
            {
                // Počet prvků které vybírám pro možné kombinace je roven minimálnímu počtu uživatelů.
                int k = wish.MinimalParticipantsCount > addept.MinimalParticipantsCount
                    ? wish.MinimalParticipantsCount
                    : addept.MinimalParticipantsCount;

                var wishesForCombinations = addepts.Except(new[] { addept });
                
                foreach (var combination in Combinations(wishesForCombinations, k - 2))
                {
                    var wishesToCheck = combination
                        .Append(wish)
                        .Append(addept)
                        .ToList();
                    
                    if (TryFindDateInterval(wishesToCheck, out intersectionInterval))
                    {
                        wishes = wishesToCheck;
                        return true;
                    }
                }
            }

            return false;
        }
        
        private bool TryFindDateInterval(List<CalendarEventWish> wishes, out DateInterval result)
        {
            result = null;
            
            // Seřadím přání podle minimální délky události, od nejdelší po nejkratší.
            // Zvýším tak šanci, že nebude nalezen žádný průnik časů už v dřívejší iteraci,
            // tedy průměrný počet iterací.
            var sortedWishes = wishes.OrderByDescending(w => w.MinimalLength).ToList();

            var genesis = sortedWishes[0];
            
            // Minimální délka hledaného intervalu je určena prvním přáním z kolekce (to si nárokuje největší min. délku).
            TimeSpan minimalLength = genesis.MinimalLength;
        
            // List společných průniků. Zpočátku je jedná o časové intervaly z prvního přání.
            List<DateInterval> intervals = genesis.DateIntervals.ToList();

            var authorIds = new HashSet<string> { genesis.AuthorId };

            foreach (var wish in sortedWishes.Skip(1))
            {
                // Každé přání musí mít jiného autora.
                if (!authorIds.Add(wish.AuthorId))
                    return false;
                
                intervals = _dateIntervalService.GetOverlaps(intervals, wish.DateIntervals, minimalLength);
        
                if (intervals.Count == 0)
                    return false;
            }
        
            // Vracím nejdelší interval.
            result = intervals.OrderByDescending(i => i.Length).First();
            return true;
        }
        
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