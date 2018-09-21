
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Security;
using EURIS.Entities;
using EURIS.Service;


namespace EURISTest.Models
{
    public class ListinoModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Codice { get; set; }

        [Required]
        [StringLength(255)]
        public string Descrizione { get; set; }

        public List<Prodotto> Prodotti { get; set; }

        public ListinoModel(Listino listino, List<Prodotto> prodotti)
        {
            Id = listino.Id;
            Codice = listino.Codice;
            Descrizione = listino.Descrizione;
            Prodotti = prodotti;
        }
    }


    public static class ListinoModelExtensions
    {
        public static Listino ListinoModel2Listino(ListinoModel _listino)
        {
            DettaglioListinoService dlSrv = new DettaglioListinoService();
            List<DettaglioListino> dettagli = dlSrv.GetDettagliByListino(_listino.Id);
            
            Listino listino = new Listino()
            {
                Id = _listino.Id,
                Codice = _listino.Codice,
                Descrizione = _listino.Descrizione,
                //DettaglioListino = dettagli
            };

            return listino;
        }

        public static List<Prodotto> GetProdotti(this ListinoModel _listino)
        {
            Listino listino = ListinoModel2Listino(_listino);

            ListinoService listSrv = new ListinoService();
            List<Prodotto> prodotti = listSrv.GetProdotti(listino);

            return prodotti;
        }

        public static List<Prodotto> GetProdottiFree(this ListinoModel _listino)
        {
            Listino listino = ListinoModel2Listino(_listino);

            ListinoService listSrv = new ListinoService();
            List<Prodotto> prodotti = listSrv.GetProdottiFree(listino);

            return prodotti;
        }
    }
}