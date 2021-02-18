using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace G8InstaDev.Models
{
    public class Publicacao : InstaDevBase, IPublicacao
    {
        public int IdPublicacao { get; set; }
        public string Imagem { get; set; }
        public string Legenda { get; set; }
        public int IdUsuario { get; set; }

        public string FotoUsuario { get; set; }
        public string NomeCompleto { get; set; }
        
        
        public string NomeUsuario { get; set; }
        
        

        private const string PATH = "Database/Feed.csv";

        Usuario usuarioModel = new Usuario();

        public Publicacao()
        {
            CreateFolderAndFile(PATH);
        }

        public string Prepare(Publicacao p)
        {
            return $"{p.IdPublicacao};{p.Imagem};{p.Legenda};{p.IdUsuario}";
        }

        public int idPublicacao()
        {
            var publicacoes = ReadAll();

            if (publicacoes.Count == 0)
            {
                return 1;
            }

            var codigo = publicacoes[publicacoes.Count - 1].IdPublicacao;

            codigo++;

            return codigo;
        }
        public void Create(Publicacao p)
        {
            string[] linhas = { Prepare(p) };
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int id)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
            RewriteCSV(PATH, linhas);
        }

        public void DeletarTodasPublicacoesUsuario(int id){
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[3] == id.ToString());
            RewriteCSV(PATH, linhas);
        }

        public List<Publicacao> ReadAll()
        {
            List<Publicacao> feeds = new List<Publicacao>();
            string[] linhas = File.ReadAllLines(PATH);

            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");

                Publicacao publicacao = new Publicacao();
                publicacao.IdPublicacao = int.Parse(linha[0]);
                publicacao.Imagem = linha[1];
                publicacao.Legenda = linha[2];
                publicacao.IdUsuario = int.Parse(linha[3]);





                // List<String> csv = usuarioModel.BuscarUsuarioPorId(publicacao.IdUsuario);
                // var linhaBuscada =
                // csv.Find(
                //     x =>
                //     x.Split(";")[0] == linha[3]
                // );

                var usuarioBuscado = usuarioModel.BuscarUsuarioPorId(publicacao.IdUsuario);

                // var usuarioLinha = linhaBuscada.Split(";");
                publicacao.FotoUsuario = usuarioBuscado.Foto;
                publicacao.NomeUsuario = usuarioBuscado.NomeDoUsuario;
                publicacao.NomeCompleto = usuarioBuscado.NomeCompleto;

                feeds.Add(publicacao);
            }

            feeds.Reverse();

            return feeds;
        }

        public List<Publicacao> Read(int id)
        {
            List<Publicacao> pubs = ReadAll();

            pubs = pubs.FindAll(pub => pub.IdPublicacao == id);
            pubs.Reverse();

            return pubs;
        }


        public List<Publicacao> AcharPostsDoUsuario(int id)
        {
            List<Publicacao> publicacoes = new List<Publicacao>();

            Usuario usuarioModel = new Usuario();

            var usuarioLinha = usuarioModel.BuscarUsuarioPorId(id);
            
            List<string> linhas = new List<string>();
            linhas.AddRange(File.ReadAllLines(PATH));

            var linhasDoUsuario = linhas.FindAll(x => x.Split(";")[3] == id.ToString());

            foreach (var item in linhasDoUsuario)
            {
                string[] linha = item.Split(";");


                Publicacao publicacao = new Publicacao();
                publicacao.IdPublicacao = int.Parse(linha[0]);
                publicacao.Imagem = linha[1];
                publicacao.Legenda = linha[2];
                publicacao.IdUsuario = int.Parse(linha[3]);
                publicacao.FotoUsuario = usuarioLinha.Foto;
                publicacao.NomeUsuario = usuarioLinha.NomeDoUsuario;
                publicacao.NomeCompleto = usuarioLinha.NomeCompleto;

                publicacoes.Add(publicacao);
            }

            publicacoes.Reverse();

            return publicacoes;
        }

        public void Update(Publicacao p)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == p.IdPublicacao.ToString());
            linhas.Add(Prepare(p));
            RewriteCSV(PATH, linhas);
        }

        public Usuario Buscar(int id)
        {
            List<String> csv = usuarioModel.ReadAllLinesCSV("Database/Usuario.csv");

            var linhaBuscada =
            csv.Find(
                x =>
                x.Split(";")[0] == id.ToString()
            );

            var usuarioLinha = linhaBuscada.Split(";");
            Usuario usuarioBuscado = new Usuario();
            usuarioBuscado.Foto = usuarioLinha[6].ToString();
            usuarioBuscado.NomeDoUsuario = usuarioLinha[3].ToString();
            return usuarioBuscado;
        }
    }
}