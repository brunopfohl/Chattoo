using Chattoo.Application.Common.DTOs;

namespace Chattoo.Application.Groups.DTOs
{
    /// <summary>
    /// Skupina uživatelů.
    /// </summary>
    public class GroupDto : AuditableDto
    {
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
    }
}