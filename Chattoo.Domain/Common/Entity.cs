using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Common
{
    /// <summary>
    /// Entita, která má vlastnost/sloupec "Id".
    /// </summary>
    /// <typeparam name="TKey">Datový typ unikátního klíče entity.</typeparam>
    public abstract class Entity<TKey> : IHasKey<TKey>
    {
        public TKey Id { get; set; }
    }
}
