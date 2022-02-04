using System.Collections.Generic;

namespace Chattoo.Domain.Interfaces
{
    public interface IWithRestrictedWritePermissions
    {
        /// <summary>
        /// Uživatel, který má přístup k objektu.
        /// </summary>
        string UserId => null;
        
        /// <summary>
        /// Uživatelé, kteří mají přístup k objektu.
        /// </summary>
       ICollection<string> UsersIds => null;
    }
}