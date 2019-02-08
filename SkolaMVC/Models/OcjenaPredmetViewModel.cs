using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SkolaMVC.Models
{
    public class OcjenaPredmetViewModel
    {
        public int OcjenaId { get; set; }
        public int Vrijednost { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumOcjene { get; set; }
        public TipOcjene TipOcjene { get; set; }
   }
}