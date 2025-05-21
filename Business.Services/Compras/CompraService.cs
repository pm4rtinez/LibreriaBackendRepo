using Business.DTOs;
using Business.Interfaces.Compras;
using Data.Access.EF.Context;
using Data.Access.Entities.Compras;
using Data.Access.Entities.DetallesCompra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services.Compras
{
    public class CompraService : ICompraService
    {
        private readonly LibreriaDbContext _context;

        public CompraService(LibreriaDbContext context)
        {
            _context = context;
        }

        public async Task<List<CompraDTO>> ObtenerComprasPorUsuario(long usuarioId)
        {
            return await _context.Compras
                .Where(c => c.UsuarioId == usuarioId)
                .Include(c => c.DetallesCompra).ThenInclude(dc => dc.Libro)
                .Select(c => new CompraDTO
                {
                    Id = c.Id,
                    FechaCompra = c.Fecha,
                    ImportePagado = c.Precio,
                    TituloLibro = string.Join(", ", c.DetallesCompra.Select(dc => dc.Libro.Titulo))
                })
                .ToListAsync();
        }

        public async Task<List<CompraDTO>> ObtenerComprasPorFecha(long usuarioId, DateTime desde, DateTime hasta)
        {
            return await _context.Compras
                .Where(c => c.UsuarioId == usuarioId && c.Fecha >= desde && c.Fecha <= hasta)
                .Include(c => c.DetallesCompra).ThenInclude(dc => dc.Libro)
                .Select(c => new CompraDTO
                {
                    Id = c.Id,
                    FechaCompra = c.Fecha,
                    ImportePagado = c.Precio,
                    TituloLibro = string.Join(", ", c.DetallesCompra.Select(dc => dc.Libro.Titulo))
                })
                .ToListAsync();
        }

        public async Task RegistrarCompra(long usuarioId, long libroId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null) throw new InvalidOperationException("Usuario no encontrado.");

            var libro = await _context.Libros.FindAsync(libroId);
            if (libro == null) throw new InvalidOperationException("Libro no encontrado.");
            if (!libro.Disponible) throw new InvalidOperationException("Libro no disponible.");
            if (usuario.Saldo < libro.Precio) throw new InvalidOperationException("Saldo insuficiente.");

            usuario.Saldo -= libro.Precio;

            var compra = new Compra
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.UtcNow,
                Precio = libro.Precio,
                DetallesCompra = new List<DetalleCompra>
                {
                    new DetalleCompra { LibroId = libroId }
                }
            };

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
        }
    }
}
