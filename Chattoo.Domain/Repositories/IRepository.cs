using Chattoo.Domain.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní pro repozitář pro získávání/manipulaci dat z určitého zdroje (např. ORM databáze).
    /// </summary>
    /// <typeparam name="TEntity">Třída entity</typeparam>
    /// <typeparam name="TKey">Datový typ unikátního klíče entity</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<TEntity> GetAll();

        IQueryable<T> GetAll<T>();

        Task<TEntity> GetByIdAsync(TKey id);

        Task<T> GetByIdAsync<T>(TKey id);

        void Edit(TEntity entity);
        public void RemoveRange(IEnumerable<TEntity> entities);

        Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        void Remove(TEntity entity);

        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query);

        Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query);

        Task<List<T>> ToListAsync<T>(IQueryable<T> query);
    }

    /// <summary>
    /// Rozhraní pro repozitář pro získávání/manipulaci dat z určitého zdroje (např. ORM databáze).
    /// </summary>
    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : Entity<string>
    {
        
    }
}
