using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using CQRSCode.ReadModel;
using CQRSCode.WriteModel;
using CQRSlite.Bus;
using CQRSlite.Cache;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using StructureMap;
using StructureMap.Graph;
using IContainer = StructureMap.IContainer;

namespace CQRSWeb
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InProcessBus>().AsSelf().SingleInstance();
            builder.RegisterType<InProcessBus>().As<ICommandSender>();
            builder.RegisterType<InProcessBus>().As<IEventPublisher>();
            builder.RegisterType<InProcessBus>().As<IHandlerRegistrar>();
            builder.RegisterType<Session>().As<ISession>().InstancePerHttpRequest();
            builder.RegisterType<InMemoryEventStore>().As<IEventStore>().SingleInstance();
            builder.Register(
                c => new CacheRepository(
                    new Repository(
                        c.Resolve<IEventStore>(),
                        c.Resolve<IEventPublisher>()),
                    c.Resolve<IEventStore>()));
        }

        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
            {
                x.Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.AssemblyContainingType<ReadModelFacade>();
                    s.Convention<FirstInterfaceConvention>();
                });
            });
            return ObjectFactory.Container;
        }
    }
}