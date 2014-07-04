using Autofac;
using Autofac.Integration.Mvc;
using CQRSCode.ReadModel;
using CQRSCode.ReadModel.Handlers;
using CQRSCode.WriteModel;
using CQRSCode.WriteModel.Handlers;
using CQRSlite.Bus;
using CQRSlite.Cache;
using CQRSlite.Config;
using CQRSlite.Domain;
using CQRSlite.Events;
using CQRSlite.Messages;

namespace CQRSPub
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacServiceLocator>().As<IServiceLocator>();
            builder.RegisterType<HandlerRegistrar>().As<IHandlerRegistrar>().SingleInstance();
            builder.RegisterType<MessageRouter>().As<IMessageRouter>().SingleInstance();
            builder.RegisterType<MessageRouterRegistrar>().As<IMessageRouteRegistrar>().SingleInstance();
            
            builder.Register(
                c => new InProcessBus(c.Resolve<IMessageRouter>())).AsImplementedInterfaces().SingleInstance();
            

            builder.RegisterType<Session>().As<ISession>().InstancePerHttpRequest();
            builder.RegisterType<InMemoryEventStore>().As<IEventStore>().SingleInstance();
            builder.Register(
                c => new CacheRepository(
                    new Repository(
                        c.Resolve<IEventStore>(),
                        c.Resolve<IEventPublisher>()),
                    c.Resolve<IEventStore>())).As<IRepository>();
            builder.RegisterType<ReadModelFacade>().As<IReadModelFacade>();

            builder.RegisterType<InventoryCommandHandlers>().AsSelf().InstancePerDependency();
            builder.RegisterType<InventoryItemDetailView>().AsSelf().InstancePerDependency();
            builder.RegisterType<InventoryListView>().AsSelf().InstancePerDependency();
        }
    }
}