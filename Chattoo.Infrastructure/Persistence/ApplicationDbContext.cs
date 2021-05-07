using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Chattoo.Application.Common.Interfaces;
using Chattoo.Domain.Entities;
using Chattoo.Domain.Interfaces;
using Chattoo.Domain.Repositories;
using Chattoo.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;

namespace Chattoo.Infrastructure.Persistence
{
    /// <summary>
    /// Třída databázového kontextu (jednotky pro správu dat v databázi).
    /// </summary>
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IUnitOfWork
    {
        private IDbContextTransaction _dbContextTransaction;

        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        private readonly ICurrentUserIdService _currentUserIdService;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            IDomainEventService domainEventService,
            IDateTime dateTime,
            ICurrentUserIdService currentUserIdService) : base(options, operationalStoreOptions)
        {
            _domainEventService = domainEventService;
            _dateTime = dateTime;
            _currentUserIdService = currentUserIdService;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var users = Set<User>();
            
            foreach (EntityEntry<ApplicationUser> entry in ChangeTracker.Entries<ApplicationUser>().ToList())
            {
                var appUser = await users.FindAsync(entry.Entity.Id);

                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        Id = entry.Entity.Id,
                        UserName = entry.Entity.UserName
                    };

                    await users.AddAsync(newAppUser, cancellationToken);
                }
            }
            
            foreach (EntityEntry<IAuditableEntity> entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserIdService.UserId;
                        entry.Entity.CreatedAt = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _currentUserIdService.UserId;
                        entry.Entity.ModifiedAt = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _dbContextTransaction = Database.BeginTransaction(isolationLevel);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);
                
                if (domainEventEntity is null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
