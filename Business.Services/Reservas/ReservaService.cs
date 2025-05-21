using Business.DTOs;
using Business.Interfaces.Reservas;
using Data.Access.EF.Context;
using Data.Access.Entities.Reservas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services.Reservas
{
    public class ReservaService : IReservaService
    {
        private readonly LibreriaDbContext _context;

        public ReservaService(LibreriaDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservaDTO>> ObtenerReservasPorUsuarioAsync(int usuarioId)
        {
            return await _context.Reservas
                .Where(r => r.UsuarioId == usuarioId)
                .Include(r => r.Libro)
                .Select(r => new ReservaDTO
                {
                    Id = r.Id,
                    TituloLibro = r.Libro.Titulo,
                    FechaReserva = r.FechaReserva,
                    FechaLimite = r.FechaLimite,
                    Estado = r.Estado
                })
                .ToListAsync();
        }

        public async Task<List<ReservaDTO>> ObtenerReservasPorFechaAsync(int usuarioId, DateTime desde, DateTime hasta)
        {
            return await _context.Reservas
                .Where(r => r.UsuarioId == usuarioId && r.FechaReserva >= desde && r.FechaReserva <= hasta)
                .Include(r => r.Libro)
                .Select(r => new ReservaDTO
                {
                    Id = r.Id,
                    TituloLibro = r.Libro.Titulo,
                    FechaReserva = r.FechaReserva,
                    FechaLimite = r.FechaLimite,
                    Estado = r.Estado
                })
                .ToListAsync();
        }

        public async Task RegistrarReservaAsync(int usuarioId, int libroId)
        {
            var libro = await _context.Libros.FindAsync(libroId);
            if (libro == null || !libro.Disponible)
                throw new InvalidOperationException("El libro no está disponible para reserva.");

            var reserva = new Reserva
            {
                UsuarioId = usuarioId,
                LibroId = libroId,
                FechaReserva = DateTime.UtcNow,
                FechaLimite = DateTime.UtcNow.AddDays(7),
                Estado = "Activa"
            };

            libro.Disponible = false;

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
        }


        public async Task DevolverLibroAsync(long reservaId)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Libro)
                .FirstOrDefaultAsync(r => r.Id == reservaId);

            if (reserva == null) throw new InvalidOperationException("Reserva no encontrada.");
            if (reserva.Estado != "Activa") throw new InvalidOperationException("La reserva ya está cerrada.");

            reserva.Estado = "Devuelto";
            reserva.Libro.Disponible = true;

            await _context.SaveChangesAsync();
        }


        public async Task<int> DiasRestantesAsync(long reservaId)
        {
            var reserva = await _context.Reservas.FindAsync(reservaId);
            if (reserva == null)
                throw new InvalidOperationException("Reserva no encontrada.");

            if (reserva.Estado != "Activa")
                return 0;

            var dias = (reserva.FechaLimite - DateTime.UtcNow).Days;
            return dias < 0 ? 0 : dias;
        }

    }
}
