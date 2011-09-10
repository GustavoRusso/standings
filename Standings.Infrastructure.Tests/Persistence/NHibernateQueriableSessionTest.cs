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

        [TestMethod]
        public void Load_DelegatesToISession()
        {
            var mockSession = MockRepository.GenerateMock<ISession>();
            const int entityId = 1;

            var queriableSession = new NHibernateQueriableSession<Object>();
            queriableSession.Session = mockSession;
            queriableSession.Load<object>(entityId);

            mockSession.AssertWasCalled(m => m.Load<object>(entityId));
        }

        [TestMethod]
        public void Delete_DelegatesToISession()
        {
            var mockSession = MockRepository.GenerateMock<ISession>();
            var entity = new object();

            var queriableSession = new NHibernateQueriableSession<Object>();
            queriableSession.Session = mockSession;
            queriableSession.Delete(entity);

            mockSession.AssertWasCalled(m => m.Delete(entity));
        }

        [TestMethod]
        public void Provider_ReturnANotNullProvider()
        {
            var stubSession = MockRepository.GenerateMock<ISession>();

            var queriableSession = new NHibernateQueriableSession<Object>();
            queriableSession.Session = stubSession;
            var provider = queriableSession.Provider;
            
            Assert.IsNotNull(provider);
        }

        [TestMethod]
        public void Expression_ReturnANotNullExpression()
        {
            var stubSession = MockRepository.GenerateMock<ISession>();

            var queriableSession = new NHibernateQueriableSession<Object>();
            queriableSession.Session = stubSession;
            var expression = queriableSession.Expression;

            Assert.IsNotNull(expression);
        }
    }
}
