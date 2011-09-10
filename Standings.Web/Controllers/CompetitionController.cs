using System;
using System.Linq;
using System.Web.Mvc;
using Standings.Domain;
using Standings.Infrastructure.Repositories;
using Standings.Web.Models.Competition;

namespace Standings.Web.Controllers
{
    public class CompetitionController : Controller
    {
        public CompetitionRepository CompetitionRepository { get; set; }

        // GET: /Competition/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        // GET: /Competition/Admin
        public ActionResult Admin()
        {
            return View(CompetitionRepository);
        }

        // GET: /Competition/Details/5
        public ActionResult Details(string id)
        {
            var competition = (from c in CompetitionRepository
                              where c.Name == id
                              select c).Single();
            return View(competition);
        }

        // GET: /Competition/Create
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /Competition/Create
        [HttpPost]
        public ActionResult Create(CreateCompetitionModel formModel)
        {
            var newCompetition = new Competition();
            newCompetition.Name = formModel.Name;

            if(string.IsNullOrEmpty(newCompetition.Name))
            {
                var randomKey = Guid.NewGuid();
                newCompetition.Name = randomKey.ToString();
            }

            CompetitionRepository.Add(newCompetition);
            TempData["InformationMessage"] = "The competition was successfully created.";
            return RedirectToAction("Admin");
        }

        // GET: /Competition/Delete/5
        public ActionResult Delete(int id)
        {
            return View(CompetitionRepository[id]);
        }

        // POST: /Competition/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            CompetitionRepository.RemoveAt(id);
            TempData["InformationMessage"] = "The competition was deleted.";
            return RedirectToAction("Admin");
        }

    }
}
