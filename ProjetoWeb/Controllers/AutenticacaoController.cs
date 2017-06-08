using ProjetoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProjetoWeb.Controllers
{
    public class AutenticacaoController : Controller
    {
        private ProjetoWebEntities db = new ProjetoWebEntities();
        
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {

            var usuario = db.Usuario.Where(u => u.Email == model.Login && u.Senha == model.Senha).FirstOrDefault();

            if(usuario != null)
            {
                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.GrupoUsuario.Descricao),
                }, 
                "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                if (usuario.GrupoUsuario.Descricao == "Administrador")
                {
                    return Redirect("~/home");
                }
                else
                {
                    return Redirect("~/loja");
                }
            }


            ViewBag.falha = "Usuário ou senha inválido";
            return View();
        }

        public ActionResult Sair()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");

            return RedirectToAction("Login");
        }
    }
}