using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Castle.Core.Internal;
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
            var infrastructureAssembly = typeof(CompetitionRepository).Assembly;
            Type[] allRepositories = infrastructureAssembly.GetExportedTypes()
               .Where(t => t.IsClass).Where(t => t.IsAbstract == false)
               .Where(t => t.Namespace.Contains(".Repositories"))
               .Where(t => t.Is<IList>())
               .OrderBy(t => t.Name)
               .ToArray();

            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new RepositoriesInstaller());

            Type[] allRegisteredImplementations = containerWithControllers.GetAllImplementation();
            CollectionAssert.AreEquivalent(allRepositories, allRegisteredImplementations);
        }

        [TestMethod]
        public void Install_RegisterAllRepositoriesAsSingleton()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new RepositoriesInstaller());

            var nonSingletonRepositories = containerWithControllers.GetAllHandlers()
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Singleton)
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
