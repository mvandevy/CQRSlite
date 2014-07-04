using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using CQRSCode.ReadModel;
using CQRSCode.WriteModel.Handlers;
using CQRSlite;
using CQRSlite.Bus;
using CQRSlite.Config;
using CQRSlite.Messages;
using CQRSWeb.Controllers;

namespace CQRSWeb
{	
    public class MvcApplication : System.Web.HttpApplication
    {

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AutofacServiceLocator>().As<IServiceLocator>();
            builder.RegisterType<HomeController>().InstancePerHttpRequest();
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterHandlers((IServiceLocator) (DependencyResolver.Current.GetService(typeof(IServiceLocator))));
        }

        private void RegisterHandlers(IServiceLocator serviceLocator)
        {
            var registrar = new BusRegistrar(serviceLocator);
			registrar.Register(typeof(InventoryCommandHandlers));
        }
    }
}