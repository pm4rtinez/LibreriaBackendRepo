using Data.Access.EF.Context;
using Data.Access.Entities.DetallesCompra;
using Data.Access.Interfaces.Repositories.DetallesCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.EF.Repositories
{
    public class DetalleCompraRepository : Repository<DetalleCompra>, IDetalleCompraRepository
    {
        public DetalleCompraRepository(LibreriaDbContext context) : base(context)
        {
        }
    }
}
