using Chattoo.Application.Common.DTOs;

namespace Chattoo.Application.CommunicationChannels.DTOs
{
    /// <summary>
    /// Komunikační kanál sloužící pro sdílení obsahu (zpráv) mezi uživateli.
    /// </summary>
    public class CommunicationChannelDto : AuditableDto
    {
        /// <summary>
        /// Vrací nebo nastavuje název komunikačního kanálu.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popis komunikačního kanálu.
        /// </summary>
        public string Description { get; set; }
    }
}