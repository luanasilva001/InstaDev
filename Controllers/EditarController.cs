using Microsoft.AspNetCore.Mvc;
using G8InstaDev.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using System;

namespace G8InstaDev_master.Controllers
{
    [Route("Editar")]
    public class EditarController : Controller
    {
        Usuario editar = new Usuario();


        [Route("Listar")]
        public IActionResult Index()
        {

            ViewBag.Usuario = editar.BuscarUsuarioPorId(int.Parse(HttpContext.Session.GetString("_IdLogado")));

            ViewBag.Editar = editar.ReadAll();
            return View();

        }

        [Route("Cadastrar")]
        public IActionResult Editar(IFormCollection form)
        {
            Usuario usuarioNovo = new Usuario();
            Usuario usuarioBuscado = new Usuario();



            if (form.Files.Count > 0)
            {
                var file = form.Files[0];


                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Editar");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var nomeFoto = Guid.NewGuid();

                // nomeFoto.ToString() + ".png";

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Editar/", folder, nomeFoto.ToString() + ".png");

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    // Salvamos o arquivo no caminho especificado
                    file.CopyTo(stream);
                }

                usuarioNovo.Foto = nomeFoto.ToString() + ".png";


            }
            else
            {
                Usuario logado = usuarioBuscado.BuscarUsuarioPorId(int.Parse(HttpContext.Session.GetString("_IdLogado")));
                usuarioNovo.Foto = logado.Foto;
            }


            usuarioNovo.IdUsuario = int.Parse(HttpContext.Session.GetString("_IdLogado"));
            usuarioNovo.Email = form["Email"];
            usuarioNovo.NomeCompleto = form["NomeCompleto"];
            usuarioNovo.NomeDoUsuario = form["NomeUsuario"];
            usuarioNovo.Senha = form["Senha"];
            editar.Update(usuarioNovo);
            ViewBag.Editar = editar.ReadAll();
            return LocalRedirect("~/Editar/Listar");
        }

        [Route("{id}")]
        public IActionResult Excluir([FromQuery]int id)
        {
            Publicacao publicacao = new Publicacao();
            var userId = -1;
            if(id == 0){
                userId = int.Parse(HttpContext.Session.GetString("_IdLogado"));
            }
            editar.Delete(userId);
            publicacao.DeletarTodasPublicacoesUsuario(userId);
            ViewBag.Editar = editar.ReadAll();

            HttpContext.Session.Remove("_IdLogado");
            HttpContext.Session.Remove("_UserName");
            HttpContext.Session.Remove("_NomeCompleto");
            HttpContext.Session.Remove("_Email");
            HttpContext.Session.Remove("_Foto");

            return LocalRedirect("~/");

        }
    }
}