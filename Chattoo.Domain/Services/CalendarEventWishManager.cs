using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Extensions;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using Chattoo.Domain.ValueObjects;

namespace Chattoo.Domain.Services
{
    public class CalendarEventWishManager
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ICalendarEventWishRepository _wishRepository;
        private readonly ICalendarEventTypeRepository _calendarEventTypeRepository;
        private readonly ChannelManager _channelManager;
        private readonly GroupManager _groupManager;

        public CalendarEventWishManager(ICurrentUserService currentUserService,
            ICalendarEventWishRepository wishRepository, ChannelManager channelManager, GroupManager groupManager,
            ICalendarEventTypeRepository calendarEventTypeRepository)
        {
            _currentUserService = currentUserService;
            _wishRepository = wishRepository;
            _channelManager = channelManager;
            _groupManager = groupManager;
            _calendarEventTypeRepository = calendarEventTypeRepository;
        }

        public async Task<CalendarEventWish> GetWishOrThrow(string wishId)
        {
            var wish = await _wishRepository.GetByIdAsync(wishId)
                ?? throw new CalendarEventWishNotFoundException(wishId);

            if (!_currentUserService.CanViewWish(wish))
            {
                throw new ForbiddenAccessException();
            }

            return wish;
        }

        public async Task<CalendarEventWish> Create(string channelId, string groupId, ICollection<IDateInterval> dateIntervals,
            ICollection<string> typeIds, int? minimalParticipantsCount, int? maximalParticipantsCount)
        {
            var types = _calendarEventTypeRepository.GetByIds(typeIds.ToArray()).ToList();

            var dateIntervalEntities = dateIntervals
                .Select(DateInterval.Create)
                .ToList();

            CalendarEventWish wish;
            
            if (channelId.IsNotNullOrEmpty())
            {
                var channel = await _channelManager.GetChannelOrThrow(channelId);

                if (!_currentUserService.CanViewChannel(channel))
                {
                    throw new ForbiddenAccessException();
                }
                
                wish = CalendarEventWish.Create(
                    _currentUserService.User,
                    channel,
                    dateIntervalEntities,
                    types,
                    minimalParticipantsCount,
                    maximalParticipantsCount
                );
            }
            else
            {
                var group = await _groupManager.GetGroupOrThrow(groupId);
                
                if (!_currentUserService.CanViewGroup(group))
                {
                    throw new ForbiddenAccessException();
                }
                
                wish = CalendarEventWish.Create(
                    _currentUserService.User,
                    group,
                    dateIntervalEntities,
                    types,
                    minimalParticipantsCount,
                    maximalParticipantsCount
                );
            }

            return wish;
        }
        
        public async Task DeleteWish(string eventId)
        {
            var wish = await GetWishOrThrow(eventId);

            if (!_currentUserService.CanEditWish(wish))
            {
                throw new ForbiddenAccessException();
            }
            
            _wishRepository.Remove(wish);
        }
        
        public async Task<CalendarEventWish> UpdateWish(CalendarEventWish wish, ICollection<string> typeIds,
            ICollection<IDateInterval> dateIntervals, int? minimalParticipantsCount, int? maximalParticipantsCount)
        {
            if (!_currentUserService.CanEditWish(wish))
            {
                throw new ForbiddenAccessException();
            }
            
            wish.SetMinimalParticipantsCount(minimalParticipantsCount);
            wish.SetMaximalParticipantsCount(maximalParticipantsCount);
            
            var types = _calendarEventTypeRepository
                .GetByIds(typeIds.ToArray())
                .ToList();
            
            wish.UpdateTypes(types);
            wish.UpdateDateIntervals(dateIntervals.Select(DateInterval.Create).ToList());

            return wish;
        }
    }
}