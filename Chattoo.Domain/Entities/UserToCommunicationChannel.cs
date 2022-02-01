using System.Collections.Generic;
using Chattoo.Domain.Common;

namespace Chattoo.Domain.Entities
{
    public class UserToCommunicationChannel : ValueObject
    {
        protected UserToCommunicationChannel()
        {
            
        }
        
        public string UserId { get; private set; }
        
        public string ChannelId { get; private set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return UserId;
            yield return ChannelId;
        }
    }
}