using System;
using System.Linq;
using Castle.MicroKernel;
using Castle.Windsor;

namespace Standings.Web.Tests.Installers
{
    public static class WindsorContainerHelper
    {
        public static IHandler[] GetAllHandlers(this IWindsorContainer container)
        {
            return GetHandlersFor(container, typeof(object));
        }

        public static IHandler[] GetHandlersFor(this IWindsorContainer container, Type type)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        public static Type[] GetAllImplementation(this IWindsorContainer container)
        {
            return container.GetAllHandlers()
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

        public static Type[] GetImplementationTypesFor(this IWindsorContainer container, Type type)
        {
            return container.GetHandlersFor(type)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy(t => t.Name)
                .ToArray();
        }

    }
}
