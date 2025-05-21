using Data.Access.EF.Context;
using Data.Access.Entities.Categorias;
using Data.Access.Interfaces.Repositories.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.EF.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(LibreriaDbContext context) : base(context)
        {
        }
    }
}
