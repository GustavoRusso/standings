using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Standings.Web.Controllers;

namespace Standings.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_WhenTheUserIsNotParticipatingInAnyCompetition_ShowMessageToSuggestJoinToCompetition()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewBag);
            Assert.AreEqual("You are not signed up to any competition. Surely there is something you feel like doing. What are you waiting to start?", result.ViewBag.StatusMessage);
        }

        [TestMethod]
        public void About()
        {
            var controller = new HomeController();

            var result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
