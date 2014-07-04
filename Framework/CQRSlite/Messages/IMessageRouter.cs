using System;
using System.Collections.Generic;

namespace CQRSlite.Messages
{
    public interface IMessageRouter
    {
        bool TryGetRouteHandlers(IMessage message, out IList<Action<IMessage>> handlers);
        bool TryGetRoute(Type type, out IList<Action<IMessage>> handlers);
    }
}
