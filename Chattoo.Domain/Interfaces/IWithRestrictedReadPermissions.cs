using System;
using System.Collections.Generic;
using Chattoo.Domain.Entities;

namespace Chattoo.Domain.Interfaces
{
    /// <summary>
    /// Rozhraní pro entitu, která má určitého majitele.
    /// </summary>
    public interface IWithRestrictedReadPermissions
    {
        /// <summary>
        /// Uživatel, který má přístup k objektu.
        /// </summary>
        User User => null;
        
        /// <summary>
        /// Uživatelé, kteří mají přístup k objektu.
        /// </summary>
       ICollection<User> Users => null;
    }
}