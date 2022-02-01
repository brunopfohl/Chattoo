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
        protected CommunicationChannelRole()
        {
            
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
        
        public virtual ICollection<User> Users { get; set; }
    }
}
