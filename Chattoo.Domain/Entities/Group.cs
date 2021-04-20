using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita skupiny uživatelů.
    /// </summary>
    public class Group : AuditableEntity, IAuditableEntity
    {
        public Group()
        {
            Users = new List<User>();
            Roles = new List<GroupRole>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci uživatelů, kteří jsou součástí této skupiny.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci uživatelských rolí, které jsou dostupné v této skupině.
        /// </summary>
        public virtual ICollection<GroupRole> Roles { get; set; }
    }
}
