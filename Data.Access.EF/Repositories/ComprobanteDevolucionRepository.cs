// EF/Repositories/ComprobanteDevolucionRepository.cs
using Data.Access.EF.Context;
using Data.Access.EF.Context;
using Data.Access.Entities.Comprobantes;
using Data.Access.Interfaces.Repositories.Comprobantes;
using Microsoft.EntityFrameworkCore;

namespace Data.Access.EF.Repositories
{
    public class ComprobanteDevolucionRepository : Repository<ComprobanteDevolucion>, IComprobanteDevolucionRepository
    {
        private readonly LibreriaDbContext _context;

        public ComprobanteDevolucionRepository(LibreriaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ComprobanteDevolucion?> GetByReservaIdWithLibroAsync(long reservaId)
        {
            return await _context.ComprobantesDevolucion
                .Include(c => c.Reserva)
                .ThenInclude(r => r.Libro)
                .FirstOrDefaultAsync(c => c.ReservaId == reservaId);
        }

        public async Task<ComprobanteDevolucion?> GetByReservaIdAsync(long reservaId)
        {
            return await _context.ComprobantesDevolucion
                .FirstOrDefaultAsync(c => c.ReservaId == reservaId);
        }
        public async Task<ComprobanteDevolucion> GetByIdAsync(long id, string includeProperties = "")
        {
            IQueryable<ComprobanteDevolucion> query = _context.ComprobantesDevolucion;

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }


    }
}
