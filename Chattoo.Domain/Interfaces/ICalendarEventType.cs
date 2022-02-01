namespace Chattoo.Domain.Interfaces
{
    /// <summary>
    /// Tyl kalendářní události.
    /// </summary>
    public interface ICalendarEventType
    {
        /// <summary>
        /// Vrací nebo nastavuje název typu kalendářní události.
        /// </summary>
        string Name { get; set; } 
    }
}