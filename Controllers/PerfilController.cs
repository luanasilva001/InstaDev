using G8InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G8InstaDev.Controllers
{
    [Route("Perfil")]
    public class PerfilController : Controller
    {

        public IActionResult Index([FromQuery] int idUsuario)
        {
            Usuario usuario = new Usuario();

            var userId = -1;

            if(idUsuario == 0)
                userId = int.Parse(HttpContext.Session.GetString("_IdLogado"));
            else
                userId = idUsuario;

            Publicacao publicacao = new Publicacao();

            ViewBag.logado = int.Parse(HttpContext.Session.GetString("_IdLogado")); 

            ViewBag.publicacao = publicacao.AcharPostsDoUsuario(userId);

            ViewBag.perfil = usuario.BuscarUsuarioPorId(userId);

            ViewBag.userLogado = usuario.BuscarUsuarioPorId(int.Parse(HttpContext.Session.GetString("_IdLogado")));

            return View();
        }

        [Route("Listar")]

        public IActionResult Listar()
        {
            
            return View();
        }

        [Route("Sair")]
        public IActionResult Sair()
        {
            HttpContext.Session.Remove("_IdLogado");
            return LocalRedirect("~/");
        }
    }
}