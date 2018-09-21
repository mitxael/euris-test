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
    public class ProdottoController : Controller
    {

        public ActionResult Index()
        {
            ProdottoService prodSrv = new ProdottoService();
            List<Prodotto> prodotti = prodSrv.GetProdotti();

            ViewBag.Prodotti = prodotti;

            return View();
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        public ActionResult Delete(int id)
        {
            ProdottoService prodSrv = new ProdottoService();

            Prodotto prodotto = prodSrv.GetProdotto(id);

            if (prodotto == null)
                return HttpNotFound();

            Boolean result = prodSrv.DeleteProdotto(prodotto);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            ProdottoService prodSrv = new ProdottoService();
            Prodotto prodotto = prodSrv.GetProdotto(id);

            if (prodotto == null)
                return HttpNotFound();

            var viewModel = new ProdottoModel(prodotto);

            return View("Details", viewModel);

        }

        public ActionResult Edit(int id)
        {
            ProdottoService prodSrv = new ProdottoService();
            Prodotto prodotto = prodSrv.GetProdotto(id);

            if (prodotto == null)
                return HttpNotFound();

            var viewModel = new ProdottoModel(prodotto);

            return View("Edit", viewModel);
        }

        public ActionResult Save(Prodotto prodotto)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProdottoModel(prodotto);

                return View("Edit", viewModel);
            }

            ProdottoService prodSrv = new ProdottoService();
            if (prodotto.Id == 0)
            {
                prodSrv.CreateProdotto(prodotto);
            }
            else
            {
                prodSrv.UpdateProdotto(prodotto);
            }

            return RedirectToAction("Index");
        }

    }
}
