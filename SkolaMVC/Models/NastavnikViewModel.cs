using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkolaMVC.Models
{
    public class NastavnikViewModel
    {
        public int NastavnikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string JMBG { get; set; }
    }
}