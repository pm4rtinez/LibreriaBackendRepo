using Data.Access.EF.Context;
using Data.Access.Entities.Usuarios;
using Data.Access.Interfaces.Repositories.Usuarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.EF.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        private readonly LibreriaDbContext _context;

        public UsuarioRepository(LibreriaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByCorreoAsync(string correo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }
}
