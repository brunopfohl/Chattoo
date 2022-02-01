using System.Collections.Generic;
using System.Threading.Tasks;
using Chattoo.Application.Common.Exceptions;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Common;
using Chattoo.Domain.Exceptions;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;

namespace Chattoo.Application.Common.Services
{
    public class GetByIdUserSafeService
    {
        private readonly ICurrentUserService _currentUserService;

        public GetByIdUserSafeService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        
        public async Task<TEntity> GetAsync<TEntity, TKey>(IRepository<TEntity, TKey> repository, TKey id) where TEntity : Entity<TKey>
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                return null;
            }
            
            var entity = await repository.GetByIdAsync(id);

            TryThrowException(entity, id);

            return entity;
        }
        
        public TEntity Get<TEntity, TKey>(IRepository<TEntity, TKey> repository, TKey id) where TEntity : Entity<TKey>
        {
            if (EqualityComparer<TKey>.Default.Equals(id, default))
            {
                return null;
            }
            
            var entity = repository.GetById(id);

            TryThrowException(entity, id);

            return entity;
        }

        private void TryThrowException<TEntity, TKey>(TEntity entity, TKey id) where TEntity : Entity<TKey>
        {
            if (entity == null)
            {
                throw new NotFoundException(nameof(TKey), id);
            }

            if (entity is IWithRestrictedReadPermissions restrictedEntity)
            {
                ThrowIfNotAccessible(restrictedEntity);
            }
        }
        
        private void ThrowIfNotAccessible(IWithRestrictedReadPermissions restrictedEntity)
        {
            bool hasAccess = !(restrictedEntity.Users != null && !restrictedEntity.Users.Contains(_currentUserService.User));

            if (restrictedEntity.User != null && restrictedEntity.User != _currentUserService.User)
            {
                hasAccess = false;
            }

            if (!hasAccess)
            {
                throw new ForbiddenAccessException();
            }
        }
    }
}