using System;

namespace Chattoo.Domain.Interfaces
{
    public interface IAggregateRoot<TKey> : IHasKey<TKey>
    {
    }

    public interface IAggregateRoot : IAggregateRoot<string>
    {
        
    }
}