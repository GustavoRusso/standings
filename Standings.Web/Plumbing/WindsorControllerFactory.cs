using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace Standings.Web.Plumbing
{
    /// <summary>
    /// http://docs.castleproject.org/Windsor.Windsor-tutorial-part-two-plugging-Windsor-in.ashx
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        public IKernel Kernel { get; private set; }

        public WindsorControllerFactory(IKernel kernel)
        {
            Kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            Kernel.ReleaseComponent(controller);
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)Kernel.Resolve(controllerType);
        }
    }
}