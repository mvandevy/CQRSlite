using System;
using System.Collections.Generic;
using System.Linq;
using CQRSlite.Commands;
using CQRSlite.Events;
using CQRSlite.Messages;

namespace CQRSlite.Bus
{
    public class InProcessBus : ICommandSender, IEventPublisher
    {
        private readonly IMessageRouter _router;

        public InProcessBus(IMessageRouter router)
        {
            _router = router;
        }

        public void Send<T>(T command) where T : ICommand
        {
            IList<Action<IMessage>> handlers;
            if (_router.TryGetRouteHandlers(command, out handlers))
            {
                if (handlers.Count != 1) throw new InvalidOperationException("Cannot send to more than one handler");
                handlers.First()(command);
            }
            else
            {
                throw new InvalidOperationException("No handler registered");
            }
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            IList<Action<IMessage>> handlers; 
            if (!_router.TryGetRouteHandlers(@event, out handlers)) return;
            foreach (var handler in handlers)
            {
                handler(@event);
            }
                
        }
    }
}
