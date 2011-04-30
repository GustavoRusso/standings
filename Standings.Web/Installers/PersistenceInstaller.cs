using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate.Cfg;
using Standings.Web.Plumbing;

namespace Standings.Web.Installers
{
    /// <summary>
    /// http://docs.castleproject.org/Windsor.Windsor-Tutorial-Part-Six-Persistence-Layer.ashx
    /// </summary>
    public class PersistenceInstaller : IWindsorInstaller
    {
        public Configuration Configuration { get; set; }

        public PersistenceInstaller()
        {
            Configuration = (new Configuration()).Configure();
        }

        public PersistenceInstaller(Configuration configuration)
        {
            Configuration = configuration;
        }

        #region Implementation of IWindsorInstaller

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddFacility("PersistenceFacility", new PersistenceFacility(Configuration));
        }

        #endregion
    }
}