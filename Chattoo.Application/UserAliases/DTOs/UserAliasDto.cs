using Chattoo.Application.Common.DTOs;
using Chattoo.Application.Common.Mappings;
using Chattoo.Domain.Entities;

namespace Chattoo.Application.UserAliases.DTOs
{
    /// <summary>
    /// Alias (přezdívka) uživatele.
    /// </summary>
    public class UserAliasDto : AuditableDto, IMapFrom<UserAlias>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele s touto přezdívkou.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje alias (přezdívku) uživatele.1
        /// </summary>
        public string Alias { get; set; }
    }
}