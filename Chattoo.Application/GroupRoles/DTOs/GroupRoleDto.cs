using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Enums;

namespace Chattoo.Application.GroupRoles.DTOs
{
    /// <summary>
    /// Uživatelská role v kontextu skupiny.
    /// </summary>
    public class GroupRoleDto : AuditableDto
    {
        /// <summary>
        /// Vrací nebo nastavuje název uživatelské role.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje oprávnění, které uživatelům poskytuje tato role.
        /// </summary>
        public UserGroupPermission Permission { get; set; }
    }
}