using Chattoo.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Chattoo.Infrastructure.Identity
{
    /// <summary>
    /// Třída poskytující extension metody pro objekty typu <see cref="IdentityResult"/>.
    /// </summary>
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
