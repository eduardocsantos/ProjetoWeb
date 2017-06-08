using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoWeb.Models;
using System.IO;

namespace ProjetoWeb.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ProdutoController : Controller
    {
        private ProjetoWebEntities db = new ProjetoWebEntities();

        // GET: Produto
        public ActionResult Index()
        {
            return View(db.Produto.ToList());
        }

        // GET: Produto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produto.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Marca,Modelo,Descricao,Valor,Saldo,Imagem")] Produto produto, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                if(foto != null)
                {

                    var uploadPath = Server.MapPath("~/FotoProduto");

                    string ext = Path.GetExtension(foto.FileName);
                    string nomeFoto = Guid.NewGuid().ToString();
                    nomeFoto += ext;

                    var caminho = Path.Combine(uploadPath, nomeFoto);
                    foto.SaveAs(caminho);

                    produto.Imagem = "/FotoProduto/" + nomeFoto;
                }
                db.Produto.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produto.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Marca,Modelo,Descricao,Valor,Saldo,Imagem")] Produto produto, HttpPostedFileBase foto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;

                if (foto != null)
                {
                    var uploadPath = Server.MapPath("~/FotoProduto");

                    string ext = Path.GetExtension(foto.FileName);
                    string nomeFoto = Guid.NewGuid().ToString();
                    nomeFoto += ext;

                    var caminho = Path.Combine(uploadPath, nomeFoto);
                    foto.SaveAs(caminho);

                    produto.Imagem = "/FotoProduto/" + nomeFoto;
                }
                else
                {
                    db.Entry(produto).Property(p => p.Imagem).IsModified = false;
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produto.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produto.Find(id);
            db.Produto.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
