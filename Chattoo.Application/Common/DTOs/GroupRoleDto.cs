using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;

namespace Chattoo.Application.Common.DTOs
{
    /// <summary>
    /// Uživatelská role v kontextu skupiny.
    /// </summary>
    public class GroupRoleDto : AuditableDto, IMapFrom<GroupRole>
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