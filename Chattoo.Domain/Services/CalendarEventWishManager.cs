using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
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
        private readonly ChannelManager _channelManager;
        private readonly GroupManager _groupManager;

        public CalendarEventWishManager(ICurrentUserService currentUserService,
            ICalendarEventWishRepository wishRepository, ChannelManager channelManager, GroupManager groupManager)
        {
            _currentUserService = currentUserService;
            _wishRepository = wishRepository;
            _channelManager = channelManager;
            _groupManager = groupManager;
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

        public async Task<CalendarEventWish> Create(string channelId, ICollection<IDateInterval> dateIntervals,
            CalendarEventType type, int? minimalParticipantsCount)
        {
            var dateIntervalEntities = dateIntervals
                .Select(DateInterval.Create)
                .ToList();

            CalendarEventWish wish;
            
            var channel = await _channelManager.GetChannelOrThrow(channelId);

            if (!_currentUserService.CanViewChannel(channel))
            {
                throw new ForbiddenAccessException();
            }
            
            wish = CalendarEventWish.Create(
                _currentUserService.User,
                channel,
                dateIntervalEntities,
                type,
                minimalParticipantsCount
            );

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
        
        public async Task<CalendarEventWish> UpdateWish(CalendarEventWish wish, CalendarEventType type,
            ICollection<IDateInterval> dateIntervals, int? minimalParticipantsCount)
        {
            if (!_currentUserService.CanEditWish(wish))
            {
                throw new ForbiddenAccessException();
            }
            
            wish.SetMinimalParticipantsCount(minimalParticipantsCount);
            
            wish.SetType(type);
            wish.UpdateDateIntervals(dateIntervals.Select(DateInterval.Create).ToList());

            return wish;
        }
    }
}