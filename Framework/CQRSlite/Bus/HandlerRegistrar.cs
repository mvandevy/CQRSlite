using System;
using System.Collections.Generic;
using CQRSlite.Infrastructure;
using CQRSlite.Messages;

namespace CQRSlite.Bus
{
    public class HandlerRegistrar : IHandlerRegistrar
    {
        private static readonly Dictionary<Type, IList<Action<IMessage>>> Routes = new Dictionary<Type, IList<Action<IMessage>>>();

        public void RegisterHandler<T>(Action<T> handler) where T : IMessage
        {
            IList<Action<IMessage>> handlers;
            if (!Routes.TryGetValue(typeof(T), out handlers))
            {
                handlers = new List<Action<IMessage>>();
                Routes.Add(typeof(T), handlers);
            }
            handlers.Add(DelegateAdjuster.CastArgument<IMessage, T>(x => handler(x)));
        }

        public bool TryGetRoute(Type type,
            out IList<Action<IMessage>> handlers)
        {
            return Routes.TryGetValue(
                type,
                out handlers);
        }
    }
}
