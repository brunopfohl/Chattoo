using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Chattoo.Domain.Common;
using Chattoo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chattoo.Infrastructure.Persistence.Repositories
{
    public class ReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        protected readonly ApplicationDbContext _dbContext;
        protected readonly IMapper _mapper;

        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public IUnitOfWork UnitOfWork => _dbContext;

        public ReadOnlyRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<T> GetAll<T>()
        { 
            return GetAll().ProjectTo<T>(_mapper.ConfigurationProvider);
        }

        public TEntity GetById(TKey id)
        {
            return _dbContext.Find<TEntity>(id);
        }
        
        public IQueryable<TEntity> GetByIds(params TKey[] ids)
        {
            return GetAll().Where(e => ids.Contains(e.Id));
        }
        
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public async Task<T> GetByIdAsync<T>(TKey id)
        {
            var entity = await GetByIdAsync(id);
            var result = _mapper.Map<T>(entity);
            return result;
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
    }
    
    public class ReadOnlyRepository<TEntity> : Repository<TEntity, string> where TEntity : Entity<string>
    {
        public ReadOnlyRepository(ApplicationDbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
        }
    }
}