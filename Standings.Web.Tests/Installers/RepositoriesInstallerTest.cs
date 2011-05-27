using System;
using System.Linq;
using Castle.Core;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Standings.Infrastructure.Repositories;
using Standings.Web.Installers;

namespace Standings.Web.Tests.Installers
{
    [TestClass]
    public class RepositoriesInstallerTest
    {
        [TestMethod]
        public void Install_RegisterAllRepositoryInTheInfrastructureAssembly()
        {
            Type genericRepositoryType = typeof(Repository<>).GetGenericTypeDefinition();
            var infrastructureAssembly = typeof(Repository<>).Assembly;
            Type[] allRepositories = infrastructureAssembly.GetExportedTypes()
               .Where(t => t.IsClass).Where(t => t.IsAbstract == false)
               .Where(t => t.Namespace.Contains(".Repositories"))
               .Where(t => (t.IsGenericType && t.GetGenericTypeDefinition() == genericRepositoryType) ||
                    (t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == genericRepositoryType)
               )
               .OrderBy(t => t.Name)
               .ToArray();
            
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new RepositoriesInstaller());

            Type[] allRegisteredImplementations = containerWithControllers.GetAllImplementation();
            CollectionAssert.AreEquivalent(allRepositories, allRegisteredImplementations);
        }

        [TestMethod]
        public void Install_RegisterAllRepositoriesWithLifestyleTypePerWebRequest()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new RepositoriesInstaller());

            var nonSingletonRepositories = containerWithControllers.GetAllHandlers()
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.PerWebRequest)
                .ToArray();
            Assert.AreEqual(0, nonSingletonRepositories.Length);
        }

        [TestMethod]
        public void Install_RegisterComponentsWithTheSameNameThatItsTypes()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new RepositoriesInstaller());

            var componentsWithWrongName = containerWithControllers.GetAllHandlers()
                .Where(controller => controller.ComponentModel.Name != controller.ComponentModel.Implementation.Name)
                .ToArray();
            Assert.AreEqual(0, componentsWithWrongName.Length);
        }
    }
}
