using SkolaMVC.DBModels;
using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class RandomUcenikController : Controller
    {
        // GET: RandomUcenik
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetRandomUcenik()
        {
            using (var context = new SkolaContext())
            {
                var ucenici = context.Uceniks.Select(u => new UcenikViewModel
                {
                    UcenikId = u.UcenikId,
                    Ime = u.Ime,
                    Prezime = u.Prezime,
                    Pol = u.Pol,
                    JMBG = u.JMBG,
                    Adresa = u.Adresa,
                    DatumRodjenja = u.DatumRodjenja,
                    ImeRoditelja = u.ImeRoditelja,
                    BrojUDnevniku = u.BrojUDnevniku,
                    Drzavljanstvo = u.Drzavljanstvo,
                    OdjeljenjeId = u.OdjeljenjeId,
                    OdjeljenjeNaziv = u.Odjeljenje.Naziv,
                    GradNaziv = u.Grad.Naziv,
                    DrzavaNaziv = u.Grad.Drzava.Naziv

                }).ToList();

                Random rnd = new Random();
                int r = rnd.Next(0, ucenici.Count);

                return PartialView("_RandomUcenik", ucenici.ElementAt(r));  //vraca partial sa random ucenikom
            }
        }
    }
}