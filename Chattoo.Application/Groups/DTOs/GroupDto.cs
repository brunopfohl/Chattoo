using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;

namespace Chattoo.Application.Groups.DTOs
{
    /// <summary>
    /// Skupina uživatelů.
    /// </summary>
    public class GroupDto : AuditableDto, IMapFrom<Group>
    {
        /// <summary>
        /// Vrací nebo nastavuje název skupiny uživatelů.
        /// </summary>
        public string Name { get; set; }
    }
}