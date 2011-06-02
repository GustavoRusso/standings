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
        public void GetItemByIndex_DelegateToQueryableSession()
        {
            var mockSession = MockRepository.GenerateMock<IQueryableSession<Object>>();
            const int entityId = 1;

            var rep = new Repository<object>();
            rep.QueryableSession = mockSession;
            var entity = rep[entityId];

            mockSession.AssertWasCalled(m => m.Load<object>(entityId));
        }

        [TestMethod]
        public void GetEnumerator_DelegateToQueryableSession()
        {
            var mockSession = MockRepository.GenerateMock<IQueryableSession<Object>>();

            var rep = new Repository<object>();
            rep.QueryableSession = mockSession;
            rep.GetEnumerator();

            mockSession.AssertWasCalled(m => m.GetEnumerator());
        }

        [TestMethod]
        public void RemoveAt_RetrieveTheEntityAtTheSpecifiedIndexAndRemoveIt()
        {
            var entity = new object();
            const int entityId = int.MaxValue;
            var mockSession = MockRepository.GenerateMock<IQueryableSession<Object>>();
            mockSession.Stub(s => s.Load<object>(entityId)).Return(entity);

            var rep = new Repository<object>();
            rep.QueryableSession = mockSession;
            rep.RemoveAt(entityId);

            mockSession.AssertWasCalled(m => m.Load<object>(entityId));
            mockSession.AssertWasCalled(m => m.Delete(entity));
        }
    }
}
