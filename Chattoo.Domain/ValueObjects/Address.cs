using System.Collections.Generic;
using Chattoo.Domain.Common;

namespace Chattoo.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        protected Address()
        {
            
        }
        
        /// <summary>
        /// Vrací nebo nastavuje název místa.
        /// </summary>
        public string Name { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje stát.
        /// </summary>
        public string Country { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje město.
        /// </summary>
        public string City { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje název ulice.
        /// </summary>
        public string Street { get; private set; }
        
        /// <summary>
        /// Vrací nebo nastavuje popisné číslo budovy (textový řetězec může uložit i hodnoty typu "560/25").
        /// </summary>
        public string StreetNumber { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Country;
            yield return City;
            yield return Street;
            yield return StreetNumber;
        }
    }
}