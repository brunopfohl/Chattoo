using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;

namespace Chattoo.Application.Users.DTOs
{
    /// <summary>
    /// Uživatel aplikace.
    /// </summary>
    public class UserDto : AuditableDto, IMapFrom<User>
    {
        /// <summary>
        /// Vrací nebo nastavuje uživatelské jméno uživatele.
        /// </summary>
        public string UserName { get; set; }
    }
}