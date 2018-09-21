using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects.SqlClient;

namespace EURIS.Service
{
    public class ListinoService
    {
        LocalDbEntities context = new LocalDbEntities();

        public Listino GetListino(int id)
        {
            Listino listino = new Listino();

            listino = (from item in context.ListinoSet select item).SingleOrDefault(x => x.Id == id);
            
            return listino;
        }

        public List<Listino> GetListini()
        {
            List<Listino> listini = new List<Listino>();
            
            listini = (from item in context.ListinoSet
                select item).ToList();
            
            return listini;
        }

        public List<Prodotto> GetProdotti(Listino listino)
        {           
            var listProduct = context.DettaglioListinoSet.Where(x => x.Id_listino == listino.Id).ToList();

            List<Prodotto> prodotti = new List<Prodotto>();

            foreach (var dettaglio in listProduct)
            {
                ProdottoService prodSrv = new ProdottoService();
                if(dettaglio.Id_prodotto.HasValue)
                    prodotti.Add(prodSrv.GetProdotto(dettaglio.Id_prodotto.Value));
            }
            return prodotti;
        }

        public List<Prodotto> GetProdottiFree(Listino listino)
        {
            /*T-SQL QUERY:
                select a.* from ProdottoSet a
                left join DettaglioListinoSet b on a.Id = b.Id_prodotto
                where (b.Id_prodotto is NULL OR b.Id_listino != 1)
            */
            List<Prodotto> prodotti = ( from a in context.ProdottoSet
                                        join b in context.DettaglioListinoSet on a.Id equals b.Id_prodotto into temp
                                        from bsub in temp.DefaultIfEmpty()
                                        where bsub.Id_prodotto == null || bsub.Id_listino != listino.Id
                                        select a
                                      ).ToList();

            return prodotti;
        }

        public Listino CreateListino(Listino listino)
        {
            Listino existingListino = GetListino(listino.Id);

            if (existingListino == null)
            {
                context.ListinoSet.Add(listino);
                context.SaveChanges();
            }
            else
            {
                throw new DbUpdateException();
            }

            return listino;
        }

        public Boolean DeleteListino(Listino listino)
        {
            Listino newListino = GetListino(listino.Id);

            if(newListino != null)
            {              
                /// FIRST remove related entries in dettaglioListino
                DettaglioListinoService dlSrv = new DettaglioListinoService();
                List<DettaglioListino> dettagli = dlSrv.GetDettagliByListino(listino.Id);
                foreach (var dettaglio in dettagli)
                {
                    context.DettaglioListinoSet.Remove(dettaglio);
                    context.SaveChanges();
                }
                
                context.ListinoSet.Remove(listino);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Listino UpdateListino(Listino listino, List<Prodotto> prodotti)
        {
            Listino newListino = GetListino(listino.Id);
            newListino = GetListino(listino.Id);

            if(newListino != null)
            {
                newListino.Codice = listino.Codice;
                newListino.Descrizione = listino.Descrizione;

                context.SaveChanges();
            }
            else
            {
                throw new DbUpdateException();
            }

            return listino;
        }

        public Boolean AddProduct(Listino listino, Prodotto prodotto, int quantita)
        {
            Listino existingListino = GetListino(listino.Id);

            if (existingListino != null)
            {
                ProdottoService prodSrv = new ProdottoService();
                Prodotto existingProdotto = prodSrv.GetProdotto(prodotto.Id);

                if (existingProdotto != null)
                {
                    DettaglioListino dettaglio = new DettaglioListino()
                    {
                        Id_listino = listino.Id,
                        Id_prodotto = prodotto.Id,
                        quantita = quantita
                    };
                    context.DettaglioListinoSet.Add(dettaglio);
                    context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                    //throw new DbUpdateException();       
                }
            }
            else
            {
                return false;
                //throw new DbUpdateException();
            }
        }

        public Boolean RemoveProduct(Listino listino, Prodotto prodotto, int quantita)
        {
            Listino existingListino = GetListino(listino.Id);

            if (existingListino != null)
            {
                ProdottoService prodSrv = new ProdottoService();
                Prodotto existingProdotto = prodSrv.GetProdotto(prodotto.Id);

                if (existingProdotto != null)
                {
                    DettaglioListinoService dlSrv = new DettaglioListinoService();

                    ///Get from different context (NOT WORKING)
                    //DettaglioListino dettaglio = dlSrv.GetDettaglioByListinoProdotto(listino.Id, prodotto.Id);
                    
                    ///Get from same context (OK)
                    DettaglioListino dettaglio = context.DettaglioListinoSet.SingleOrDefault(x => x.Id_listino == listino.Id && x.Id_prodotto == prodotto.Id);

                    if (dettaglio != null)
                    {
                        context.DettaglioListinoSet.Remove(dettaglio);
                        context.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                    //throw new DbUpdateException();       
                }
            }
            else
            {
                return false;
                //throw new DbUpdateException();
            }
        }
    }
}
