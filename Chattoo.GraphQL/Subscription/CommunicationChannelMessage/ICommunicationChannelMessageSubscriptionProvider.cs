using System;
using System.Collections.Concurrent;
using Chattoo.Application.Common.DTOs;

namespace Chattoo.GraphQL.Subscription.CommunicationChannelMessage
{
    public interface ICommunicationChannelMessageSubscriptionProvider
    {
        IObservable<CommunicationChannelMessageDto> CommunicationChannelMessages();
        CommunicationChannelMessageDto AddCommunicationChannelMessage(CommunicationChannelMessageDto communicationChannelMessage);
        
        ConcurrentStack<CommunicationChannelMessageDto> AllCommunicationChannelMessages { get; } 
    }
}