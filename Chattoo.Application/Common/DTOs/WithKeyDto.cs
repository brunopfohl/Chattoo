using Chattoo.Domain.Interfaces;

namespace Chattoo.Application.Common.DTOs
{
    /// <summary>
    /// Objekt, který má identifikační klíč "Id".
    /// </summary>
    /// <typeparam name="TKey">Typ identifikačního klíče.</typeparam>
    public class WithKeyDto<TKey> : IHasKey<TKey>
    {
        /// <summary>
        /// Vrací nebo nastavuje Id záznamu.
        /// </summary>
        public TKey Id { get; set; }
    }
    
    /// <summary>
    /// Objekt, který má identifikační klíč "Id" typu <see cref="string"/>.
    /// </summary>
    public class WithKeyDto : WithKeyDto<string>
    {
        
    }
}