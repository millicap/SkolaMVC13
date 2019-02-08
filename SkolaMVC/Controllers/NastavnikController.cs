using SkolaMVC.DBModels;
using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class NastavnikController : Controller
    {
        // GET: Nastavnik
        public ActionResult Index()
        {
            using (var context = new SkolaContext())
            {
                var nastavnikList = context.Nastavniks.Select(n => new NastavnikViewModel
                {
                    NastavnikId = n.NastavnikId,
                    Ime = n.Ime,
                    Prezime = n.Prezime,
                    JMBG = n.JMBG,

                }).ToList();

                return View(nastavnikList);
            }
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NastavnikViewModel nastavnik)
        {
            using (var context = new SkolaContext())

            {
                context.Nastavniks.Add(new Nastavnik()
                {
                    NastavnikId = nastavnik.NastavnikId,
                    Ime = nastavnik.Ime,
                    Prezime = nastavnik.Prezime,
                    JMBG = nastavnik.JMBG
                });
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id)
        {
            int nastavnikId = Convert.ToInt32(id);

            using (var context = new SkolaContext())
            {
                Nastavnik nastavnik = context.Nastavniks.Where(n => n.NastavnikId == nastavnikId).FirstOrDefault();
                NastavnikViewModel nastavnikVM = new NastavnikViewModel()
                {
                    NastavnikId = nastavnik.NastavnikId,
                    Ime = nastavnik.Ime,
                    Prezime = nastavnik.Prezime,
                    JMBG = nastavnik.JMBG
                };

                return View(nastavnikVM);
            }
        }

        [HttpPost]
        public ActionResult Edit(NastavnikViewModel nastavnik)
        {
            using (var context = new SkolaContext())
            {
                Nastavnik na = context.Nastavniks.Find(nastavnik.NastavnikId);
                na.NastavnikId = nastavnik.NastavnikId;
                na.Ime = nastavnik.Ime;
                na.Prezime = nastavnik.Prezime;
                na.JMBG = nastavnik.JMBG;

                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(string id)
        {
            int nastavnikId = Convert.ToInt32(id);

            using (var context = new SkolaContext())
            {
                Nastavnik nastavnik = context.Nastavniks.Where(n => n.NastavnikId == nastavnikId).FirstOrDefault();
                NastavnikViewModel nastavnikVM = new NastavnikViewModel()
                {
                    NastavnikId = nastavnik.NastavnikId,
                    Ime = nastavnik.Ime,
                    Prezime = nastavnik.Prezime,
                    JMBG = nastavnik.JMBG
                };

                return View(nastavnikVM);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (var context = new SkolaContext())
            {

                context.Nastavniks.Remove(context.Nastavniks.Find(id));
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }

}