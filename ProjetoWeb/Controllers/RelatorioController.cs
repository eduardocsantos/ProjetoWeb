using ProjetoWeb.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoWeb.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListaVendasPorDia(string ano, string mes)
        {
            var repositorio = new RelatorioRepositorio();
            var totais = repositorio.VendasPorDia(ano, mes);
            return Json(totais, JsonRequestBehavior.AllowGet);
        }
    }
}