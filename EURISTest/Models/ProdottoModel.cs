
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using EURIS.Entities;


namespace EURISTest.Models
{
    public class ProdottoModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Codice { get; set; }

        [Required]
        [StringLength(255)]
        public string Descrizione { get; set; }

        public ProdottoModel(Prodotto prodotto)
        {
            Id = prodotto.Id;
            Codice = prodotto.Codice;
            Descrizione = prodotto.Descrizione;
        }
    }
}