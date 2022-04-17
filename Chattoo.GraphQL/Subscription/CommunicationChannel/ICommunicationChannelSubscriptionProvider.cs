using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Chattoo.Application.CommunicationChannels.DTOs;

namespace Chattoo.GraphQL.Subscription.CommunicationChannel
{
    public interface ICommunicationChannelSubscriptionProvider
    {
        IObservable<CommunicationChannelDto> CommunicationChannels();
        CommunicationChannelDto UpdateCommunicationChannel(CommunicationChannelDto communicationChannel);
        
        ConcurrentStack<CommunicationChannelDto> AllCommunicationChannels { get; } 
    }
}