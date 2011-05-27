using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Standings.Infrastructure.Persistence;
using Standings.Infrastructure.Repositories;

namespace Standings.Infrastructure.Tests.Repositories
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void GetEnumerator_DelegateToQueryableSession()
        {
            var mockSession = MockRepository.GenerateMock<IQueryableSession<Object>>();

            var rep = new Repository<object>();
            rep.QueryableSession = mockSession;
            rep.GetEnumerator();

            mockSession.AssertWasCalled(m => m.GetEnumerator());
        }
    }
}
