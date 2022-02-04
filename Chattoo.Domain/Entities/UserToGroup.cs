using System.Collections.Generic;
using Chattoo.Domain.Common;

namespace Chattoo.Domain.Entities
{
    public class UserToGroup : ValueObject
    {
        protected UserToGroup()
        {
            
        }
        
        public string UserId { get; private set; }
        
        public string GroupId { get; private set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return GroupId;
        }

        public static UserToGroup Create(string userId, string groupId)
        {
            var entity = new UserToGroup()
            {
                UserId = userId,
                GroupId = groupId
            };

            return entity;
        }
    }
}