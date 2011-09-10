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
    }
}
