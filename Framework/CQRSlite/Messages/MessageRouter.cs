using System;
using System.Collections.Generic;
using CQRSlite.Bus;

namespace CQRSlite.Messages
{
    public class MessageRouter : IMessageRouter
    {
        private readonly IHandlerRegistrar _handlerRegistrar;

        public MessageRouter(IHandlerRegistrar handlerRegistrar)
        {
            if (handlerRegistrar == null)
                throw new ArgumentNullException("handlerRegistrar");

            _handlerRegistrar = handlerRegistrar;
        }
        
        public bool TryGetRouteHandlers(IMessage message, out IList<Action<IMessage>> handlers)
        {
            var routingKey = message.GetType();
            return TryGetRoute(routingKey, out handlers);
        }

        public bool TryGetRoute(Type type, out IList<Action<IMessage>> handlers)
        {
            return _handlerRegistrar.TryGetRoute(type, out handlers);
        }
    }
}
