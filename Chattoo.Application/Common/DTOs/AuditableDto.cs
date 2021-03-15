using System;

namespace Chattoo.Application.Common.DTOs
{
    /// <summary>
    /// Objekt, který nese informace o tom úpravách (kdy a kým byl vytvořen, upraven, smazán).
    /// </summary>
    public class AuditableDto<TKey> : WithKeyDto<TKey>
    {
        /// <summary>
        /// Vrací nebo nastavuje, kdy byl záznam vytvořen.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje kým (kterým uživatelem) byl objekt vytvořen.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje, kdy byl záznam naposledy upraven.
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje kým (kterým uživatelem) byl záznam naposledy upraven.
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje, kdy byl záznam smazán (pokud byl).
        /// </summary>
        public DateTime? DeletedAt { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje kým (kterým uživatelem) byl záznam smazán.
        /// </summary>
        public string DeletedBy { get; set; }
    }

    /// <summary>
    /// Objekt, který nese informace o tom úpravách (kdy a kým byl vytvořen, upraven, smazán).
    /// </summary>
    public class AuditableDto : AuditableDto<string>
    {
        
    }
}