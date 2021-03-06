using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Chattoo.Application.Common.DTOs;

namespace Chattoo.GraphQL.Subscription.CommunicationChannelMessage
{
    public class CommunicationChannelMessageSubscriptionProvider: ICommunicationChannelMessageSubscriptionProvider
    {
        private readonly ISubject<CommunicationChannelMessageDto> _communicationChannelMessageStream =
            new ReplaySubject<CommunicationChannelMessageDto>(1);
        
        public ConcurrentStack<CommunicationChannelMessageDto> AllCommunicationChannelMessages { get; }

        public CommunicationChannelMessageSubscriptionProvider()
        {
            AllCommunicationChannelMessages = new ConcurrentStack<CommunicationChannelMessageDto>();
        }
        
        public IObservable<CommunicationChannelMessageDto> CommunicationChannelMessages()
        {
            return _communicationChannelMessageStream.AsObservable();
        }

        public CommunicationChannelMessageDto AddCommunicationChannelMessage(
            CommunicationChannelMessageDto communicationChannelMessage)
        {
            _communicationChannelMessageStream.OnNext(communicationChannelMessage);
            return communicationChannelMessage;
        }

        public void AddError(Exception exception)
        {
            _communicationChannelMessageStream.OnError(exception);
        }
    }
}