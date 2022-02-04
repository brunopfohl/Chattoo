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
        protected User()
        {
            Aliases = new List<UserAlias>();
            GroupRoles = new List<GroupRole>();
            ChannelRoles = new List<CommunicationChannelRole>();
            JoinedEvents = new List<UserToCalendarEvent>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje uživatelské jméno.
        /// </summary>
        public virtual string UserName { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci aliasů (přezdívek), které má tento uživatel napříč aplikací.
        /// </summary>
        public virtual ICollection<UserAlias> Aliases { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci rolí, kterými uživatel disponuje napříč všemi skupinami.
        /// </summary>
        public virtual ICollection<GroupRole> GroupRoles { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci rolí, kterými uživatel disponuje napříč všemi komunikačními kanály.
        /// </summary>
        public virtual ICollection<CommunicationChannelRole> ChannelRoles { get; private set; }
        
        public virtual ICollection<UserToGroup> Groups { get; private set; }
        
        public virtual ICollection<UserToCommunicationChannel> Channels { get; private set; }
        
        public virtual ICollection<UserToCalendarEvent> JoinedEvents { get; private set; }

        public UserAlias AddAlias(string aliasText)
        {
            var alias = UserAlias.Create(Id, aliasText);
            
            Aliases.Add(alias);
            
            return alias;
        }

        public UserAlias DeleteAlias(string id)
        {
            var alias = GetAlias(id);

            Aliases.Remove(alias);

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