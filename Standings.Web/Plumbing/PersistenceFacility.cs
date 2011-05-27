using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using NHibernate;
using NHibernate.Cfg;
using Standings.Infrastructure.Persistence;

namespace Standings.Web.Plumbing
{
    /// <summary>
    /// http://docs.castleproject.org/Windsor.Windsor-Tutorial-Part-Six-Persistence-Layer.ashx
    /// </summary>
    public class PersistenceFacility: AbstractFacility
    {
        public Configuration NHibernateConfiguration { get; set; }

        public PersistenceFacility(Configuration nhibernateConfiguration)
        {
            NHibernateConfiguration = nhibernateConfiguration;
        }

        #region Overrides of AbstractFacility

        protected override void Init()
        {
            Kernel.Register(Component.For<ISessionFactory>().UsingFactoryMethod(NHibernateConfiguration.BuildSessionFactory).LifeStyle.Singleton);
            Kernel.Register(Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession()).LifeStyle.PerWebRequest);
            Kernel.Register(Component.For(typeof(IQueryableSession<>)).ImplementedBy(typeof(NHibernateQueriableSession<>)).LifeStyle.PerWebRequest);
        }

        #endregion
    }
}