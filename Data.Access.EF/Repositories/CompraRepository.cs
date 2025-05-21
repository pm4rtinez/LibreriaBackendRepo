using Data.Access.EF.Context;
using Data.Access.Entities.Compras;
using Data.Access.Interfaces.Repositories.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.EF.Repositories
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        public CompraRepository(LibreriaDbContext context) : base(context)
        {
        }
    }
}
