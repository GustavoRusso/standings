using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Standings.Infrastructure.Web.DependencyResolver;
using Standings.Infrastructure.Web.SessionPerRequest;

namespace Standings.Infrastructure.Web
{
    public class HttpApplicationByConventions : System.Web.HttpApplication, IContainerAccessor
    {
        private readonly IWindsorContainer _container;
        public IWindsorContainer Container
        {
            get { return _container; }
        }

        public HttpApplicationByConventions()
        {
            _container = new WindsorContainer();
        }

        public HttpApplicationByConventions(IWindsorContainer container)
        {
            _container = container;
        }

        public virtual void Application_Start()
        {
            IWindsorInstaller windsorInstaller = FromAssembly.Instance(Assembly.GetCallingAssembly());
            _container.Install(windsorInstaller);

            System.Web.Mvc.DependencyResolver.SetResolver(new WindsorDependencyResolver(System.Web.Mvc.DependencyResolver.Current, _container.Kernel));

            GlobalFilters.Filters.Add(new NHibernateSessionPerRequestFilter());
        }

        public virtual void Application_End()
        {
            _container.Dispose();
        }
    }
}
