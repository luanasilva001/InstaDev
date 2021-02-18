using System.Threading.Tasks;
using G8InstaDev.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace G8InstaDev.Controllers
{
    public class PublicacaoAPIController
    {
        [HttpGet("PublicacaoAPI")]
        public List<Publicacao> List()
        {
            Publicacao publicacao = new Publicacao();
            return publicacao.ReadAll();
        }
        [HttpGet("PublicacaoAPI/{id}")]
        public List<Publicacao> List(int id)
        {
            Publicacao publicacao = new Publicacao();
            return publicacao.Read(id);
        }
        [HttpPost("PublicacaoAPI")]
        public bool Save([FromBody] Publicacao f)
        {
            Publicacao publicacao = new Publicacao();
            publicacao.Create(f);
            return true;
        }
        [HttpPut("PublicacaoAPI")]
        public bool Edit([FromBody] Publicacao f)
        {
            Publicacao publicacao = new Publicacao();
            publicacao.Update(f);
            return true;
        }
        [HttpDelete("PublicacaoAPI/{id}")]
        public bool Delete(int id)
        {
            Publicacao publicacao = new Publicacao();
            publicacao.Delete(id);
            return true;
        }
        [HttpGet("PublicacaoAPI/ListarUsuario")]
        public List<string> ListarUsuario()
        {
            Usuario usuario = new Usuario();
            List<Usuario> usuariolst = usuario.ReadAll();
            List<string> retornolst = new List<string>();
            foreach (Usuario usuarioItem in usuariolst)
            {
                retornolst.Add(usuarioItem.NomeCompleto);
                
            }
            return retornolst;
        }

    }
}
