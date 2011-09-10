using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Standings.Web.Controllers;

namespace Standings.Web.Installers
{
    /// <summary>
    /// http://docs.castleproject.org/Windsor.Windsor-tutorial-part-three-writing-your-first-installer.ashx
    /// </summary>
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(FindControllers().Configure(ConfigureControllers()));
        }

        private ConfigureDelegate ConfigureControllers()
        {
            return (c => c.Named(c.ServiceType.Name).LifeStyle.Transient);
        }

        private BasedOnDescriptor FindControllers()
        {
            return AllTypes.FromThisAssembly()
                .BasedOn<IController>()
                .If(Component.IsInSameNamespaceAs<CompetitionController>())
                .If(t => t.Name.EndsWith("Controller"));
        }
    }
}