using ProjetoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProjetoWeb.Controllers
{
    public class LojaController : Controller
    {
        private ProjetoWebEntities db = new ProjetoWebEntities();
        // GET: Loja
        public ActionResult Index()
        {
            return View(db.Produto.ToList());
        }

        public ActionResult Carrinho()
        {
            return View();
        }

        public JsonResult FinalizarCompra(List<Produto> produtos)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var idUsuario = Convert.ToInt32(claimsIdentity.FindFirst(ClaimTypes.Sid).Value);

                var venda = new Venda { DataVenda = DateTime.Now, IdUsuario = idUsuario };
                venda.TotalVenda = 0;
                foreach (var prod in produtos)
                {
                    var itemVenda = new VendaItem
                    {
                        CodigoProduto = prod.Codigo,
                        Quantidade = 1,
                        ValorUnitario = prod.Valor,
                        ValorTotal = prod.Valor
                    };
                    venda.TotalVenda += prod.Valor;

                    db.VendaItem.Add(itemVenda);
                }

                db.Venda.Add(venda);

                db.SaveChanges();
                return Json(1);
            }
            catch (Exception ex)
            {
                return Json(0);
                //Pode gravar log do erro no banco: ex.Message
                
            }

        }

        public PartialViewResult HistoricoCompras()
        {
            return PartialView("_HistoricoCompras", db.Venda.ToList());
        }

    }
}