using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core;
using Castle.Core.Internal;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Standings.Web.Controllers;
using Standings.Web.Installers;

namespace Standings.Web.Tests.Installers
{
    /// <summary>
    /// http://docs.castleproject.org/Windsor.Windsor-tutorial-part-three-a-testing-your-first-installer.ashx
    /// </summary>
    [TestClass]
    public class ControllersInstallerTest
    {
        [TestMethod]
        public void Install_OnlyRegisterComponentsOfTypeOfIControllers()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());

            var allHandlers = containerWithControllers.GetAllHandlers();
            Assert.AreNotEqual(0, allHandlers.Length);
            var controllerHandlers = containerWithControllers.GetHandlersFor(typeof(IController));
            CollectionAssert.AreEquivalent(allHandlers, controllerHandlers);
        }

        [TestMethod]
        public void Install_RegisterAllComponentsOfTypeOfIControllersInTheWebAssembly()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());

            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = containerWithControllers.GetImplementationTypesFor(typeof(IController));
            CollectionAssert.AreEquivalent(allControllers, registeredControllers);
        }

        [TestMethod]
        public void AllControllersAndOnlyControllersHaveTheControllerSuffixInItsName()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());

            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Name.EndsWith("Controller"));
            var registeredControllers = containerWithControllers.GetImplementationTypesFor(typeof(IController));
            CollectionAssert.AreEquivalent(allControllers, registeredControllers);
        }

        [TestMethod]
        public void AllControllersAreInTheControllerNamespace()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());

            var allControllers = GetPublicClassesFromApplicationAssembly(c => c.Namespace.Contains("Controllers"));
            var registeredControllers = containerWithControllers.GetImplementationTypesFor(typeof(IController));
            CollectionAssert.AreEquivalent(allControllers, registeredControllers);
        }

        [TestMethod]
        public void Install_RegisterComponentsWithTheSameNameThatItsTypes()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());

            var controllersWithWrongName = containerWithControllers.GetHandlersFor(typeof(IController))
                .Where(controller => controller.ComponentModel.Name != controller.ComponentModel.Implementation.Name)
                .ToArray();
            Assert.AreEqual(0, controllersWithWrongName.Length);
        }

        [TestMethod]
        public void Install_RegisterAllControllersAsTransient()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());

            var nonTransientControllers = containerWithControllers.GetHandlersFor(typeof(IController))
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();
            Assert.AreEqual(0, nonTransientControllers.Length);
        }

        [TestMethod]
        public void Install_AllControllersExposeThemselvesAsService()
        {
            IWindsorContainer containerWithControllers = new WindsorContainer().Install(new ControllersInstaller());

            var controllersWithWrongName = containerWithControllers.GetHandlersFor(typeof(IController))
                .Where(controller => controller.Service != controller.ComponentModel.Implementation)
                .ToArray();
            Assert.AreEqual(0, controllersWithWrongName.Length);
        }

        private Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return typeof(HomeController).Assembly.GetExportedTypes()
                .Where(t => t.IsClass)
                .Where(t => t.IsAbstract == false)
                .Where(where.Invoke)
                .OrderBy(t => t.Name)
                .ToArray();
        }
    }
}