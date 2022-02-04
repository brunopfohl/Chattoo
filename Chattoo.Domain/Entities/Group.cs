using System.Collections.Generic;
using System.Linq;
using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita skupiny uživatelů.
    /// </summary>
    public class Group : AuditableEntity, IAuditableEntity, IAggregateRoot,
        IWithRestrictedReadPermissions, IWithRestrictedWritePermissions
    {
        protected Group()
        {
            Roles = new List<GroupRole>();
            Participants = new List<UserToGroup>();
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci uživatelských rolí, které jsou dostupné v této skupině.
        /// </summary>
        public virtual ICollection<GroupRole> Roles { get; set; }
        
        public virtual ICollection<UserToGroup> Participants { get; set; }
        
        #region IWithRestrictedReadPermissions

        ICollection<string> IWithRestrictedReadPermissions.UsersIds =>
            Participants.Select(u => u.UserId).ToList();

        #endregion
        
        #region IWithRestrictedWritePermissions

        ICollection<string> IWithRestrictedWritePermissions.UsersIds =>
            Participants.Select(u => u.UserId).ToList();

        #endregion
    }
}
