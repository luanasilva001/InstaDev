using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using G8InstaDev.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace G8InstaDev.Controllers
{
    [Route("Usuario")]
    public class CadastroController : Controller
    {
        Usuario usuarioModel = new Usuario();
        // [Route("Listar")]
        public IActionResult Index(){
            ViewBag.Cadastro = usuarioModel.ReadAll();

            // Usuario novalistagemdeusuario = new Usuario();
            // ViewBag.novalistagemdeusuario = usuarioModel.ReadAll();
            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection formCadastro){
            Usuario cadastrar = new Usuario();
            cadastrar.IdUsuario = usuarioModel.idCadastro();
            cadastrar.Email = formCadastro["Email"];
            cadastrar.NomeCompleto = formCadastro["NomeCompleto"];
            cadastrar.NomeDoUsuario = formCadastro["NomeUsuario"];
            cadastrar.Senha = formCadastro["Senha"];
            cadastrar.Foto = "padrao.png";

            usuarioModel.Create(cadastrar);
            ViewBag.Cadastro = usuarioModel.ReadAll();
            return LocalRedirect("~/");
        }
    }
}