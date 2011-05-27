using System;
using System.IO;
using System.Web.Mvc;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Standings.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Admin/
        public ActionResult Index()
        {
            var se = new SchemaExport((new Configuration()).Configure());
            se.SetDelimiter(";\n");
            TextWriter oldOut = Console.Out;
            var creationSchema = new StringWriter();
            Console.SetOut(creationSchema);
            se.Create(true, false);
            Console.SetOut(oldOut);
            ViewBag.SchemaExport = creationSchema.ToString();
            return View();
        }

        // GET: /Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: /Admin/Create
        public ActionResult Create()
        {
            return View();
        } 

        // POST: /Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        // GET: /Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: /Admin/Edit/5
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

        // GET: /Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: /Admin/Delete/5
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
