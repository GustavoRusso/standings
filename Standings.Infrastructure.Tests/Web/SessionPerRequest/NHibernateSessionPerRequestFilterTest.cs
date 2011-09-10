using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using Rhino.Mocks;
using Standings.Infrastructure.Web.SessionPerRequest;

namespace Standings.Infrastructure.Tests.Web.SessionPerRequest
{
    [TestClass]
    public class NHibernateSessionPerRequestFilterTest
    {
        private void SetCurrentSessionUsingTheDependencyResolver(ISession currentSession)
        {
            var stubDependencyResolver = MockRepository.GenerateMock<IDependencyResolver>();
            stubDependencyResolver.Stub(s => s.GetService(typeof(ISession))).Return(currentSession);
            System.Web.Mvc.DependencyResolver.SetResolver(stubDependencyResolver);
        }

        [TestMethod]
        public void OnActionExecuting_ResolveCurrentNHibernateSessionAndBeginNewTransaction()
        {
            var mockCurrentSession = MockRepository.GenerateMock<ISession>();
            SetCurrentSessionUsingTheDependencyResolver(mockCurrentSession);

            var sessionFilter = new NHibernateSessionPerRequestFilter();
            sessionFilter.OnActionExecuting(null);

            mockCurrentSession.AssertWasCalled(m=>m.BeginTransaction());
        }

        [TestMethod]
        public void OnResultExecuted_CommitTheCurrentTransaction()
        {
            var mockCurrentTransaction = MockRepository.GenerateMock<ITransaction>();
            var stubCurrentSession = MockRepository.GenerateMock<ISession>();
            stubCurrentSession.Stub(s => s.Transaction).Return(mockCurrentTransaction);
            SetCurrentSessionUsingTheDependencyResolver(stubCurrentSession);

            var sessionFilter = new NHibernateSessionPerRequestFilter();
            sessionFilter.OnResultExecuted(null);

            mockCurrentTransaction.AssertWasCalled(m => m.Commit());
        }

        [TestMethod]
        public void OnException_RollbackTheCurrentTransaction()
        {
            var mockCurrentTransaction = MockRepository.GenerateMock<ITransaction>();
            mockCurrentTransaction.Stub(s => s.IsActive).Return(true);

            var stubCurrentSession = MockRepository.GenerateMock<ISession>();
            stubCurrentSession.Stub(s => s.Transaction).Return(mockCurrentTransaction);
            SetCurrentSessionUsingTheDependencyResolver(stubCurrentSession);

            var sessionFilter = new NHibernateSessionPerRequestFilter();
            sessionFilter.OnException(null);

            mockCurrentTransaction.AssertWasCalled(m => m.Rollback());
        }

        [TestMethod]
        public void OnException_WhenThereIsNoActiveTransaction_DoesNotRollbackTheCurrentTransaction()
        {
            var mockCurrentTransaction = MockRepository.GenerateMock<ITransaction>();
            mockCurrentTransaction.Stub(s => s.IsActive).Return(false);

            var stubCurrentSession = MockRepository.GenerateMock<ISession>();
            stubCurrentSession.Stub(s => s.Transaction).Return(mockCurrentTransaction);
            SetCurrentSessionUsingTheDependencyResolver(stubCurrentSession);

            var sessionFilter = new NHibernateSessionPerRequestFilter();
            sessionFilter.OnException(null);

            mockCurrentTransaction.AssertWasNotCalled(m => m.Rollback());
        }
    }
}
