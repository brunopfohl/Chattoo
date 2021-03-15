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
        /// <summary>
        /// Vrací nebo nastavuje název role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje Id komunikačního kanálu, pod který spadá tato role.
        /// </summary>
        public string ChannelId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje práva uživatele, který disponuje touto rolí.
        /// </summary>
        public CommunicationChannelPermission Permission { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje komunikační kanál, pod který spadá tato role.
        /// </summary>
        public virtual CommunicationChannel Channel { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci uživatelů, kteří disponují touto rolí.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}
