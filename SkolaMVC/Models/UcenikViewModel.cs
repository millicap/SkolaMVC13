using SkolaMVC.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkolaMVC.Models
{
    public class UcenikViewModel
    {
        public int UcenikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Pol { get; set; }
        [StringLength(13, ErrorMessage = "JMBG mora da sadrzi 13 cifara"), MinLength(13, ErrorMessage = "JMBG mora da sadrzi 13 cifara")]
        //[MaxWordAttribute(13, ErrorMessage ="Poruka")]
        public string JMBG { get; set; }
        public string Adresa { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Datum rodjenja")]
        public DateTime DatumRodjenja { get; set; }
        [Display(Name = "Ime roditelja")]
        public string ImeRoditelja { get; set; }
        [Display(Name = "Broj u dnevniku")]
        public int BrojUDnevniku { get; set; }
        public string Drzavljanstvo { get; set; }
        public int OdjeljenjeId { get; set; }
        [Display(Name = "Odjeljenje")]
        public string OdjeljenjeNaziv { get; set; }

        public int GradId { get; set; }

        [Display(Name = "Grad")]
        public string GradNaziv { get; set; }
        [Display(Name = "Drzava")]
        public string DrzavaNaziv { get; set; }

    }
}