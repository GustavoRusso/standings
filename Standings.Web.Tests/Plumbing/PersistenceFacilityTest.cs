using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using Standings.Web.Plumbing;
using Standings.Web.Tests.Helpers;
using Standings.Web.Tests.Installers;

namespace Standings.Web.Tests.Plumbing
{
    [TestClass]
    public class PersistenceFacilityTest
    {
        [TestMethod]
        public void Init_RegisterComponentForISessionFactoryAsSingleton()
        {
            IWindsorContainer container = new WindsorContainer();
            var stubNHibCong = NHibernateHelper.GenerateStubConfiguration();

            var persistenceFacility = new PersistenceFacility(stubNHibCong);
            var facility = persistenceFacility as IFacility;
            facility.Init(container.Kernel,null);

            var registeredComponents = container.GetImplementationTypesFor(typeof(ISessionFactory));
            Assert.AreEqual(1, registeredComponents.Length);
            var sessionHandler = container.GetHandlersFor(typeof(ISessionFactory)).First();
            Assert.AreEqual(LifestyleType.Singleton, sessionHandler.ComponentModel.LifestyleType);
        }

        [TestMethod]
        public void Init_RegisterComponentForISessionWithLifeStylePerWebRequest()
        {
            IWindsorContainer container = new WindsorContainer();
            var stubNHibCong = NHibernateHelper.GenerateStubConfiguration();

            var persistenceFacility = new PersistenceFacility(stubNHibCong);
            var facility = persistenceFacility as IFacility;
            facility.Init(container.Kernel, null);

            var registeredComponents = container.GetImplementationTypesFor(typeof(ISession));
            Assert.AreEqual(1, registeredComponents.Length);
            var sessionHandler = container.GetHandlersFor(typeof(ISession)).First();
            Assert.AreEqual(LifestyleType.PerWebRequest, sessionHandler.ComponentModel.LifestyleType);
        }

    }
}
