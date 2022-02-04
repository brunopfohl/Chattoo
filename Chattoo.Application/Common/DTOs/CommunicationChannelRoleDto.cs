using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Enums;

namespace Chattoo.Application.Common.DTOs
{
    /// <summary>
    /// Role uživatele v komunikačním kanálu (např. admin, moderátor, guest, ...).
    /// </summary>
    public class CommunicationChannelRoleDto : AuditableDto, IMapFrom<CommunicationChannelRole>
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