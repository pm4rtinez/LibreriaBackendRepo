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

        public async Task<List<ReservaDTO>> ObtenerReservasPorUsuarioAsync(long usuarioId)
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
                    EstadoReserva = r.EstadoReserva,
                    Ampliada = r.Ampliada,
                    TieneComprobante = r.TieneComprobante 
                })
                .ToListAsync();
        }


        public async Task<List<ReservaDTO>> ObtenerReservasPorFechaAsync(long usuarioId, DateTime desde, DateTime hasta)
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
                    EstadoReserva = r.EstadoReserva,
                    TieneComprobante = r.TieneComprobante 
                })
                .ToListAsync();
        }

        public async Task RegistrarReservaAsync(long usuarioId, long libroId)
        {
            var libro = await _context.Libros.FindAsync(libroId);

            if (libro == null)
                throw new InvalidOperationException($" El libro con ID {libroId} no existe en la base de datos.");

            if (!libro.Disponible)
                throw new InvalidOperationException($" El libro '{libro.Titulo}' no está disponible actualmente para reserva.");

            var reservaExistente = await _context.Reservas
                .AnyAsync(r => r.LibroId == libroId && r.EstadoReserva == EstadoReservaId.Activo);

            if (reservaExistente)
                throw new InvalidOperationException($" El libro '{libro.Titulo}' ya está reservado por otro usuario.");

            var reserva = new Reserva
            {
                UsuarioId = usuarioId,
                LibroId = libroId,
                FechaReserva = DateTime.UtcNow,
                FechaLimite = DateTime.UtcNow.AddDays(7),
                EstadoReserva = EstadoReservaId.Activo
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
            if (reserva.EstadoReserva != EstadoReservaId.Activo)
                throw new InvalidOperationException("La reserva ya está devuelta.");

            reserva.EstadoReserva = EstadoReservaId.Terminado;
            reserva.Libro.Disponible = true;

            await _context.SaveChangesAsync();
        }

        public async Task<int> DiasRestantesAsync(long reservaId)
        {
            var reserva = await _context.Reservas.FindAsync(reservaId);
            if (reserva == null)
                throw new InvalidOperationException("Reserva no encontrada.");

            if (reserva.EstadoReserva != EstadoReservaId.Activo)
                return 0;

            var dias = (reserva.FechaLimite - DateTime.UtcNow).Days;
            return dias < 0 ? 0 : dias;
        }

        public async Task<ReservaExtendidaDTO?> AmpliarReservaAsync(long reservaId, int dias)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Libro)
                .FirstOrDefaultAsync(r => r.Id == reservaId);

            if (reserva == null)
            {
                Console.WriteLine("❌ Reserva no encontrada.");
                return null;
            }

            if (reserva.EstadoReserva != EstadoReservaId.Activo)
            {
                Console.WriteLine("❌ La reserva no está activa.");
                return null;
            }

            if (reserva.Ampliada)
            {
                Console.WriteLine("❌ La reserva ya fue ampliada anteriormente.");
                return null;
            }

            var fechaAnterior = reserva.FechaLimite;
            reserva.FechaLimite = reserva.FechaLimite.AddDays(dias);
            reserva.Ampliada = true;

            await _context.SaveChangesAsync();

            return new ReservaExtendidaDTO
            {
                ReservaId = reserva.Id,
                TituloLibro = reserva.Libro.Titulo,
                FechaLimiteAnterior = fechaAnterior,
                NuevaFechaLimite = reserva.FechaLimite,
                Estado = reserva.EstadoReserva.ToString()
            };
        }







    }
}
