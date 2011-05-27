using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using Rhino.Mocks;
using Standings.Infrastructure.Persistence;

namespace Standings.Infrastructure.Tests.Persistence
{
    [TestClass]
    public class NHibernateQueriableSessionTest
    {
        [TestMethod]
        public void Dispose_DelegatesToISession()
        {
            var mockSession = MockRepository.GenerateMock<ISession>();

            var queriableSession = new NHibernateQueriableSession<Object>();
            queriableSession.Session = mockSession;
            queriableSession.Dispose();

            mockSession.AssertWasCalled(m => m.Dispose());
        }

        [TestMethod]
        public void Save_DelegatesToISession()
        {
            var mockSession = MockRepository.GenerateMock<ISession>();
            var entity = new object();

            var queriableSession = new NHibernateQueriableSession<Object>();
            queriableSession.Session = mockSession;
            queriableSession.Save(entity);

            mockSession.AssertWasCalled(m => m.Save(entity));
        }
    }
}
