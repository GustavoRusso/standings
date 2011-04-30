using Castle.MicroKernel;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Standings.Web.Installers;
using Standings.Web.Plumbing;
using Standings.Web.Tests.Helpers;

namespace Standings.Web.Tests.Installers
{
    [TestClass]
    public class PersistenceInstallerTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Install_AddFacilityOfTypePersistenceFacility()
        {
            var stubNHibConfiguration = NHibernateHelper.GenerateStubConfiguration(TestContext.TestDir);
            var persistenceInstaller = new PersistenceInstaller(stubNHibConfiguration);

            IWindsorContainer container = new WindsorContainer().Install(persistenceInstaller);

            IFacility[] facilities = container.Kernel.GetFacilities();
            Assert.AreEqual(1, facilities.Length);
            Assert.IsInstanceOfType(facilities[0], typeof(PersistenceFacility));
        }
    }
}
