using System;

namespace Chattoo.Domain.Common
{
    /// <summary>
    /// Entita o které víme, kdy byla vytvořena případně, upravena, smazána.
    /// </summary>
    public class AuditableEntity<TKey> : Entity<TKey>
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
    /// Entita (s klíčem typu string) o které víme, kdy byla vytvořena, upravena, smazána.
    /// </summary>
    public class AuditableEntity : AuditableEntity<string>
    {
        
    }
}
