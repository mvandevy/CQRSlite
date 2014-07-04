using System;
using System.Collections.Generic;
using CQRSlite.Messages;

namespace CQRSlite.Bus
{
    public interface IHandlerRegistrar
    {
        void RegisterHandler<T>(Action<T> handler) where T : IMessage;
        bool TryGetRoute(Type type, out IList<Action<IMessage>> handlers);
    }
}