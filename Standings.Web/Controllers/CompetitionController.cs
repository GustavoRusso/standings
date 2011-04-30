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
            return View(CompetitionRepository);
        }

        // GET: /Competition/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
            newCompetition.Description = formModel.Description;
            CompetitionRepository.Add(newCompetition);
            TempData["InformationMessage"] = "The competition was created correctly.";
            return RedirectToAction("Index");
        }

        // GET: /Competition/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: /Competition/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Competition/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Competition/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
