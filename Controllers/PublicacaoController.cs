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
    [Route("Feed")]
    public class PublicacaoController : Controller
    {
         Publicacao feedModel = new Publicacao();
    
        [Route("Listar")]
         public IActionResult Index()
         {
            Usuario usuario = new Usuario();
            ViewBag.usuario = usuario.BuscarUsuarioPorId(int.Parse(HttpContext.Session.GetString("_IdLogado")));
            ViewBag.Feeds = feedModel.ReadAll();
            return View();
         }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Publicacao novoFeed = new Publicacao();
            novoFeed.IdPublicacao = feedModel.idPublicacao();

            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Feed");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var nomeFoto = Guid.NewGuid();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, nomeFoto.ToString() + ".png");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // Salvamos o arquivo no caminho especificado
                    file.CopyTo(stream);
                }

                novoFeed.Imagem = nomeFoto.ToString() + ".png";

            }
            else
            {

                novoFeed.Imagem = "padrao.png";
            }

            novoFeed.Legenda = form["Legenda"];
            novoFeed.IdUsuario = int.Parse(HttpContext.Session.GetString("_IdLogado"));
            feedModel.Create(novoFeed);
            ViewBag.Feeds = feedModel.ReadAll();
            return LocalRedirect("~/Feed/Listar");

        }

        [Route("{id}")]
        public  IActionResult Excluir (int id)
        {
            feedModel.Delete(id);
            ViewBag.Jogadores = feedModel.ReadAll();
            return LocalRedirect("~/Feed/Listar");
        }

    }
}