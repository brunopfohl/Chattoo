using Chattoo.Domain.Common;
using System.Threading.Tasks;

namespace Chattoo.Application.Common.Interfaces
{
    /// <summary>
    /// Rozhraní pro službu pro zapisování událostí týkajících se manipulace doménových objektů.
    /// </summary>
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
