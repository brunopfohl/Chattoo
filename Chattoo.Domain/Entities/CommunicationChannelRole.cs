using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita role uživatele v komunikačním kanálu (např. admin, moderátor, guest).
    /// </summary>
    public class CommunicationChannelRole : AuditableEntity, IAuditableEntity
    {
        private List<User> _users;
        
        protected CommunicationChannelRole()
        {
            _users = new List<User>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název role.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, pod který spadá tato role.
        /// </summary>
        public string ChannelId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje práva uživatele, který disponuje touto rolí.
        /// </summary>
        public CommunicationChannelPermission Permission { get; private set; }

        public virtual IReadOnlyCollection<User> Users => _users.AsReadOnly();

        public static CommunicationChannelRole Create(string name, string channelId,
            CommunicationChannelPermission permission)
        {
            var entity = new CommunicationChannelRole()
            {
                ChannelId = channelId
            };
            
            entity.SetName(name);
            entity.SetPermission(permission);

            return entity;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPermission(CommunicationChannelPermission permission)
        {
            Permission = permission;
        }
    }
}
