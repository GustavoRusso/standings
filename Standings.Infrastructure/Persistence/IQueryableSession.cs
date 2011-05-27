using System.Linq;
using NHibernate;

namespace Standings.Infrastructure.Persistence
{
    public interface IQueryableSession<T> : IQueryable<T>, ISession
    {
    }
}