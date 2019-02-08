using SkolaMVC.DBModels;
using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class UcenikPredmetiController : Controller
    {
        // GET: UcenikPredmeti
        public ActionResult Index()
        {
            using (var context = new SkolaContext())
            {
                var ucenici = context.Uceniks.Select(u => new UcenikViewModel()
                {
                    UcenikId = u.UcenikId,
                    Ime = u.Ime,
                    Prezime = u.Prezime,
                    JMBG = u.JMBG,
                    OdjeljenjeNaziv =u.Odjeljenje.Naziv
                }).ToList();
                return View(ucenici);
            }
                
        }

        public ActionResult Details(string id)
        {
            int ucenikId = Convert.ToInt32(id);
            using (var context = new SkolaContext())
            {
                UcenikPredmetViewModel ucenikPredmetVM = context.Uceniks.Where(u => u.UcenikId == ucenikId).Select(u => new UcenikPredmetViewModel()
                {
                    UcenikId = u.UcenikId,
                    Ime = u.Ime,
                    Prezime = u.Prezime,
                    Predmeti = u.Ocjenas.Select(o => new PredmetViewModel()
                    {
                        PredmetId = o.Predmet.PredmetId,
                        Naziv = o.Predmet.Naziv

                    }).Distinct().ToList()

                }).FirstOrDefault();

                return View(ucenikPredmetVM);
            }
        }

        public ActionResult GetOcjene(string ucenikId, string predmetId)
        {
            int UcenikId = Convert.ToInt32(ucenikId);
            int PredmetId = Convert.ToInt32(predmetId);

            using (var context = new SkolaContext())
            {
                var ucenik = context.Uceniks.Find(UcenikId);
                var ocjene = ucenik.Ocjenas.Where(o => o.Predmet.PredmetId == PredmetId).Select(o => new OcjenaPredmetViewModel()
                {
                    OcjenaId =o.OcjenaId,
                    DatumOcjene = o.Datum,
                    Vrijednost = o.VrijednostOcjene,
                    TipOcjene = (TipOcjene)o.TipOcjene

                }).ToList();
                return PartialView("_OcjenePredmet", ocjene);
            }         
        }
    }



}