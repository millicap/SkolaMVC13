
using SkolaMVC.DBModels;
using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class StatistikaController : Controller
    {
        // GET: Statistika
        public ActionResult Index()
        {
            using (var context = new SkolaContext())
            {
                StatistikaViewModel statistikaVM = new StatistikaViewModel();
                statistikaVM.BrojUcenika = context.Uceniks.Count();

                statistikaVM.Top3Odlicnih = context.Uceniks.ToList().OrderByDescending(u => VratiProsjek(u.Ocjenas.ToList())).Select(u => (u.Ime +
                 " " + u.Prezime )).Take(3).ToList();

                statistikaVM.BrojOdlicnih = brojUcenika(context.Uceniks.ToList(), 5);
                statistikaVM.BrojVrloDobrih = brojUcenika(context.Uceniks.ToList(), 4);
                statistikaVM.BrojDobrih = brojUcenika(context.Uceniks.ToList(), 3);
                statistikaVM.BrojDovoljnih= brojUcenika(context.Uceniks.ToList(), 2);
                statistikaVM.BrojNedovoljnih = brojUcenika(context.Uceniks.ToList(), 1);

                statistikaVM.BrojOdlicnihPoOdjeljenjima = context.Odjeljenjes.ToList().Select(o => new BrojOdlicnihOdjeljenjeViewModel()
                {
                    BrojOdlicnih = brojUcenika(o.Uceniks.ToList(), 5),
                    OdjeljenjeId = o.OdjeljenjeId
                }).ToList().Where(a => a.BrojOdlicnih > 0).ToList();

                statistikaVM.BrojMuskih = context.Uceniks.Where(u => u.Pol == "Musko").ToList().Count;
                statistikaVM.BrojZenskih = context.Uceniks.Where(u => u.Pol == "Zensko").ToList().Count;

                var odjeljenja = context.Odjeljenjes.ToList();

                statistikaVM.NajboljeOdjeljenjePoProsjeku = odjeljenja.Select(o => new NajboljeOdjeljenjePoProsjekuViewModel()
                {
                    OdjeljenjeId = o.OdjeljenjeId,
                    Prosjek = Math.Round(vratiProsjekOdjeljenja(o.Uceniks.ToList()), 2),
                    Naziv = o.Naziv

                }).ToList().OrderByDescending(o => o.Prosjek).FirstOrDefault() as NajboljeOdjeljenjePoProsjekuViewModel;


                var ucenici = context.Uceniks.ToList();

                ucenici.Average(u => (decimal)u.DatumRodjenja.Subtract(DateTime.Now).Days);

                statistikaVM.ProsjekGodinaUcenika = ucenici.Average(u => ((decimal)u.DatumRodjenja.Subtract(DateTime.Now).Days / 365) * -1);

                statistikaVM.Top5Odlicnih = context.Uceniks.ToList().Select(u => new Top5UceniciViewModel
                {
                    Ime = u.Ime,
                    Prezime = u.Prezime,
                    Prosjek = Math.Round(VratiProsjek(u.Ocjenas.ToList()), 2)
                }).OrderByDescending(o => o.Prosjek).Take(5).ToList();

                statistikaVM.Top3NajboljiNastavnici = context.Nastavniks.ToList().Select(n => new Top3NajboljiNastavniciViewModel
                {
                    Ime = n.Ime,
                    Prezime = n.Prezime,
                    Prosjek = Math.Round(VratiProsjek(n.Ocjenas.ToList()), 2)

                }).OrderByDescending(o => o.Prosjek).Take(3).ToList();


                statistikaVM.Top4NajboljiPredmeti = context.Predmets.ToList().Select(p => new Top4NajboljiPredmeti
                {
                    Naziv = p.Naziv,
                    Prosjek = Math.Round(VratiProsjek(p.Ocjenas.ToList()), 2)

                }).OrderByDescending(o => o.Prosjek).Take(4).ToList();

                statistikaVM.Pol = "";
                statistikaVM.OdjeljenjeId = -1;

                statistikaVM.Odjeljenja = new List<SelectListItem>();

                statistikaVM.Odjeljenja = context.Odjeljenjes.Select(o => new SelectListItem()
                {
                    Value = "" + o.OdjeljenjeId,
                    Text = "" + o.Naziv

                }).ToList();

                statistikaVM.Odjeljenja.Insert(0, new SelectListItem() { Text = "Izaberi odjeljenje", Value = "-1" });

                return View(statistikaVM);
            }

        }
        private double vratiProsjekOdjeljenja(List<Ucenik> listaUcenika)
        {
            double prosjek = 0;
            if (listaUcenika.Count == 0) return 0;
            prosjek = listaUcenika.Average(u => VratiProsjek(u.Ocjenas.ToList()));

            return prosjek;
         
        }

        private int brojUcenika(List<Ucenik> listaUcenika,  int ocjena)
        {
            var novaLista = listaUcenika.Where(u => Math.Round(VratiProsjek(u.Ocjenas.ToList()), MidpointRounding.AwayFromZero) == ocjena).Select(u => u).ToList();
            return novaLista.Count();         
        }

        private double VratiProsjek(List<Ocjena> listaOcjena)
        {
            double prosjek = 0;

            if (listaOcjena.Count == 0) return 1;
            prosjek = listaOcjena.Average(o => o.VrijednostOcjene);
            return prosjek;
        }

        /* [HttpPost]
         public ActionResult Pretraga(string pretraga)
         {
             using (var context = new SkolaContext())
             {
                 var lista = context.Uceniks.Where(u => u.Ime.Contains(pretraga.Trim()) || u.Prezime.Contains(pretraga.Trim()) || u.JMBG.Contains(pretraga.Trim()) ||
                 u.BrojUDnevniku.ToString().Contains(pretraga.Trim()) || pretraga == null).Select(u => new UcenikViewModel
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
                     OdjeljenjeNaziv = u.Odjeljenje.Naziv

                 }).ToList();

                 return View(lista);
             }

         }*/

        public ActionResult Pretraga()
        {
            return View();
              
        }

        public ActionResult GetData(string pretraga, int odjeljenje, string pol)
        {
             using (var context = new SkolaContext())
            {
                var lista = context.Uceniks.Where(u =>((u.OdjeljenjeId == odjeljenje || odjeljenje == -1) && (u.Pol == pol || pol == "")) && (u.Ime.Contains(pretraga.Trim()) || u.Prezime.Contains(pretraga.Trim()) || u.JMBG.Contains(pretraga.Trim()) ||
                u.BrojUDnevniku.ToString().Contains(pretraga.Trim()) ||  pretraga == null)).Select(u => new UcenikViewModel
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
                    DrzavaNaziv = u.Grad.Drzava.Naziv,
                    GradNaziv = u.Grad.Naziv

                }).ToList();

                return PartialView("_PartialPretraga", lista);
            }
        }




    }
}