using Autofac;
using CQRSCode.WriteModel.Handlers;
using CQRSlite.Messages;

namespace CQRSPub.App_Start
{
    public static class HandlersConfig
    {
        public static void RegisterHandlers(IContainer container)
        {
            var registrar = container.Resolve<IMessageRouteRegistrar>();
            registrar.Register(typeof(InventoryCommandHandlers));
        }
        
    }
}