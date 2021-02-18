using System;
using System.Collections.Generic;
using System.IO;

namespace G8InstaDev.Models
{
    public class InstaDevBase
    {

        public void CreateFolderAndFile(string path)
        {
            string folder = path.Split("/")[0];

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        
        public List<string> ReadAllLinesCSV(string path)
        {
            List<string> linhas = new List<string>();

            using (StreamReader file = new StreamReader(path))
            {
                string linha;
                while ((linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }

            return linhas;
        }
        public void RewriteCSV(string path, List<string> linhas)
        {
            using (StreamWriter ouput = new StreamWriter(path))
            {
                foreach (var item in linhas)
                {
                    ouput.Write(item + '\n');
                }
            }
        }
    }
}