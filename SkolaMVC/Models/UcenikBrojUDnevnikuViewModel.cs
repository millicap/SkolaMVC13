using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkolaMVC.Models
{
    public class UcenikBrojUDnevnikuViewModel
    {
        [Required]
        public int UcenikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int BrojUDnevniku { get; set; }

    }
}