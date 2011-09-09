using System;
using System.Linq;
using System.Web.Mvc;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Standings.Infrastructure.Web.DependencyResolver;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace Standings.Infrastructure.Tests.Web.DependencyResolver
{
    [TestClass]
    public class WindsorDependencyResolverTest
    {
        [TestMethod]
        public void GetService_WhenTheServiceIsNotRegisteredInTheContainer_AskTheResolutionToTheDefaultMvcResolver()
        {
            var stubDependency = MockRepository.GenerateStub<IControllerFactory>();
            var mockDefaultResolver = MockRepository.GenerateMock<IDependencyResolver>();
            mockDefaultResolver.Stub(s => s.GetService(typeof (IControllerFactory))).Return(stubDependency);
            IKernel stubKernel = new WindsorContainer().Kernel;

            var resolver = new WindsorDependencyResolver(mockDefaultResolver, stubKernel);
            var resolvedDependency = resolver.GetService(typeof(IControllerFactory));

            mockDefaultResolver.AssertWasCalled(m => m.GetService(typeof(IControllerFactory)));
            Assert.AreEqual(stubDependency, resolvedDependency);
        }

        [TestMethod]
        public void GetService_WhenTheServiceIsRegisteredInTheContainer_ReturnTheService()
        {
            var stubDependency = MockRepository.GenerateStub<IControllerFactory>();
            var mockKernel = new WindsorContainer();
            mockKernel.Register(Component.For(typeof(IControllerFactory)).Instance(stubDependency));

            var resolver = new WindsorDependencyResolver(null, mockKernel.Kernel);
            var resolvedDependency = resolver.GetService(typeof(IControllerFactory));

            Assert.AreEqual(stubDependency, resolvedDependency);
        }

        [TestMethod]
        public void GetServices_WhenTheServicesIsNotRegisteredInContainerEitherInDefaultResolver_ReturnAnEmptyEnumerator()
        {
            var stubDefaultResolver = MockRepository.GenerateStub<IDependencyResolver>();
            stubDefaultResolver.Stub(s => s.GetServices(Arg<Type>.Is.Anything)).Return(new object[]{});
            var stubKernel = MockRepository.GenerateStub<IKernel>();
            stubKernel.Stub(s => s.ResolveAll(Arg<Type>.Is.Anything)).Return(new object[]{});

            var resolver = new WindsorDependencyResolver(stubDefaultResolver, stubKernel);
            var services = resolver.GetServices(typeof(IControllerFactory));

            Assert.AreEqual(0, services.Count());
        }

        [TestMethod]
        public void GetServices_WhenTheServicesIsRegisteredInTheContainer_ReturnTheService()
        {
            var stubDependency = MockRepository.GenerateStub<IControllerFactory>();
            var mockKernel = MockRepository.GenerateMock<IKernel>();
            mockKernel.Stub(s => s.ResolveAll(typeof(IControllerFactory))).Return(new[]{stubDependency});
            var stubDefaultResolver = MockRepository.GenerateStub<IDependencyResolver>();
            stubDefaultResolver.Stub(s => s.GetServices(Arg<Type>.Is.Anything)).Return(new object[]{});

            var resolver = new WindsorDependencyResolver(stubDefaultResolver, mockKernel);
            var services = resolver.GetServices(typeof(IControllerFactory));

            Assert.AreEqual(1, services.Count());
            Assert.IsTrue(services.Contains(stubDependency));
        }

        [TestMethod]
        public void GetServices_WhenTheServicesIsRegisteredInTheDefaultResolver_ReturnTheService()
        {
            var stubDependency = MockRepository.GenerateStub<IControllerFactory>();
            var stubDefaultResolver = MockRepository.GenerateStub<IDependencyResolver>();
            stubDefaultResolver.Stub(s => s.GetServices(typeof(IControllerFactory))).Return(new[]{stubDependency});
            var stubKernel = MockRepository.GenerateStub<IKernel>();
            stubKernel.Stub(s => s.ResolveAll(Arg<Type>.Is.Anything)).Return(new object[]{ });

            var resolver = new WindsorDependencyResolver(stubDefaultResolver, stubKernel);
            var services = resolver.GetServices(typeof(IControllerFactory));

            Assert.AreEqual(1, services.Count());
            Assert.IsTrue(services.Contains(stubDependency));
        }

        [TestMethod]
        public void GetServices_WhenTheServicesIsRegisteredInBothTheContainerAndTheDefaultResolver_ReturnServicesFromBothResolversWithoutDuplications()
        {
            var stubDependency = MockRepository.GenerateStub<IControllerFactory>();
            var mockDefaultResolver = MockRepository.GenerateMock<IDependencyResolver>();
            mockDefaultResolver.Stub(s => s.GetServices(typeof(IControllerFactory))).Return(new[]{stubDependency});
            var mockKernel = MockRepository.GenerateMock<IKernel>();
            mockKernel.Stub(s => s.ResolveAll(typeof(IControllerFactory))).Return(new []{stubDependency});

            var resolver = new WindsorDependencyResolver(mockDefaultResolver, mockKernel);
            var services = resolver.GetServices(typeof(IControllerFactory));

            Assert.AreEqual(1, services.Count());
            Assert.IsTrue(services.Contains(stubDependency));
        }
    }
}
