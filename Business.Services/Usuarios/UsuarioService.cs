using Business.DTOs;
using Business.Interfaces.Usuarios;
using Data.Access.EF.Context;
using Data.Access.Entities.Reservas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly LibreriaDbContext _context;

        public UsuarioService(LibreriaDbContext context)
        {
            _context = context;
        }


        public UsuarioDTO ObtenerPerfil(long usuarioId)
        {
            var u = _context.Usuarios.Find(usuarioId);
            return new UsuarioDTO
            {
                Id = u.Id,
                NombreCompleto = u.Nombre,
                Correo = u.Correo,
                Saldo = u.Saldo
            };
        }

        public void AñadirSaldo(long usuarioId, decimal cantidad)
        {
            var u = _context.Usuarios.Find(usuarioId);
            u.Saldo += cantidad;
            _context.SaveChanges();
        }

        public List<ReservaDTO> ObtenerReservasActivas(long usuarioId)
        {
            return _context.Reservas
                .Where(r => r.UsuarioId == usuarioId && r.EstadoReserva == EstadoReservaId.Activo)
                .Select(r => new ReservaDTO
                {
                    Id = r.Id,
                    TituloLibro = r.Libro.Titulo,
                    FechaReserva = r.FechaReserva,
                    FechaLimite = r.FechaLimite,
                    EstadoReserva = r.EstadoReserva,
                    TieneComprobante = r.TieneComprobante 
                }).ToList();
        }

        public HistorialDTO ObtenerHistorialCompleto(long usuarioId)
        {
            var compras = _context.Compras
                .Where(c => c.UsuarioId == usuarioId)
                .Include(c => c.DetallesCompra)
                    .ThenInclude(dc => dc.Libro)
                .Select(c => new CompraDTO
                {
                    Id = c.Id,
                    FechaCompra = c.Fecha,
                    ImportePagado = c.Precio,
                    TituloLibro = string.Join(", ", c.DetallesCompra.Select(dc => dc.Libro.Titulo))
                })
                .ToList();

            var reservas = ObtenerReservasPorUsuario(usuarioId);

            return new HistorialDTO
            {
                Compras = compras,
                Reservas = reservas
            };
        }

        private List<ReservaDTO> ObtenerReservasPorUsuario(long usuarioId)
        {
            return _context.Reservas
                .Where(r => r.UsuarioId == usuarioId)
                .Select(r => new ReservaDTO
                {
                    Id = r.Id,
                    TituloLibro = r.Libro.Titulo,
                    FechaReserva = r.FechaReserva,
                    FechaLimite = r.FechaLimite,
                    EstadoReserva = r.EstadoReserva,
                    TieneComprobante = r.TieneComprobante 
                }).ToList();
        }


        public async Task<UsuarioDTO> ObtenerPerfilAsync(int id)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null) return null;

            return new UsuarioDTO
            {
                Id = u.Id,
                NombreCompleto = u.Nombre,
                Correo = u.Correo,
                Saldo = u.Saldo
            };
        }

        public async Task AñadirSaldoAsync(int id, decimal cantidad)
        {
            var u = await _context.Usuarios.FindAsync(id);
            if (u == null) throw new Exception("Usuario no encontrado");

            u.Saldo += cantidad;
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReservaDTO>> ObtenerReservasActivasAsync(int id)
        {
            return await _context.Reservas
                .Where(r => r.UsuarioId == id && r.EstadoReserva == EstadoReservaId.Activo)
                .Select(r => new ReservaDTO
                {
                    Id = r.Id,
                    TituloLibro = r.Libro.Titulo,
                    FechaReserva = r.FechaReserva,
                    FechaLimite = r.FechaLimite,
                    EstadoReserva = r.EstadoReserva,
                    TieneComprobante = r.TieneComprobante 
                }).ToListAsync();
        }

        public async Task<HistorialDTO> ObtenerHistorialCompletoAsync(int id)
        {
            var compras = await _context.Compras
                .Where(c => c.UsuarioId == id)
                .Include(c => c.DetallesCompra)
                    .ThenInclude(dc => dc.Libro)
                .Select(c => new CompraDTO
                {
                    Id = c.Id,
                    FechaCompra = c.Fecha,
                    ImportePagado = c.Precio,
                    TituloLibro = string.Join(", ", c.DetallesCompra.Select(dc => dc.Libro.Titulo))
                }).ToListAsync();

            var reservas = await _context.Reservas
                .Where(r => r.UsuarioId == id)
                .Select(r => new ReservaDTO
                {
                    Id = r.Id,
                    TituloLibro = r.Libro.Titulo,
                    FechaReserva = r.FechaReserva,
                    FechaLimite = r.FechaLimite,
                    EstadoReserva = r.EstadoReserva,
                    TieneComprobante = _context.ComprobantesDevolucion.Any(c => c.ReservaId == r.Id)
                }).ToListAsync();


            return new HistorialDTO
            {
                Compras = compras,
                Reservas = reservas
            };
        }


        public void ActualizarAvatar(long usuarioId, string nuevaUrl)
        {
            var usuario = _context.Usuarios.Find(usuarioId);
            if (usuario == null) throw new Exception("Usuario no encontrado");

            usuario.AvatarUrl = nuevaUrl;
            _context.SaveChanges();
        }




    }
}
