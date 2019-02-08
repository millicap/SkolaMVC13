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
    public class OcjenaController : Controller
    {
        List<OcjenaViewModel> Ocjene = new List<OcjenaViewModel>();
        // GET: Ocjena
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(int ucenikID)
        {
            using (var context = new SkolaContext())
            {
                OcjenaViewModel ocjena = new OcjenaViewModel();
                ocjena.UcenikId = ucenikID;

                ViewBag.Nastavnici = context.Nastavniks.Select(n => new SelectListItem()

                {
                    Text = n.Ime + " " + n.Prezime,
                    Value = "" + n.NastavnikId
                }).ToList();

                ViewBag.Predmeti = context.Predmets.Select(p => new SelectListItem()

                {
                    Text = p.Naziv,
                    Value = "" + p.PredmetId
                }).ToList();

                return View(ocjena);
            }  
        }

        [HttpPost]
        public ActionResult Create(OcjenaViewModel ocjena)
        {
            /*Ocjene = DataHelper.LoadOcjene();
            ocjena.OcjenaId = Ocjene.GetNextOcjenaId();
            Ocjene.Add(ocjena);
            Ocjene.SaveOcjena();
            return RedirectToAction("Details", "Ucenik", new {id = ocjena.UcenikId});*/


            using (var context = new SkolaContext())
            {
                context.Ocjenas.Add(new Ocjena()
                {
                    OcjenaId = ocjena.OcjenaId,
                    UcenikId = ocjena.UcenikId,
                    Datum = ocjena.Datum,
                    VrijednostOcjene = ocjena.Vrijednost,
                    TipOcjene = (int)ocjena.TipOcjene,
                    PredmetId = ocjena.Predmet,
                    NastavnikId = ocjena.Nastavnik 

                });
                context.SaveChanges();
            }
            return RedirectToAction("Details", "Ucenik", new { id = ocjena.UcenikId });
        }


        public ActionResult Edit(string id)
        {
            /* Ocjene = DataHelper.LoadOcjene();
             int ocjenaId = Convert.ToInt32(id);
             OcjenaViewModel ocjena = Ocjene.Where(o => o.OcjenaId == ocjenaId).FirstOrDefault();
             return View(ocjena);*/
            using (var context = new SkolaContext())
            {
                int ocjenaId = Convert.ToInt32(id);
                Ocjena ocjena = context.Ocjenas.Find(ocjenaId);
                OcjenaViewModel ocjenaViewModel = new OcjenaViewModel()
                {
                    OcjenaId = ocjena.OcjenaId,
                    UcenikId = ocjena.UcenikId,
                    Datum = ocjena.Datum,
                    Vrijednost = ocjena.VrijednostOcjene,
                    TipOcjene = (TipOcjene)ocjena.TipOcjene,
                    Predmet = ocjena.PredmetId,
                    Nastavnik = ocjena.NastavnikId

                };

                ViewBag.Nastavnici = context.Nastavniks.Select(n => new SelectListItem()

                {
                    Text = n.Ime + " " + n.Prezime,
                    Value = "" + n.NastavnikId
                }).ToList();

                ViewBag.Predmeti = context.Predmets.Select(p => new SelectListItem()

                {
                    Text = p.Naziv,
                    Value = "" + p.PredmetId
                }).ToList();
                return View(ocjenaViewModel);
            }   

        }

        [HttpPost]
        public ActionResult Edit(OcjenaViewModel ocjenaVM)
        {
            /*Ocjene = DataHelper.LoadOcjene();
            for (int i = 0; i < Ocjene.Count; i++)
            {
                if (Ocjene.ElementAt(i).OcjenaId == ocjena.OcjenaId)
                {
                    Ocjene[i] = ocjena;
                    break;
                }
            }
            Ocjene.SaveOcjena();
            return RedirectToAction("Details", "Ucenik", new { id = ocjena.UcenikId });*/


            using (var context = new SkolaContext())
            {
                Ocjena ocjena = context.Ocjenas.Find(ocjenaVM.OcjenaId);
                ocjena.OcjenaId = ocjenaVM.OcjenaId;
                ocjena.UcenikId = ocjenaVM.UcenikId;
                ocjena.Datum = ocjenaVM.Datum;
                ocjena.VrijednostOcjene = ocjenaVM.Vrijednost;
                ocjena.TipOcjene = (int)ocjenaVM.TipOcjene;
                ocjena.PredmetId = ocjenaVM.Predmet;
                ocjena.NastavnikId = ocjenaVM.Nastavnik;

                context.SaveChanges();
            };
            
            return RedirectToAction("Details", "Ucenik", new { id = ocjenaVM.UcenikId });
        }


        public ActionResult Delete(string id)
        {
            /*Ocjene = DataHelper.LoadOcjene();
            int ocjenaId = Convert.ToInt32(id);
            OcjenaViewModel ocjena = Ocjene.Where(o => o.OcjenaId == ocjenaId).FirstOrDefault();
            return View(ocjena);*/

            using (var context = new SkolaContext())
            {
                int ocjenaId = Convert.ToInt32(id);
                Ocjena ocjena = context.Ocjenas.Find(ocjenaId);
                OcjenaViewModel ocjenaViewModel = new OcjenaViewModel()
                {
                    OcjenaId = ocjena.OcjenaId,
                    UcenikId = ocjena.UcenikId,
                    Datum = ocjena.Datum,
                    Vrijednost = ocjena.VrijednostOcjene,
                    TipOcjene = (TipOcjene)ocjena.TipOcjene,
                    Predmet = ocjena.PredmetId,
                    Nastavnik = ocjena.NastavnikId

                };
                return View(ocjenaViewModel);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            /* Ocjene = DataHelper.LoadOcjene();
             int ucenikId = 0;
             for (int i = 0; i < Ocjene.Count; i++)
             {
                 if (Ocjene.ElementAt(i).OcjenaId == id)
                 {
                     ucenikId = Ocjene.ElementAt(i).UcenikId;
                     Ocjene.RemoveAt(i);
                     break;
                 }
             }        
             Ocjene.SaveOcjena();
             return RedirectToAction("Details", "Ucenik", new { id = ucenikId });*/

            int ucenikId = 0;

            using (var context = new SkolaContext())
            {
                ucenikId = context.Ocjenas.Find(id).UcenikId;
                context.Ocjenas.Remove(context.Ocjenas.Find(id));
                context.SaveChanges();
            }
            return RedirectToAction("Details", "Ucenik", new {id = ucenikId });
        }


    }
}