using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using G8InstaDev.Models;

namespace G8InstaDev.Controllers
{

    public class LoginController : Controller
    {
        [TempData]
        public string Mensagem { get; set; }

        Usuario usuarioModel = new Usuario();
        
        [Route("Logar")]
        public IActionResult Logar(IFormCollection form)
        {
            List<String> csv = usuarioModel.ReadAllLinesCSV("Database/Usuario.csv");

            var logado = 
            csv.Find(
                x => 
                (x.Split(";")[1] == form["Login"] ||
                x.Split(";")[3] == form["Login"]) &&
                x.Split(";")[4] == form["Senha"]
            );


            if(logado != null)
            {

                HttpContext.Session.SetString("_IdLogado", logado.Split(";")[0]);
                HttpContext.Session.SetString("_UserName", logado.Split(";")[3]);
                HttpContext.Session.SetString("_NomeCompleto", logado.Split(";")[2]);
                HttpContext.Session.SetString("_Email", logado.Split(";")[1]);
                HttpContext.Session.SetString("_Foto", logado.Split(";")[6]);
                return LocalRedirect("~/Feed/Listar");

            }

            Mensagem = "Dados incorretos, tente novamente...";
            return LocalRedirect("~/");
        }

        
        public IActionResult Index()
        {
            return View();
        }
    }
}