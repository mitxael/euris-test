using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
using EURISTest.Models;

namespace EURISTest.Controllers
{
    public class ListinoController : Controller
    {

        public ActionResult Index()
        {
            ListinoService listSrv = new ListinoService();
            List<Listino> listini = listSrv.GetListini();

            ViewBag.Listini = listini;

            return View();
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        public ActionResult Delete(int id)
        {
            ListinoService listSrv= new ListinoService();

            Listino listino = listSrv.GetListino(id);

            if (listino == null)
                return HttpNotFound();

            Boolean result = listSrv.DeleteListino(listino);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            ListinoService listSrv = new ListinoService();
            Listino listino = listSrv.GetListino(id);
            
            if (listino == null)
                return HttpNotFound();

            List<Prodotto> prodotti = listSrv.GetProdotti(listino);
            
            var viewModel = new ListinoModel(listino, prodotti);

            return View("Details", viewModel);

        }

        public ActionResult Edit(int id)
        {
            ListinoService listSrv = new ListinoService();
            Listino listino = listSrv.GetListino(id);

            if (listino == null)
                return HttpNotFound();

            List<Prodotto> prodotti = listSrv.GetProdotti(listino);

            var viewModel = new ListinoModel(listino, prodotti);

            return View("Edit", viewModel);
        }

        public ActionResult Save(Listino listino, List<Prodotto> prodotti)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ListinoModel(listino, prodotti);

                return View("Edit", viewModel);
            }

            ListinoService listSrv = new ListinoService();
            if (listino.Id == 0)
            {
                listSrv.CreateListino(listino);
            }
            else
            {
                listSrv.UpdateListino(listino, prodotti);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Manage(int id)
        {
            ListinoService listSrv = new ListinoService();
            Listino listino = listSrv.GetListino(id);

            if (listino == null)
                return HttpNotFound();

            List<Prodotto> prodotti = listSrv.GetProdotti(listino);

            var viewModel = new ListinoModel(listino, prodotti);

            return View("Manage", viewModel);
        }

        public ActionResult AddProduct(int idListino, int idProdotto, int quantita)
        {
            /*foreach (ModelState state in ViewData.ModelState.Values.Where(x => x.Errors.Count > 0))
            {
                var faultyState = state;
            }*/

            if (ModelState.IsValid)
            {
                ListinoService listSrv = new ListinoService();
                Listino listino = listSrv.GetListino(idListino);

                ProdottoService prodSrv = new ProdottoService();
                Prodotto prodotto = prodSrv.GetProdotto(idProdotto);
                bool result = listSrv.AddProduct(listino, prodotto, quantita);

                List<Prodotto> prodotti = listSrv.GetProdotti(listino);
                var viewModel = new ListinoModel(listino, prodotti);

                return View("Manage", viewModel);
                //return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return null;
            }
        }

        public ActionResult RemoveProduct(int idListino, int idProdotto, int quantita)
        {
            if (ModelState.IsValid)
            {
                ListinoService listSrv = new ListinoService();
                Listino listino = listSrv.GetListino(idListino);

                ProdottoService prodSrv = new ProdottoService();
                Prodotto prodotto = prodSrv.GetProdotto(idProdotto);
                bool result = listSrv.RemoveProduct(listino, prodotto, quantita);

                List<Prodotto> prodotti = listSrv.GetProdotti(listino);
                var viewModel = new ListinoModel(listino, prodotti);

                return View("Manage", viewModel);
                //return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                return null;
            }
        }
    }
}
