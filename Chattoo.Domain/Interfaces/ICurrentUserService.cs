using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Interfaces
{
    /// <summary>
    /// Rozhraní pro službu poskytující aktuálně přihlášeného uživatele.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Vrací aktuálně přihlášeného uživatele.
        /// </summary>
        public User User { get; }

        public bool CanViewChannel(CommunicationChannel channel);
        
        public bool CanViewChannel(string channelId);


        public bool CanEditChannel(CommunicationChannel channel);

        public bool CanViewGroup(Group group);
        
        public bool CanViewGroup(string groupId);

        public bool CanEditGroup(Group group);

        public bool CanViewEvent(CalendarEvent calendarEvent);

        public bool CanEditEvent(CalendarEvent calendarEvent);

        public bool CanViewWish(CalendarEventWish wish);

        public bool CanEditWish(CalendarEventWish wish);

        public bool CanEditMessage(CommunicationChannelMessage message);
    }
}
