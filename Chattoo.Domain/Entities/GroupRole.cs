using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Enums;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita uživatelské role v kontextu skupiny.
    /// </summary>
    public class GroupRole : AuditableEntity, IAuditableEntity
    {
        protected GroupRole()
        {
            
        }
        
        /// <summary>
        /// Vrací nebo nastavuje Id skupiny, do které tato role patří.
        /// </summary>
        public string GroupId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název uživatelské role.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Vrací nebo nastavuje oprávnění, které uživatelům poskytuje tato role.
        /// </summary>
        public UserGroupPermission Permission { get; private set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
