using Chattoo.Application.Common.DTOs;
using Chattoo.Domain.Enums;

namespace Chattoo.Application.CommunicationChannelRoles.DTOs
{
    /// <summary>
    /// Role uživatele v komunikačním kanálu (např. admin, moderátor, guest, ...).
    /// </summary>
    public class CommunicationChannelRoleDto : AuditableDto
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
    }
}