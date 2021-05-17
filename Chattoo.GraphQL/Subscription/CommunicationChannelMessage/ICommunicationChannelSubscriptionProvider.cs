using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Chattoo.Application.CommunicationChannels.DTOs;

namespace Chattoo.GraphQL.Subscription.CommunicationChannelMessage
{
    public interface ICommunicationChannelSubscriptionProvider
    {
        IObservable<CommunicationChannelDto> CommunicationChannels();
        CommunicationChannelDto UpdateCommunicationChannel(CommunicationChannelDto communicationChannel);
        
        ConcurrentStack<CommunicationChannelDto> AllCommunicationChannels { get; } 
    }

    public class CommunicationChannelSubscriptionProvider: ICommunicationChannelSubscriptionProvider
    {
        private readonly ISubject<CommunicationChannelDto> _communicationChannelStream =
            new ReplaySubject<CommunicationChannelDto>(1);
        
        public ConcurrentStack<CommunicationChannelDto> AllCommunicationChannels { get; }

        public CommunicationChannelSubscriptionProvider()
        {
            AllCommunicationChannels = new ConcurrentStack<CommunicationChannelDto>();
        }
        
        public IObservable<CommunicationChannelDto> CommunicationChannels()
        {
            return _communicationChannelStream.AsObservable();
        }

        public CommunicationChannelDto UpdateCommunicationChannel(
            CommunicationChannelDto communicationChannel)
        {
            _communicationChannelStream.OnNext(communicationChannel);
            return communicationChannel;
        }

        public void AddError(Exception exception)
        {
            _communicationChannelStream.OnError(exception);
        }
    }
}