using System.IO;
using System.Collections.Generic;
using G8InstaDev.Interface;
using System;

namespace G8InstaDev.Models
{
    public class Usuario : InstaDevBase, IUsuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeDoUsuario { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Foto { get; set; }

        private const string PATH = "Database/Usuario.csv";

        // Usuario usuario = new Usuario();

        public Usuario()
        {
            CreateFolderAndFile(PATH);
        }

        public string Prepare(Usuario u)
        {
            return $"{u.IdUsuario};{u.Email};{u.NomeCompleto};{u.NomeDoUsuario};{u.Senha};{u.DataNascimento};{u.Foto}";
        }

        public int idCadastro()
        {
            var cadastro = ReadAll();

            if (cadastro.Count == 0)
            {
                return 1;
            }

            var codigo = cadastro[cadastro.Count - 1].IdUsuario;

            codigo++;

            return codigo;
        }
        public void Create(Usuario u)
        {
            string[] linhas = { Prepare(u) };
            File.AppendAllLines(PATH, linhas);
        }

        public List<Usuario> ReadAll()
        {
            List<Usuario> cadastros = new List<Usuario>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                Usuario usuario = new Usuario();
                usuario.IdUsuario = int.Parse(linha[0]);
                usuario.Email = linha[1];
                usuario.NomeCompleto = linha[2];
                usuario.NomeDoUsuario = linha[3];
                usuario.Senha = linha[4];
                usuario.DataNascimento = DateTime.Parse(linha[5]);

                if (linha[6] == "")
                {
                    usuario.Foto = "padrao.png";
                }
                else
                {
                    usuario.Foto = linha[6];
                }

                cadastros.Add(usuario);

            }

            return cadastros;
        }

        public Usuario BuscarUsuarioPorId(int id)
        {
            Usuario usuarioBusca = new Usuario();

            List<String> csv = usuarioBusca.ReadAllLinesCSV("Database/Usuario.csv");

            var linhaBuscada =
            csv.Find(
                x =>
                x.Split(";")[0] == id.ToString()
            );

            var usuarioLinha = linhaBuscada.Split(";");
            Usuario usuarioBuscado = new Usuario();
            usuarioBuscado.IdUsuario = int.Parse(usuarioLinha[0]);
            usuarioBuscado.Email = usuarioLinha[1];
            usuarioBuscado.NomeCompleto = usuarioLinha[2];
            usuarioBuscado.NomeDoUsuario = usuarioLinha[3];
            usuarioBuscado.Senha = usuarioLinha[4];
            usuarioBuscado.DataNascimento = DateTime.Parse(usuarioLinha[5]);
            if (usuarioLinha[6] == "")
            {
                usuarioBuscado.Foto = "padrao.png";
            }
            else
            {
                usuarioBuscado.Foto = usuarioLinha[6];
            }
            return usuarioBuscado;
        }

        public void Update(Usuario u)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == u.IdUsuario.ToString());
            linhas.Add(Prepare(u));
            RewriteCSV(PATH, linhas);
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            RewriteCSV(PATH, linhas);
        }
    }

}