﻿using System.Collections.Generic;
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
        /// <summary>
        /// Vrací nebo nastavuje název uživatelské role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje oprávnění, které uživatelům poskytuje tato role.
        /// </summary>
        public UserGroupPermission Permission { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kolekci uživatelů, kteří disponují touto rolí.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
    }
}