using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní repozitáře aliasů (přezdívek) uživatelů.
    /// </summary>
    public interface IUserAliasRepository : IRepository<UserAlias>
    {
        /// <summary>
        /// Vrací aliasy (přezdívky) uživatele s předaným Id.
        /// </summary>
        /// <param name="userId">Id uživatele, jehož přezdívky má metoda vrátit.</param>
        /// <returns>Přezdívky uživatele s Id předaném v parametru</returns>
        public IQueryable<UserAlias> GetByUserId(string userId);
    }
}