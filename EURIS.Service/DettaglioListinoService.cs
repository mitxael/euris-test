using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EURIS.Service
{
    public class DettaglioListinoService
    {
        LocalDbEntities context = new LocalDbEntities();

        public DettaglioListino GetDettaglioListino(int id)
        {
            DettaglioListino dettaglioListino = context.DettaglioListinoSet.SingleOrDefault(x => x.Id == id);
            
            return dettaglioListino;
        }

        public List<DettaglioListino> GetDettagliByListino(int id)
        {
            List<DettaglioListino> dettaglioListino = context.DettaglioListinoSet.Where(x => x.Id_listino == id).ToList();
            
            return dettaglioListino;
        }

        public List<DettaglioListino> GetDettagliByProdotto(int id)
        {
            List<DettaglioListino> dettaglioListino = context.DettaglioListinoSet.Where(x => x.Id_prodotto == id).ToList();
            
            return dettaglioListino;
        }

        public DettaglioListino GetDettaglioByListinoProdotto(int idListino, int idProdotto)
        {
            DettaglioListino dettaglioListino = context.DettaglioListinoSet.SingleOrDefault(x => x.Id_listino == idListino && x.Id_prodotto == idProdotto);
            
            return dettaglioListino;
        }
      
    }
}
