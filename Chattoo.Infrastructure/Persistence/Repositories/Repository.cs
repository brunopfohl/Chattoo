using AutoMapper;
using Chattoo.Domain.Common;
using Chattoo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Výchozí repozitář obsahující základní akce pro manipulaci s datovým zdrojem (z tohoto repozitáře dědí ostatní repozitáře).
    /// </summary>
    /// <typeparam name="TEntity">Typ entity</typeparam>
    /// <typeparam name="TKey">Unikátní klíč entity</typeparam>
    public class Repository<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        public Repository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity.Id is null || entity.Id.Equals(default(TKey)))
            {
                await DbSet.AddAsync(entity, cancellationToken);
            }
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void Edit(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }

    /// <summary>
    /// Výchozí repozitář obsahující základní akce pro manipulaci s datovým zdrojem (z tohoto repozitáře dědí ostatní repozitáře).
    /// </summary>
    public class Repository<TEntity> : Repository<TEntity, string> where TEntity : Entity<string>
    {
        public Repository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}
