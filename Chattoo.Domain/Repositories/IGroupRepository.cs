using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní repozitáře skupin uživatelů.
    /// </summary>
    public interface IGroupRepository : IRepository<Group>
    {
        /// <summary>
        /// Vrací uživatelské skupiny, do kterých spadá daný uživatel.
        /// </summary>
        /// <param name="userId">Id uživatele, jehož skupiny se mají načíst.</param>
        /// <returns>Kolekci <see cref="Group"/> uživatelských skupin, kterých je daný uživatel součástí.</returns>
        public IQueryable<Group> GetByUserId(string userId);
    }
}