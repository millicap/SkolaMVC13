using SkolaMVC.DBModels;
using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class DrzavaController : Controller
    {
        // GET: Drzava
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            using (var context = new SkolaContext())
            {
                var drzave = context.Drzavas.Select(d => new DrzavaViewModel()
                {
                    Drzavaid = d.DrzavaId,
                    Naziv = d.Naziv
                }).ToList();

                return PartialView("_SveDrzave", drzave);
            }
        }

        [HttpPost]
        public ActionResult Create(string naziv)
        {
            using (var context = new SkolaContext())
            {
                bool proslo = true;
                string poruka = "";

                if (naziv.Trim() == "")
                {
                    proslo = false;
                    poruka = "GRESKA: Unesite naziv!";
                }
                else if(naziv.Length> 50)
                {
                    proslo = false;
                    poruka = "GRESKA: Naziv drzave je predugacak!";
                }
                else if (!Regex.IsMatch(naziv, @"^[a-zA-Z]+$"))
                {
                    proslo = false;
                    poruka = "GRESKA: Naziv drzave ne moze da sadrzi brojeve!";
                }
                if (proslo)
                {
                    Drzava drzava = new Drzava()
                    {
                        Naziv = naziv
                    };
                    context.Drzavas.Add(drzava);
                    context.SaveChanges();
                }

                return new JsonResult() { Data = new {Success= proslo, Message= poruka }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }
    }
}