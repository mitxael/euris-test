using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace EURIS.Service
{
    public class ProdottoService
    {
        LocalDbEntities context = new LocalDbEntities(); 

        public List<Prodotto> GetProdotti()
        {
            List<Prodotto> prodotti = new List<Prodotto>();
            
            prodotti = (from item in context.ProdottoSet
                          select item).ToList();

            return prodotti;
        }

        public Prodotto GetProdotto(int id)
        {
            Prodotto prodotto = new Prodotto();

            prodotto = (from item in context.ProdottoSet select item).SingleOrDefault(x => x.Id == id);
            
            return prodotto;
        }

        public Prodotto CreateProdotto(Prodotto prodotto)
        {
            Prodotto existingProdotto = GetProdotto(prodotto.Id);

            if (existingProdotto == null)
            {
                context.ProdottoSet.Add(prodotto);
                context.SaveChanges();
            }
            else
            {
                throw new DbUpdateException();
            }

            return prodotto;
        }

        public Boolean DeleteProdotto(Prodotto prodotto)
        {
            Prodotto newProdotto = GetProdotto(prodotto.Id);

            if(newProdotto != null)
            {
                /// FIRST remove related entries in dettaglioListino
                DettaglioListinoService dlSrv = new DettaglioListinoService();
                List<DettaglioListino> dettagli = dlSrv.GetDettagliByProdotto(prodotto.Id);
                foreach (var dettaglio in dettagli)
                {
                    context.DettaglioListinoSet.Remove(dettaglio);
                }

                context.ProdottoSet.Remove(prodotto);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Prodotto UpdateProdotto(Prodotto prodotto)
        {
            Prodotto newProdotto = GetProdotto(prodotto.Id);

            if(newProdotto != null)
            {
                newProdotto.Codice = prodotto.Codice;
                newProdotto.Descrizione = prodotto.Descrizione;

                context.SaveChanges();
            }
            else
            {
                throw new DbUpdateException();
            }

            return prodotto;
        }

    }
}
