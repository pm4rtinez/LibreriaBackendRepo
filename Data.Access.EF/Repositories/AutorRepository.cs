using Data.Access.EF.Context;
using Data.Access.Entities.Autores;
using Data.Access.Interfaces.Repositories.Autores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.EF.Repositories
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(LibreriaDbContext context) : base(context)
        {
        }
    }
}
