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
    public class UcenikController : Controller
    {
        List<UcenikViewModel> Ucenici = new List<UcenikViewModel>();

        // GET: Ucenik
        public ActionResult Index()
        {
            /*Ucenici = DataHelper.LoadUcenik();
            return View(Ucenici);*/

            using (var context = new SkolaContext())
            {
                var ucenikList = context.Uceniks.Select(u => new UcenikViewModel
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
                    OdjeljenjeNaziv =u.Odjeljenje.Naziv,
                    GradNaziv = u.Grad.Naziv,
                    DrzavaNaziv = u.Grad.Drzava.Naziv

                }).ToList();

                return View(ucenikList);
            }
        }

        public ActionResult Create()
        {
            ViewBag.Pol = new List<string> { "Musko", "Zensko" };

            using (var context = new SkolaContext())
            {
                ViewBag.Odjeljenja = context.Odjeljenjes.Select(o => new OdjeljenjeViewModel()
                {
                    OdjeljenjeId = o.OdjeljenjeId,
                    Razrednik = o.RazrednikId,
                    Razred = o.Razred,
                    Naziv = o.Naziv,
                    SkolskaGodina = o.SkolskaGodina

                }).ToList();

                ViewBag.Odjeljenja = context.Odjeljenjes.Select(o => new SelectListItem() //
                {

                    Text = o.Naziv,
                    Value = "" + o.OdjeljenjeId
                }).ToList();

                ViewBag.Drzave = context.Drzavas.Select(d => new SelectListItem()
                {
                    Text = "" + d.Naziv,
                    Value = "" + d.DrzavaId
                }).ToList();
                ViewBag.Drzave.Insert(0, new SelectListItem {Text = "Izaberi drzavu", Value = "-1" });
            }
                return View();
    }

        public ActionResult VratiGradove(int drzavaId)
        {
            using (var context = new SkolaContext())
            {
             
                var rezultat = context.Grads.Where(g => g.DrzavaId == drzavaId).Select(g => new {

                g.GradId,
                g.Naziv}).ToList();
                return new JsonResult() { Data = rezultat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }


        [HttpPost]
        public ActionResult Create(UcenikViewModel ucenik)
        {
            /* Ucenici = DataHelper.LoadUcenik();
             ucenik.UcenikId = Ucenici.GetNextUcenikId();
             Ucenici.Add(ucenik);
             Ucenici.SaveUcenik();
             return RedirectToAction("Index");*/

            using (var context = new SkolaContext())
            {
                context.Uceniks.Add(new Ucenik()
                {
                    UcenikId = ucenik.UcenikId,
                    Ime = ucenik.Ime,
                    Prezime = ucenik.Prezime,
                    Pol = ucenik.Pol,
                    JMBG = ucenik.JMBG,
                    Adresa = ucenik.Adresa,
                    DatumRodjenja = ucenik.DatumRodjenja,
                    ImeRoditelja = ucenik.ImeRoditelja,
                    BrojUDnevniku = ucenik.BrojUDnevniku,
                    Drzavljanstvo = ucenik.Drzavljanstvo,
                    OdjeljenjeId = ucenik.OdjeljenjeId,
                    GradId = ucenik.GradId
                    
                });
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            /*int ucenikId = Convert.ToInt32(id);
            Ucenici = DataHelper.LoadUcenik();
            UcenikViewModel ucenik = Ucenici.Where(u => u.UcenikId == ucenikId).FirstOrDefault();
            ViewBag.Pol = new List<string> { "Musko", "Zensko" };
            return View(ucenik);*/

            int ucenikId = Convert.ToInt32(id);
            using (var context = new SkolaContext())
            {
                Ucenik ucenik = context.Uceniks.Where(u => u.UcenikId == ucenikId).FirstOrDefault();
                UcenikViewModel ucenikViewModel = new UcenikViewModel()
                {
                    UcenikId = ucenik.UcenikId,
                    Ime = ucenik.Ime,
                    Prezime = ucenik.Prezime,
                    Pol = ucenik.Pol,
                    JMBG = ucenik.JMBG,
                    Adresa = ucenik.Adresa,
                    DatumRodjenja = ucenik.DatumRodjenja,
                    ImeRoditelja = ucenik.ImeRoditelja,
                    BrojUDnevniku = ucenik.BrojUDnevniku,
                    Drzavljanstvo = ucenik.Drzavljanstvo,
                    OdjeljenjeId = ucenik.OdjeljenjeId
                };

                ViewBag.Pol = new List<string> { "Musko", "Zensko" };

                ViewBag.Odjeljenja = context.Odjeljenjes.Select(o => new SelectListItem() //
                {               
                  Text = o.Naziv,
                   Value = "" + o.OdjeljenjeId
                }).ToList();

                ViewBag.Drzave = context.Drzavas.Select(d => new SelectListItem()
                {
                    Text = "" + d.Naziv,
                    Value = "" + d.DrzavaId
                }).ToList();

                ViewBag.Drzave = context.Drzavas.Select(d => new SelectListItem()
                {
                    Text = "" + d.Naziv,
                    Value = "" + d.DrzavaId
                }).ToList();
                ViewBag.Drzave.Insert(0, new SelectListItem { Text = "Izaberi drzavu", Value = "-1" });

                return View(ucenikViewModel);
            }        
        }

        [HttpPost]
        public ActionResult Edit(UcenikViewModel ucenik)
        {
            /*Ucenici = DataHelper.LoadUcenik();
            for (int i = 0; i < Ucenici.Count; i++)
            {
                if (Ucenici.ElementAt(i).UcenikId == ucenik.UcenikId)
                {
                    Ucenici[i] = ucenik;
                    break;
                }
            }
            Ucenici.SaveUcenik();
            return RedirectToAction("Index");*/

            using (var context = new SkolaContext())
            {
                Ucenik uc = context.Uceniks.Find(ucenik.UcenikId);


                uc.UcenikId = ucenik.UcenikId;
                uc.Ime = ucenik.Ime;
                uc.Prezime = ucenik.Prezime;
                uc.Pol = ucenik.Pol;
                uc.JMBG = ucenik.JMBG;
                uc.Adresa = ucenik.Adresa;
                uc.DatumRodjenja = ucenik.DatumRodjenja;
                uc.ImeRoditelja = ucenik.ImeRoditelja;
                uc.BrojUDnevniku = ucenik.BrojUDnevniku;
                uc.Drzavljanstvo = ucenik.Drzavljanstvo;
                uc.OdjeljenjeId = ucenik.OdjeljenjeId;
                uc.GradId = ucenik.GradId;

                context.SaveChanges();
               }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            /*Ucenici = DataHelper.LoadUcenik();    // vraca listu ucenika
            int ucenikId = Convert.ToInt32(id);
            UcenikViewModel ucenik = Ucenici.Where(u => u.UcenikId == ucenikId).FirstOrDefault();  //prolazi kroz listu i trazi proslijedjeni id

            return View(ucenik);   //vraca view sa podacima o tom uceniku*/

            int ucenikId = Convert.ToInt32(id);

            using (var context = new SkolaContext())
            {
                Ucenik ucenik = context.Uceniks.Where(u => u.UcenikId == ucenikId).FirstOrDefault();
                UcenikViewModel ucenikViewModel = new UcenikViewModel()
                {
                    UcenikId = ucenik.UcenikId,
                    Ime = ucenik.Ime,
                    Prezime = ucenik.Prezime,
                    Pol = ucenik.Pol,
                    JMBG = ucenik.JMBG,
                    Adresa = ucenik.Adresa,
                    DatumRodjenja = ucenik.DatumRodjenja,
                    ImeRoditelja = ucenik.ImeRoditelja,
                    BrojUDnevniku = ucenik.BrojUDnevniku,
                    Drzavljanstvo = ucenik.Drzavljanstvo,
                    OdjeljenjeId = ucenik.OdjeljenjeId               
            };

                //ucenikViewModel.OdjeljenjeNaziv = ucenik.Odjeljenje.Naziv;
                return View(ucenikViewModel);
            }

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            /*Ucenici = DataHelper.LoadUcenik();
            foreach (UcenikViewModel ucenik in Ucenici)
            {
                if (ucenik.UcenikId == id)
                {
                    Ucenici.Remove(ucenik);
                    break;
                }
            }
            Ucenici.SaveUcenik();
            return RedirectToAction("Index");*/


            using (var context = new SkolaContext())
            {
                
                context.Uceniks.Remove(context.Uceniks.Find(id));
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Details(string id)
        {
            /*Ucenici = DataHelper.LoadUcenik();
            List<OcjenaViewModel> Ocjene = DataHelper.LoadOcjene();     
            int ucenikId = Convert.ToInt32(id);
            UcenikViewModel ucenik = Ucenici.Where(u => u.UcenikId == ucenikId).FirstOrDefault();
            var ocjene = Ocjene.Where(o => o.UcenikId == ucenikId);
            ViewBag.Ocjene = ocjene;
            return View(ucenik);*/

            int ucenikId = Convert.ToInt32(id);
            using (var context = new SkolaContext())
            {
                Ucenik ucenik = context.Uceniks.Where(u => u.UcenikId == ucenikId).FirstOrDefault();
                UcenikViewModel ucenikViewModel = new UcenikViewModel()
                {
                    UcenikId = ucenik.UcenikId,
                    Ime = ucenik.Ime,
                    Prezime = ucenik.Prezime,
                    Pol = ucenik.Pol,
                    JMBG = ucenik.JMBG,
                    Adresa = ucenik.Adresa,
                    DatumRodjenja = ucenik.DatumRodjenja,
                    ImeRoditelja = ucenik.ImeRoditelja,
                    BrojUDnevniku = ucenik.BrojUDnevniku,
                    Drzavljanstvo = ucenik.Drzavljanstvo,
                    OdjeljenjeId = ucenik.OdjeljenjeId,
                    GradNaziv = ucenik.Grad.Naziv,
                    DrzavaNaziv = ucenik.Grad.Drzava.Naziv

                };

                ucenikViewModel.OdjeljenjeNaziv = ucenik.Odjeljenje.Naziv;

                ViewBag.Ocjene = 
                context.Ocjenas.Where(o => o.UcenikId == ucenikId).Select(o => new OcjenaViewModel()
                {
                    OcjenaId = o.OcjenaId,
                    UcenikId = o.UcenikId,
                    Datum = o.Datum,
                    Vrijednost = o.VrijednostOcjene,
                    TipOcjene = (TipOcjene)o.TipOcjene,
                    Predmet = o.PredmetId,
                    Nastavnik = o.NastavnikId,
                    NazivPredmeta = o.Predmet.Naziv,
                    ImeNastavnika = o.Nastavnik.Ime,
                    PrezimeNastavnika = o.Nastavnik.Prezime
                }).ToList();
                return View(ucenikViewModel);
            }       
        }
    }
}