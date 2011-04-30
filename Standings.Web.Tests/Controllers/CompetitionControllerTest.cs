using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Standings.Domain;
using Standings.Infrastructure.Repositories;
using Standings.Web.Controllers;
using Standings.Web.Models.Competition;

namespace Standings.Web.Tests.Controllers
{
    [TestClass]
    public class CompetitionControllerTest
    {
        [TestMethod]
        public void Index_WhenThereAreNoCompetitions_ReturnAnEmptyListOfCompetitions()
        {
            var controller = new CompetitionController();
            controller.CompetitionRepository = new CompetitionRepository();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as IList<Competition>;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public void Index_WhenExistsAtLeastACompetition_ShowListOfExistentCompetitions()
        {
            var competition = new Competition();
            var compRep = new CompetitionRepository { competition };

            var controller = new CompetitionController();
            controller.CompetitionRepository = compRep;
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as IList<Competition>;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Contains(competition));
        }

        [TestMethod]
        public void CreateByPOST_WhenExecuteCorrectly_RedirectToIndex()
        {
            var controller = new CompetitionController();
            controller.CompetitionRepository = new CompetitionRepository();

            var actionResult = controller.Create(new CreateCompetitionModel());

            var redirectToRouteResult= actionResult as RedirectToRouteResult;
            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Index", redirectToRouteResult.RouteValues["action"]);
            Assert.AreEqual("The competition was created correctly.", controller.TempData["InformationMessage"]);
        }

        [TestMethod]
        public void CreateByPOST_WhenExecuteCorrectly_AddNewCompetitionToRepository()
        {
            var competitionRepository = new CompetitionRepository();
            var createCompetitionModel = new CreateCompetitionModel();
            createCompetitionModel.Description = "text as description";

            var controller = new CompetitionController();
            controller.CompetitionRepository = competitionRepository;
            controller.Create(createCompetitionModel);

            Assert.AreEqual(1, competitionRepository.Count);
            var compCreated = competitionRepository[0];
            Assert.AreEqual(createCompetitionModel.Description, compCreated.Description);
        }

    }
}
