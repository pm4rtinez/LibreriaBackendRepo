using Data.Access.EF.Context;
using Data.Access.Entities.Libros;
using Data.Access.Interfaces.Repositories.Libros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.EF.Repositories
{
    public class LibroRepository : Repository<Libro>, ILibroRepository
    {
        public LibroRepository(LibreriaDbContext context) : base(context)
        {
        }
    }
}
