using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Standings.Infrastructure.Repositories;

namespace Standings.Web.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromAssemblyContaining(typeof(CompetitionRepository))
                                   .Pick().If(Component.IsInSameNamespaceAs<CompetitionRepository>())
                                   .Configure(c => c.Named(c.ServiceType.Name).LifeStyle.PerWebRequest)
                                   );
        }
    }
}