//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SkolaMVC.DBModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Odjeljenje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Odjeljenje()
        {
            this.Uceniks = new HashSet<Ucenik>();
        }
    
        public int OdjeljenjeId { get; set; }
        public int RazrednikId { get; set; }
        public int Razred { get; set; }
        public string Naziv { get; set; }
        public int SkolskaGodina { get; set; }
    
        public virtual Nastavnik Nastavnik { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ucenik> Uceniks { get; set; }
    }
}