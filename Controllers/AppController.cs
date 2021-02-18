using G8InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G8InstaDev.Controllers
{
    public class AppController : Controller
    {

        public IActionResult Index()
        {
            Usuario usuario = new Usuario();
            ViewBag.usuarioBuscado = usuario.BuscarUsuarioPorId(int.Parse(HttpContext.Session.GetString("_IdLogado")));
            return View();

        }
    }
}