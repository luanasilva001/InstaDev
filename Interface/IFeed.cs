using System.Collections.Generic;
using G8InstaDev.Models;

namespace G8InstaDev
{
    public interface IPublicacao
    {
         void Create (Publicacao p);
         List<Publicacao> ReadAll();
         void Update(Publicacao p);
         void Delete(int id);
    }
}