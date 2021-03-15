using System;

namespace Chattoo.Application.Common.Interfaces
{
    /// <summary>
    /// Rozhraní pro službu poskytující aktuální čas.
    /// </summary>
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
