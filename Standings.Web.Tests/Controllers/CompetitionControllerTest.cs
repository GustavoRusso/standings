using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Standings.Domain;
using Standings.Infrastructure.Repositories;
using Standings.Web.Controllers;
using Standings.Web.Models.Competition;
using Standings.Web.Tests.Helpers;

namespace Standings.Web.Tests.Controllers
{
    [TestClass]
    public class CompetitionControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new CompetitionController();
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            var controller = new CompetitionController();

            var result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Admin_WhenThereAreNoCompetitions_ReturnAnEmptyListOfCompetitions()
        {
            var controller = new CompetitionController();
            controller.CompetitionRepository = new CompetitionRepository{QueryableSession = new InMemoryQueryableSession<Competition>()};

            var result = controller.Admin() as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as IList<Competition>;
            Assert.IsNotNull(model);
            Assert.AreEqual(0, model.Count);
        }

        [TestMethod]
        public void Admin_WhenExistsAtLeastACompetition_ShowListOfExistentCompetitions()
        {
            var competition = new Competition();
            var compRep = new CompetitionRepository {QueryableSession = new InMemoryQueryableSession<Competition>()};
            compRep.Add(competition);

            var controller = new CompetitionController();
            controller.CompetitionRepository = compRep;
            var result = controller.Admin() as ViewResult;

            Assert.IsNotNull(result);
            var model = result.Model as IList<Competition>;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Contains(competition));
        }

        [TestMethod]
        public void CreateByPOST_WhenExecuteCorrectly_RedirectToDetailsOfTheNewCompetition()
        {
            var createCompetitionModel = new CreateCompetitionModel();
            createCompetitionModel.Name = "competitionName";

            var controller = new CompetitionController();
            controller.CompetitionRepository = new CompetitionRepository { QueryableSession = new InMemoryQueryableSession<Competition>() };
            var actionResult = controller.Create(createCompetitionModel);

            var redirectToRouteResult= actionResult as RedirectToRouteResult;
            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Details", redirectToRouteResult.RouteValues["action"]);
            Assert.AreEqual(createCompetitionModel.Name, redirectToRouteResult.RouteValues["id"]);
            Assert.AreEqual("The competition was successfully created. Now you can add participants and checkpoints to your competition.", controller.TempData["InformationMessage"]);
        }

        [TestMethod]
        public void CreateByPOST_WhenExecuteCorrectly_AddNewCompetitionToRepository()
        {
            var competitionRepository = new CompetitionRepository { QueryableSession = new InMemoryQueryableSession<Competition>() };
            var createCompetitionModel = new CreateCompetitionModel();
            createCompetitionModel.Name = "name";

            var controller = new CompetitionController();
            controller.CompetitionRepository = competitionRepository;
            controller.Create(createCompetitionModel);

            Assert.AreEqual(1, competitionRepository.Count);
            var compCreated = competitionRepository[0];
            Assert.AreEqual(createCompetitionModel.Name, compCreated.Name);
        }

        [TestMethod]
        public void CreateByPOST_WhenDoesNotReceiveANameForTheCompetition_AddNewCompetitionToRepositoryWithARandomName()
        {
            var competitionRepository = new CompetitionRepository { QueryableSession = new InMemoryQueryableSession<Competition>() };

            var controller = new CompetitionController();
            controller.CompetitionRepository = competitionRepository;
            controller.Create(new CreateCompetitionModel());

            Assert.AreEqual(1, competitionRepository.Count);
            var compCreated = competitionRepository[0];
            Assert.IsNotNull(compCreated.Name);
        }

        [TestMethod]
        public void Delete_LoadTheCompetitionOnTheViewModel()
        {
            var competition = new Competition();
            var competitionRepository = new CompetitionRepository { QueryableSession = new InMemoryQueryableSession<Competition>() };
            competitionRepository.Add(competition);

            var controller = new CompetitionController();
            controller.CompetitionRepository = competitionRepository;
            var actionResult = controller.Delete(competition.Id);

            var result = actionResult as ViewResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(competition, result.Model);
        }

        [TestMethod]
        public void DeleteByPOST_WhenExecuteCorrectly_RedirectToIndex()
        {
            var competition = new Competition();
            var competitionRepository = new CompetitionRepository { QueryableSession = new InMemoryQueryableSession<Competition>() };
            competitionRepository.Add(competition);

            var controller = new CompetitionController();
            controller.CompetitionRepository = competitionRepository;
            var actionResult = controller.Delete(competition.Id, new FormCollection());

            var redirectToRouteResult = actionResult as RedirectToRouteResult;
            Assert.IsNotNull(redirectToRouteResult);
            Assert.AreEqual("Admin", redirectToRouteResult.RouteValues["action"]);
            Assert.AreEqual("The competition was deleted.", controller.TempData["InformationMessage"]);
        }

        [TestMethod]
        public void DeleteByPOST_WhenExecuteCorrectly_RemoveTheCompetitionFromTheRepository()
        {
            var competition = new Competition();
            var competitionRepository = new CompetitionRepository { QueryableSession = new InMemoryQueryableSession<Competition>() };
            competitionRepository.Add(competition);

            var controller = new CompetitionController();
            controller.CompetitionRepository = competitionRepository;
            controller.Delete(competition.Id, new FormCollection());

            Assert.IsFalse(competitionRepository.Contains(competition),"The competition should have been removed from the repository");
        }

        [TestMethod]
        public void Details_LoadTheCompetitionOnTheViewModel()
        {
            var competition = new Competition();
            competition.Name = "testingCompetition";
            var competitionRepository = new CompetitionRepository { QueryableSession = new InMemoryQueryableSession<Competition>() };
            competitionRepository.Add(competition);

            var controller = new CompetitionController();
            controller.CompetitionRepository = competitionRepository;
            var actionResult = controller.Details(competition.Name);

            var result = actionResult as ViewResult;
            Assert.AreEqual(competition, result.Model);
        }

    }
}
