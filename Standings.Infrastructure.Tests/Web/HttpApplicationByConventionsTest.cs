using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Standings.Infrastructure.Web;
using Standings.Infrastructure.Web.DependencyResolver;
using Standings.Infrastructure.Web.SessionPerRequest;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Standings.Infrastructure.Tests.Web
{
    [TestClass]
    public class HttpApplicationByConventionsTest
    {
        [TestMethod]
        public void ImplementsIContainerAccessor()
        {
            var httpApplicationByConventions = new HttpApplicationByConventions();

            var containerAccessor = httpApplicationByConventions as IContainerAccessor;

            Assert.IsNotNull(containerAccessor);
        }

        [TestMethod]
        public void New_InstantiateAContainer()
        {
            var httpApplicationByConventions = new HttpApplicationByConventions();
            
            Assert.IsNotNull(httpApplicationByConventions.Container);
        }

        [TestMethod]
        public void Application_Start_InstallTheContainer()
        {
            var windsorContainer = MockRepository.GenerateMock<IWindsorContainer>();

            var httpApplicationByConventions = new HttpApplicationByConventions(windsorContainer);
            httpApplicationByConventions.Application_Start();

            windsorContainer.AssertWasCalled(m => m.Install(Arg<IWindsorInstaller[]>.Is.NotNull));
        }

        [TestMethod]
        public void Application_Start_SetANewDependencyResolver()
        {
            var mockMvcDefaultDependencyResolver = MockRepository.GenerateMock<IDependencyResolver>();
            System.Web.Mvc.DependencyResolver.SetResolver(mockMvcDefaultDependencyResolver);
            var mockKernel = MockRepository.GenerateMock<IKernel>();
            var stubWindsorContainer = MockRepository.GenerateStub<IWindsorContainer>();
            stubWindsorContainer.Stub(s => s.Kernel).Return(mockKernel);

            var httpApplicationByConventions = new HttpApplicationByConventions(stubWindsorContainer);
            httpApplicationByConventions.Application_Start();

            IDependencyResolver currentDependencyResolver = System.Web.Mvc.DependencyResolver.Current;
            Assert.IsNotNull(currentDependencyResolver);
            var windsorDependencyResolver = (WindsorDependencyResolver)currentDependencyResolver;
            Assert.AreEqual(mockMvcDefaultDependencyResolver, windsorDependencyResolver.DefaultMvcResolver);
            Assert.AreEqual(mockKernel, windsorDependencyResolver.Kernel);
        }

        [TestMethod]
        public void Application_Start_RegisterTheNHibernateSessionPerRequestFilterAsGlobalFilter()
        {
            var stubWindsorContainer = MockRepository.GenerateStub<IWindsorContainer>();
            GlobalFilters.Filters.Clear();

            var httpApplicationByConventions = new HttpApplicationByConventions(stubWindsorContainer);
            httpApplicationByConventions.Application_Start();

            IEnumerable<Filter> sessionFilters = from f in GlobalFilters.Filters
                                                 where f.Instance.GetType().Equals(typeof(NHibernateSessionPerRequestFilter))
                                                 select f;
            Assert.AreEqual(1, sessionFilters.Count());
        }

        [TestMethod]
        public void Application_End_DisposeTheContainer()
        {
            var windsorContainer = MockRepository.GenerateMock<IWindsorContainer>();

            var httpApplicationByConventions = new HttpApplicationByConventions(windsorContainer);
            httpApplicationByConventions.Application_End();

            windsorContainer.AssertWasCalled(m => m.Dispose());
        }
    }
}
