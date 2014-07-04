using System;

namespace CQRSlite.Messages
{
    public interface IMessageRouteRegistrar
    {
        void Register(params Type[] typesFromAssemblyContainingMessages);
    }
}
