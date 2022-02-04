using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita aliasu (přezdívky) uživatele.
    /// </summary>
    public class UserAlias : AuditableEntity, IAuditableEntity
    {
        protected UserAlias()
        {
            
        }
        
        /// <summary>
        /// Vrací nebo nastavuje Id uživatele s touto přezdívkou.
        /// </summary>
        public string UserId { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje alias (přezdívku) uživatele.1
        /// </summary>
        public string Alias { get; private set; }

        public void SetAlias(string alias)
        {
            Alias = alias;
        }
        
        public static UserAlias Create(string userId, string alias)
        {
            var entity = new UserAlias
            {
                UserId = userId,
            };
            
            entity.SetAlias(alias);

            return entity;
        }
    }
}
