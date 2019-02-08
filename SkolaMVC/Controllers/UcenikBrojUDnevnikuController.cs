using SkolaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Controllers
{
    public class UcenikBrojUDnevnikuController : Controller
    {
        // GET: UcenikBrojUDnevniku
        public ActionResult Index()
        {
            List<UcenikBrojUDnevnikuViewModel> listaUcenika = new List<UcenikBrojUDnevnikuViewModel>();
            UcenikBrojUDnevnikuViewModel ucenik = new UcenikBrojUDnevnikuViewModel() {UcenikId = 0, Ime = "Milica", Prezime = "P", BrojUDnevniku = 3 };
            UcenikBrojUDnevnikuViewModel ucenik2 = new UcenikBrojUDnevnikuViewModel() { UcenikId = 1, Ime = "Dejan", Prezime = "B", BrojUDnevniku = 5 };
            listaUcenika.Add(ucenik);
            listaUcenika.Add(ucenik2);

            return View(listaUcenika);   //vratiti listu ubud view modela
        }


        [HttpPost]
        public ActionResult Index(List<UcenikBrojUDnevnikuViewModel> Model) {

            return View(Model);
        }
    }
}