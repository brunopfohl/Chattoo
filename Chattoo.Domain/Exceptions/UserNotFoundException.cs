using System;

namespace Chattoo.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}