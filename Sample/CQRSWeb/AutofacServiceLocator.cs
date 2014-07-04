using System;
using Autofac;
using CQRSlite.Config;

namespace CQRSWeb
{
    public class AutofacServiceLocator : IServiceLocator
    {
        private IContainer _container;

        public AutofacServiceLocator(IContainer container)
        {
            _container = container;
        }

        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }

        public object GetService(Type type)
        {
            return _container.Resolve(type);
        }
    }
}