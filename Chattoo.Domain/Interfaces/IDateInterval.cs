using System;

namespace Chattoo.Domain.Interfaces
{
    /// <summary>
    /// Rozhraní časového intervalu.
    /// </summary>
    public interface IDateInterval
    {
        /// <summary>
        /// Vrací nebo nastavuje počátek časového intervalu.
        /// </summary>
        DateTime StartsAt { get; }
        
        /// <summary>
        /// Vrací nebo nastavuje konec časového intervalu.
        /// </summary>
        DateTime EndsAt { get; }
    }
}