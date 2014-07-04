using System;
using System.Web.Mvc;
using CQRSlite.Config;

namespace CQRSPub
{
    public class AutofacServiceLocator : IServiceLocator
    {
        public T GetService<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }

        public object GetService(Type type)
        {
            return DependencyResolver.Current.GetService(type);
        }
    }
}