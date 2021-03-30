using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Domain.Common;
using Chattoo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Výchozí repozitář obsahující základní akce pro manipulaci s datovým zdrojem (z tohoto repozitáře dědí ostatní repozitáře).
    /// </summary>
    /// <typeparam name="TEntity">Typ entity</typeparam>
    /// <typeparam name="TKey">Unikátní klíč entity</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public IUnitOfWork UnitOfWork => _dbContext;

        public Repository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddOrUpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity.Id.Equals(default(TKey)))
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

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<T> GetAll<T>()
        {
            var result = GetAll().ProjectTo<T>(_mapper.ConfigurationProvider);
            return result;
        }

        public async Task<TEntity> GetByIdAsync(TKey id, bool throwNotFound = false)
        {
            var result = await _dbContext.FindAsync<TEntity>(id);

            if (throwNotFound && result is null)
            {
                throw new NotFoundException(nameof(TKey), id);
            }
            
            return result;
        }

        public async Task<T> GetByIdAsync<T>(TKey id, bool throwNotFound = false)
        {
            var entity = await GetByIdAsync(id, throwNotFound);
            var result = _mapper.Map<T>(entity);
            return result;
        }

        public void Edit(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public Task<T> FirstOrDefaultAsync<T>(IQueryable<T> query)
        {
            return query.FirstOrDefaultAsync();
        }

        public Task<T> SingleOrDefaultAsync<T>(IQueryable<T> query)
        {
            return query.SingleOrDefaultAsync();
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> query)
        {
            return query.ToListAsync();
        }

        public bool Exists(TKey id)
        {
            var result = GetAll().Any(e => e.Id.Equals(id));

            return result;
        }

        public void ThrowIfNotExists(TKey id)
        {
            var exists = Exists(id);

            if (!exists)
            {
                throw new NotFoundException(nameof(TKey), id);
            }
        }
    }

    /// <summary>
    /// Výchozí repozitář obsahující základní akce pro manipulaci s datovým zdrojem (z tohoto repozitáře dědí ostatní repozitáře).
    /// </summary>
    public class Repository<TEntity> : Repository<TEntity, string> where TEntity : Entity<string>
    {
        public Repository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
