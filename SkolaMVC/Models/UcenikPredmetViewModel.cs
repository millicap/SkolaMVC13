using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkolaMVC.Models
{
    public class UcenikPredmetViewModel
    {
        public int UcenikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public List<PredmetViewModel> Predmeti { get; set; }

    }
}