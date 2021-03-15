using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita aliasu (přezdívky) uživatele.
    /// </summary>
    public class UserAlias : AuditableEntity, IAuditableEntity
    {
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele s touto přezdívkou.
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje uživatele s touto přezdívkou.
        /// </summary>
        public virtual User User { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje alias (přezdívku) uživatele.1
        /// </summary>
        public string Alias { get; set; }
    }
}
