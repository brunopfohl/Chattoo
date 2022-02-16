using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chattoo.Domain.Common;

namespace Chattoo.Domain.Repositories
{
    /// <summary>
    /// Rozhraní pro repozitář pro získávání dat z určitého zdroje (např. ORM databáze).
    /// </summary>
    /// <typeparam name="TEntity">Třída entity</typeparam>
    /// <typeparam name="TKey">Datový typ unikátního klíče entity</typeparam>
    public interface IReadOnlyRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IUnitOfWork UnitOfWork { get; }

        IQueryable<TEntity> GetAll();

        IQueryable<T> GetAll<T>();

        TEntity GetById(TKey id);
        
        IQueryable<TEntity> GetByIds(params TKey[] ids);
        
        Task<TEntity> GetByIdAsync(TKey id);

        Task<T> GetByIdAsync<T>(TKey id);

        Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query);

        Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query);

        Task<List<T>> ToListAsync<T>(IQueryable<T> query);
        
        bool Exists(TKey id);
    }
    
    public interface IReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity, string> where TEntity : Entity<string>
    {
        
    }
}