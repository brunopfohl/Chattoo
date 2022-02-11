using System.Collections.Generic;
using System.Linq;
using Chattoo.Domain.Common;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita se záznamy o uživatelovi (není využíváno Identity službou).
    /// </summary>
    public class User : AuditableEntity, IAuditableEntity, IAggregateRoot
    {
        private List<UserAlias> _aliases;
        private List<GroupRole> _groupRoles;
        private List<CommunicationChannelRole> _channelRoles;
        private List<UserToGroup> _groups;
        private List<UserToCommunicationChannel> _channels;
        private List<UserToCalendarEvent> _joinedEvents;

        protected User()
        {
            _aliases = new List<UserAlias>();
            _groupRoles = new List<GroupRole>();
            _channelRoles = new List<CommunicationChannelRole>();
            _groups = new List<UserToGroup>();
            _channels = new List<UserToCommunicationChannel>();
            _joinedEvents = new List<UserToCalendarEvent>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje uživatelské jméno.
        /// </summary>
        public virtual string UserName { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci aliasů (přezdívek), které má tento uživatel napříč aplikací.
        /// </summary>
        public virtual IReadOnlyCollection<UserAlias> Aliases => _aliases.AsReadOnly();

        /// <summary>
        /// Vrací nebo nastavuje kolekci rolí, kterými uživatel disponuje napříč všemi skupinami.
        /// </summary>
        public virtual IReadOnlyCollection<GroupRole> GroupRoles => _groupRoles.AsReadOnly();

        /// <summary>
        /// Vrací nebo nastavuje kolekci rolí, kterými uživatel disponuje napříč všemi komunikačními kanály.
        /// </summary>
        public virtual IReadOnlyCollection<CommunicationChannelRole> ChannelRoles => _channelRoles.AsReadOnly();

        public virtual IReadOnlyCollection<UserToGroup> Groups => _groups.AsReadOnly();

        public virtual IReadOnlyCollection<UserToCommunicationChannel> Channels => _channels.AsReadOnly();

        public virtual IReadOnlyCollection<UserToCalendarEvent> JoinedEvents => _joinedEvents.AsReadOnly();

        public UserAlias AddAlias(string aliasText)
        {
            var alias = UserAlias.Create(Id, aliasText);
            
            _aliases.Add(alias);
            
            return alias;
        }

        public UserAlias DeleteAlias(string id)
        {
            var alias = GetAlias(id);

            _aliases.Remove(alias);

            return alias;
        }

        public UserAlias UpdateAlias(string id, string aliasText)
        {
            var alias = GetAlias(id);

            alias.SetAlias(aliasText);

            return alias;
        }
        
        public static User Create(string id, string userName)
        {
            var entity = new User
            {
                Id = id,
                UserName = userName
            };

            return entity;
        }

        private UserAlias GetAlias(string id)
        {
            var alias = Aliases.FirstOrDefault(a => a.Id == id);

            if (alias == null)
            {
                throw new NotFoundException($"{nameof(User)}:{nameof(UserAlias)}", id);
            }

            return alias;
        }
    }
}