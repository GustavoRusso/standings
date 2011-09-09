using System.Web.Mvc;
using NHibernate;

namespace Standings.Infrastructure.Web.SessionPerRequest
{
    /// <summary>
    /// This is a MVC GlobalFilter that will provide the Session Per Request implementation using NHibernate.
    /// Unfortunatly GlobalFilters.Filters store filters instances that are reused across requests.
    /// In order to provide a Global Filter Per Request - at the same time that it meant to be as simple as possible -
    /// the filter just use directly the DependencyResolver to retrieve the current NHibernate Session.
    /// </summary>
    public class NHibernateSessionPerRequestFilter: IActionFilter, IResultFilter , IExceptionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentSession = System.Web.Mvc.DependencyResolver.Current.GetService<ISession>();
            currentSession.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var currentSession = System.Web.Mvc.DependencyResolver.Current.GetService<ISession>();
            currentSession.Transaction.Commit();

            //var tx = Session.Transaction;
            //if (tx != null && tx.IsActive)
            //{
            //    var thereWereNoExceptions = filterContext.Exception == null || filterContext.ExceptionHandled;
            //    if (filterContext.Controller.ViewData.ModelState.IsValid && thereWereNoExceptions)
            //        Session.Transaction.Commit();
            //    else
            //        Session.Transaction.Rollback();
            //}
        }

        public void OnException(ExceptionContext filterContext)
        {
            var currentSession = System.Web.Mvc.DependencyResolver.Current.GetService<ISession>();
            if (currentSession.Transaction.IsActive)
                currentSession.Transaction.Rollback();
        }
    }
}
