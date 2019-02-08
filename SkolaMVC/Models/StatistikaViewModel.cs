using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkolaMVC.Models
{
    public class StatistikaViewModel
    {

        public int BrojUcenika { get; set; }
        public List<string> Top3Odlicnih { get; set; }
        public int BrojOdlicnih { get; set; }
        public int BrojVrloDobrih { get; set; }
        public int BrojDobrih { get; set; }
        public int BrojDovoljnih { get; set; }
        public int BrojNedovoljnih { get; set; }
        public List<BrojOdlicnihOdjeljenjeViewModel> BrojOdlicnihPoOdjeljenjima { get; set; }
        public int BrojMuskih { get; set; }
        public int BrojZenskih { get; set; }
        public NajboljeOdjeljenjePoProsjekuViewModel NajboljeOdjeljenjePoProsjeku { get; set; }
        public decimal ProsjekGodinaUcenika { get; set; }
        public string Pol { get; set; }
        public int OdjeljenjeId { get; set; }
        public List<SelectListItem> Odjeljenja { get; set; }

        public List<Top5UceniciViewModel> Top5Odlicnih { get; set; }
        public List<Top3NajboljiNastavniciViewModel> Top3NajboljiNastavnici { get; set; }
        public List<Top4NajboljiPredmeti> Top4NajboljiPredmeti { get; set; }

        public string Pretraga { get; set; }



    }
}