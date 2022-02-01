using System.Text;

namespace Chattoo.Domain.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string text)
        {
            var result = Encoding.ASCII.GetBytes(text);
            return result;
        }

        /// <summary>
        /// Vrací, zda-li je textový řetězec prázdný nebo je null.
        /// </summary>
        /// <param name="text">Textový řetězec</param>
        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }
        
        /// <summary>
        /// Vrací, zda-li má textový řetězec hodnotu (není null nebo není prázdný).
        /// </summary>
        /// <param name="text">Textový řetězec</param>
        public static bool IsNotNullOrEmpty(this string text)
        {
            return !text.IsNullOrEmpty();
        }
    }
}