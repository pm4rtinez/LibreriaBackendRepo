using Data.Access.EF.Context;
using Data.Access.Entities.Reservas;
using Data.Access.Interfaces.Repositories.Reservas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.EF.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly LibreriaDbContext _context;

        public ReservaRepository(LibreriaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reserva>> ObtenerPorUsuarioAsync(long usuarioId)
        {
            return await _context.Reservas
                .Include(r => r.Libro)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Reserva>> ObtenerPorFechaAsync(long usuarioId, DateTime desde, DateTime hasta)
        {
            return await _context.Reservas
                .Include(r => r.Libro)
                .Where(r => r.UsuarioId == usuarioId && r.FechaReserva >= desde && r.FechaReserva <= hasta)
                .ToListAsync();
        }

        public async Task<Reserva> ObtenerPorIdAsync(long id)
        {
            return await _context.Reservas
                .Include(r => r.Libro)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task RegistrarAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync(reserva);
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Reserva> GetByIdWithLibroAsync(long id)
        {
            return await _context.Reservas
                .Include(r => r.Libro)
                .FirstOrDefaultAsync(r => r.Id == id);
        }


        public void Update(Reserva reserva)
        {
            _context.Reservas.Update(reserva);
        }

    }
}
