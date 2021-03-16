﻿using System.Collections.Generic;
using Chattoo.Domain.Common;
using Chattoo.Domain.Interfaces;

namespace Chattoo.Domain.Entities
{
    /// <summary>
    /// Entita komunikačního kanálu sloužícího pro sdílení obsahu (zpráv) mezi uživateli.
    /// </summary>
    public class CommunicationChannel : AuditableEntity, IAuditableEntity
    {
        /// <summary>
        /// Vrací nebo nastavuje název komunikačního kanálu.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popis komunikačního kanálu.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci uživatelů, kteří jsou zařazeni do tohoto komunikačního kanálu.
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci zpráv, které byly mezi uživateli sdíleny pomocí tohoto komunikačního kanálu.
        /// </summary>
        public virtual ICollection<CommunicationChannelMessage> Messages { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje kolekci dostupných rolí pro uživatele v tomto kanálu (např. admin, moderátor, quest,...).
        /// </summary>
        public virtual ICollection<CommunicationChannelRole> Roles { get; set; }
    }
}