using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Exceptions;
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

        public CalendarEventWishManager(ICurrentUserService currentUserService,
            ICalendarEventWishRepository wishRepository, ChannelManager channelManager)
        {
            _currentUserService = currentUserService;
            _wishRepository = wishRepository;
            _channelManager = channelManager;
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

        public async Task<CalendarEventWish> Create(string channelId, string name, ICollection<IDateInterval> dateIntervals,
            CalendarEventType type, int minimalParticipantsCount, TimeSpan minimalLength)
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
                name,
                channel,
                dateIntervalEntities,
                type,
                minimalParticipantsCount,
                minimalLength
            );

            return wish;
        }

        public void AssignWishToCalendarEvent(CalendarEventWish wish, CalendarEvent calendarEvent)
        {
            if 
            (
                wish.Type != calendarEvent.CalendarEventType ||
                wish.CommunicationChannelId != calendarEvent.CommunicationChannelId
            )
            {
                throw new ArgumentException();
            }

            wish.AssignToCalendarEvent(calendarEvent.Id);
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
        
        public async Task<CalendarEventWish> UpdateWish(CalendarEventWish wish, string name, CalendarEventType type,
            ICollection<IDateInterval> dateIntervals, int minimalParticipantsCount, TimeSpan minimalLength)
        {
            if (!_currentUserService.CanEditWish(wish))
            {
                throw new ForbiddenAccessException();
            }
            
            wish.SetMinimalParticipantsCount(minimalParticipantsCount);
            
            wish.SetName(name);
            wish.SetType(type);
            wish.SetMinimalLength(minimalLength);
            wish.UpdateDateIntervals(dateIntervals.Select(DateInterval.Create).ToList());

            return wish;
        }
    }
}