using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class PogadjanjeBrojaController : Controller
    {
        // GET: PogadjanjeBroja
        public ActionResult Index()
        {
            Random random = new Random();

            int RandomBroj = random.Next(1, 101);
            
            return View();
        }
    }
}