//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EURIS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Prodotto
    {
        public Prodotto()
        {
            this.DettaglioListinoSet = new HashSet<DettaglioListino>();
        }
    
        public int Id { get; set; }
        public string Codice { get; set; }
        public string Descrizione { get; set; }
    
        public virtual ICollection<DettaglioListino> DettaglioListinoSet { get; set; }
    }
}
