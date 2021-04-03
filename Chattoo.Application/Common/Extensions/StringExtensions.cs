using System.Text;

namespace Chattoo.Application.Common.Extensions
{
    public static class StringExtensions
    {
        public static byte[] GetBytes(this string text)
        {
            var result = Encoding.ASCII.GetBytes(text);
            return result;
        }
    }
}