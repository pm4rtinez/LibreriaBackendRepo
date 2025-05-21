using Data.Access.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Interfaces.Repositories.Usuarios
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetByCorreoAsync(string correo);
    }
}
