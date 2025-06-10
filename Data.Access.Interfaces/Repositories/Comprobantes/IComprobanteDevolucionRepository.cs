using Data.Access.Entities.Comprobantes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Interfaces.Repositories.Comprobantes
{
    public interface IComprobanteDevolucionRepository : IRepository<ComprobanteDevolucion>
    {
        Task<ComprobanteDevolucion?> GetByReservaIdWithLibroAsync(long reservaId);
        Task<ComprobanteDevolucion> GetByIdAsync(long id, string includeProperties = "");

    }
}
