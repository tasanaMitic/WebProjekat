using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebProjekat.Controllers
{
    public class DispecerController : Controller
    {
        // GET: Dispecer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VoznjaNapravi()
        {
            return View("VoznjaNapravi");
        }
    }
}