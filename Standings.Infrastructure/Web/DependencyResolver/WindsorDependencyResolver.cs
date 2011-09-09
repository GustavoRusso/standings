using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;

namespace Standings.Infrastructure.Web.DependencyResolver
{
    public class WindsorDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        public readonly System.Web.Mvc.IDependencyResolver DefaultMvcResolver;
        public readonly IKernel Kernel;

        public WindsorDependencyResolver(System.Web.Mvc.IDependencyResolver defaultResolver, IKernel kernel)
        {
            DefaultMvcResolver = defaultResolver;
            Kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            if (Kernel.HasComponent(serviceType))
                return Kernel.Resolve(serviceType);

            return DefaultMvcResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var frameworkServices = DefaultMvcResolver.GetServices(serviceType);

            var kernelServices = Kernel.ResolveAll(serviceType);

            return (from s in kernelServices.Cast<object>() select s).Union(frameworkServices);
        }
    }
}
