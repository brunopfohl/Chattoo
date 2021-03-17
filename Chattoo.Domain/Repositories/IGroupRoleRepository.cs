using System.Linq;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní repozitáře rolí dostupných ve skupinách uživatelů.
    /// </summary>
    public interface IGroupRoleRepository : IRepository<GroupRole>
    {
        /// <summary>
        /// Vrací role dostupné v určité skupině.
        /// </summary>
        /// <param name="groupId">Id skupiny, jejíž dostupné role má metoda vrátit.`</param>
        /// <returns>Role dostupné ve skupině.</returns>
        public IQueryable<GroupRole> GetByGroupId(string groupId);

        /// <summary>
        /// Vrací role uživatele ve skupině.
        /// </summary>
        /// <param name="userId">Id uživatele, jehož role má metoda vrátit.</param>
        /// <param name="groupId">Id skupiny ve které se hledají role uživatele.</param>
        /// <returns>Role uživatele ve skupině.</returns>
        public IQueryable<GroupRole> GetForUserInGroup(string userId, string groupId);
    }
}