using System;

namespace Chattoo.Application.Common.Security
{
    /// <summary>
    /// Atribut jehož použití nad třídou znamená nutnost autorizace.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AuthorizeAttribute : Attribute
    {
        /// <summary>
        /// Vytvoří novou prázdnou instanci třídy <see cref="AuthorizeAttribute"/>. 
        /// </summary>
        public AuthorizeAttribute() { }

        /// <summary>
        /// Vrací nebo nastavuje seznam rolí (oddělených pomocí čásky ','), které mají přístup ke třídě.
        /// </summary>
        public string Roles { get; set; }

        /// <summary>
        /// Vrací nebo nastavuje název pravidla, které určuje přístup ke třídě.
        /// </summary>
        public string Policy { get; set; }

    }
}
