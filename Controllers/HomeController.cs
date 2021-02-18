using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using G8InstaDev.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace G8InstaDev.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        Publicacao feedModel = new Publicacao();
        public IActionResult Index()
        {
            // Usuario usuario = new Usuario();
            // ViewBag.usuarioBuscado = usuario.BuscarUsuarioPorId(int.Parse(HttpContext.Session.GetString("_IdLogado")));
            ViewBag.Feeds = feedModel.ReadAll();
            return View();
        }

      
    }
}
