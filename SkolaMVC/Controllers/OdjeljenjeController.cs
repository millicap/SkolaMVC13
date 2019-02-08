using SkolaMVC.DBModels;
using SkolaMVC.HelperClass;
using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class OdjeljenjeController : Controller
    {
        List<OdjeljenjeViewModel> Odjeljenja = new List<OdjeljenjeViewModel>();
        // GET: Odjeljenje
        public ActionResult Index()
        {

            using (var context = new SkolaContext())
            {
                var odjeljenje = context.Odjeljenjes.Select(o => new OdjeljenjeViewModel()

                {
                    OdjeljenjeId = o.OdjeljenjeId,
                    Razrednik = o.RazrednikId,
                    Razred = o.Razred,
                    Naziv = o.Naziv,
                    SkolskaGodina = o.SkolskaGodina,
                    Ime = o.Nastavnik.Ime,
                    Prezime = o.Nastavnik.Prezime

                }).ToList();
                return View(odjeljenje);
            }
                
        }

        public ActionResult Create()
        {
            using (var context = new SkolaContext())
            {
                ViewBag.Razrednik = context.Nastavniks.Select(n => new SelectListItem()
                {
                    Text = n.Ime + " " + n.Prezime,
                    Value = "" + n.NastavnikId

                }).ToList();

                return View();
            }
                
        }

        [HttpPost]
        public ActionResult Create(OdjeljenjeViewModel odjeljenje)
        {
            /* Odjeljenja = DataHelper.LoadOdjeljenje();
             odjeljenje.OdjeljenjeId = Odjeljenja.GetNextOdjeljenjeId();
             Odjeljenja.Add(odjeljenje);
             Odjeljenja.SaveOdjeljenje();
             return RedirectToAction("Index");*/

            using (var context = new SkolaContext())
            {
                Odjeljenje odjelj = new Odjeljenje()
                {
                    OdjeljenjeId = odjeljenje.OdjeljenjeId,
                    RazrednikId = odjeljenje.Razrednik,
                    Razred = odjeljenje.Razred,
                    Naziv = odjeljenje.Naziv,
                    SkolskaGodina = odjeljenje.SkolskaGodina
                };
                context.Odjeljenjes.Add(odjelj);
                context.SaveChanges();
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            /*int odjeljeneID = Convert.ToInt32(id);
            Odjeljenja = DataHelper.LoadOdjeljenje();
            OdjeljenjeViewModel odjeljenje = Odjeljenja.Where(o => o.OdjeljenjeId == odjeljeneID).FirstOrDefault();
            return View(odjeljenje);*/

            using (var context = new SkolaContext())
            {
                int odjeljeneID = Convert.ToInt32(id);
                Odjeljenje odjelj = context.Odjeljenjes.Find(odjeljeneID);

                OdjeljenjeViewModel odjeljenjeVM = new OdjeljenjeViewModel()
                {
                    OdjeljenjeId = odjelj.OdjeljenjeId,
                    Razrednik = odjelj.RazrednikId,
                    Razred = odjelj.Razred,
                    Naziv = odjelj.Naziv,
                    SkolskaGodina = odjelj.SkolskaGodina                
                };

                return View(odjeljenjeVM);
            }
        }

        [HttpPost]
        public ActionResult Edit(OdjeljenjeViewModel odjeljenje)
        {
            /*Odjeljenja = DataHelper.LoadOdjeljenje();
            for (int i = 0; i < Odjeljenja.Count; i++)
            {
                if (Odjeljenja.ElementAt(i).OdjeljenjeId == odjeljenje.OdjeljenjeId)
                {
                    Odjeljenja[i] = odjeljenje;
                    break;
                }
            }
            Odjeljenja.SaveOdjeljenje();
            return RedirectToAction("Index");*/

            using (var context = new SkolaContext())
            {
                Odjeljenje odjelj = context.Odjeljenjes.Find(odjeljenje.OdjeljenjeId);
                odjelj.OdjeljenjeId = odjeljenje.OdjeljenjeId;
                odjelj.RazrednikId = odjeljenje.Razrednik;
                odjelj.Razred = odjeljenje.Razred;
                odjelj.Naziv = odjeljenje.Naziv;
                odjelj.SkolskaGodina = odjeljenje.SkolskaGodina;

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            /*int odjeljeneID = Convert.ToInt32(id);
            Odjeljenja = DataHelper.LoadOdjeljenje();
            OdjeljenjeViewModel odjeljenje = Odjeljenja.Where(o => o.OdjeljenjeId == odjeljeneID).FirstOrDefault();
            return View(odjeljenje);*/

            using (var context = new SkolaContext())
            {
                int odjeljeneID = Convert.ToInt32(id);
                Odjeljenje odjelj = context.Odjeljenjes.Find(odjeljeneID);

                OdjeljenjeViewModel odjeljenjeVM = new OdjeljenjeViewModel()
                {
                    OdjeljenjeId = odjelj.OdjeljenjeId,
                    Razrednik = odjelj.RazrednikId,
                    Razred = odjelj.Razred,
                    Naziv = odjelj.Naziv,
                    SkolskaGodina = odjelj.SkolskaGodina
                };

                return View(odjeljenjeVM);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            /*Odjeljenja = DataHelper.LoadOdjeljenje();
            for (int i = 0; i < Odjeljenja.Count; i++)
            {
                if (Odjeljenja.ElementAt(i).OdjeljenjeId == id)
                {
                    Odjeljenja.RemoveAt(i);
                    break;
                }
            }
            Odjeljenja.SaveOdjeljenje();
            return RedirectToAction("Index");*/

            using (var context = new SkolaContext())
            {
                if (context.Odjeljenjes.Find(id).Uceniks.Count == 0)    //ukoliko to odjeljenje nema ucenike
                {
                    context.Odjeljenjes.Remove(context.Odjeljenjes.Find(id));   //obrisi
                    context.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Greska");
                    Odjeljenje odjelj = context.Odjeljenjes.Find(id);
                    OdjeljenjeViewModel odjeljenjeVM = new OdjeljenjeViewModel()
                    {
                        OdjeljenjeId = odjelj.OdjeljenjeId,
                        Razrednik = odjelj.RazrednikId,
                        Razred = odjelj.Razred,
                        Naziv = odjelj.Naziv,
                        SkolskaGodina = odjelj.SkolskaGodina
                    };
                    return View(odjeljenjeVM);
                }
            }
            return RedirectToAction("Index");

        }




    }
}