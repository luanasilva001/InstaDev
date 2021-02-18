using G8InstaDev.Models;
using System.Collections.Generic;

namespace G8InstaDev.Interface
{
    public interface IUsuario
    {
        void Create (Usuario u);
        List<Usuario> ReadAll();
        void Update(Usuario u);
        void Delete(int id);
    }
}