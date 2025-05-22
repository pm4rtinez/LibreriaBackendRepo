using Business.DTOs;
using Business.Interfaces.Libros;
using Data.Access.EF.Context;
using Data.Access.Entities.Compras;
using Data.Access.Entities.DetallesCompra;
using Data.Access.Entities.Reservas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services.Libros
{
    public class LibroService : ILibroService
    {
        private readonly LibreriaDbContext _context;

        public LibroService(LibreriaDbContext context)
        {
            _context = context;
        }

        public List<LibroDTO> BuscarLibros(string titulo = null, string categoria = null, decimal? maxPrecio = null, bool? disponible = null)
        {
            var query = _context.Libros.AsQueryable();

            if (!string.IsNullOrEmpty(titulo)) query = query.Where(l => l.Titulo.Contains(titulo));
            if (!string.IsNullOrEmpty(categoria)) query = query.Where(l => l.Categoria.Nombre == categoria);
            if (maxPrecio.HasValue) query = query.Where(l => l.Precio <= maxPrecio.Value);
            if (disponible.HasValue) query = query.Where(l => l.Disponible == disponible.Value);

            return query.Select(l => new LibroDTO
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor.Nombre,
                Categoria = l.Categoria.Nombre,
                Precio = l.Precio,
                Disponible = l.Disponible
            }).ToList();
        }

        public LibroDTO ObtenerDetalles(long libroId)
        {
            return _context.Libros
                .Where(l => l.Id == libroId)
                .Select(l => new LibroDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor.Nombre,
                    Categoria = l.Categoria.Nombre,
                    Precio = l.Precio,
                    Disponible = l.Disponible
                })
                .FirstOrDefault();
        }

        public void ComprarLibro(long usuarioId, long libroId)
        {
            var libro = _context.Libros.Find(libroId);
            var usuario = _context.Usuarios.Find(usuarioId);

            if (libro == null || usuario == null) throw new Exception("Datos inválidos.");
            if (!libro.Disponible) throw new Exception("Libro no disponible.");
            if (usuario.Saldo < libro.Precio) throw new Exception("Saldo insuficiente.");

            usuario.Saldo -= libro.Precio;

            var compra = new Compra
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Now,
                Precio = libro.Precio,
                DetallesCompra = new List<DetalleCompra>
                {
                    new DetalleCompra { LibroId = libroId }
                }
            };

            _context.Compras.Add(compra);
            _context.SaveChanges();
        }

        public void ReservarLibro(long usuarioId, long libroId)
        {
            var libro = _context.Libros.Find(libroId);
            if (libro == null || !libro.Disponible) throw new Exception("Libro no disponible.");

            var reserva = new Reserva
            {
                UsuarioId = usuarioId,
                LibroId = libroId,
                FechaReserva = DateTime.Now,
                FechaLimite = DateTime.Now.AddDays(7),
                EstadoReserva = EstadoReservaId.Activo
            };

            libro.Disponible = false;
            _context.Reservas.Add(reserva);
            _context.SaveChanges();
        }

        public async Task<List<LibroDTO>> BuscarLibrosAsync(string titulo, string categoria, decimal? maxPrecio, bool? disponible)
        {
            var query = _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .AsQueryable();

            if (!string.IsNullOrEmpty(titulo)) query = query.Where(l => l.Titulo.Contains(titulo));
            if (!string.IsNullOrEmpty(categoria)) query = query.Where(l => l.Categoria.Nombre == categoria);
            if (maxPrecio.HasValue) query = query.Where(l => l.Precio <= maxPrecio.Value);
            if (disponible.HasValue) query = query.Where(l => l.Disponible == disponible.Value);

            return await query.Select(l => new LibroDTO
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Autor = l.Autor.Nombre,
                Categoria = l.Categoria.Nombre,
                Precio = l.Precio,
                Disponible = l.Disponible
            }).ToListAsync();
        }

        public async Task<LibroDTO> ObtenerDetallesAsync(long libroId)
        {
            return await _context.Libros
                .Where(l => l.Id == libroId)
                .Select(l => new LibroDTO
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor.Nombre,
                    Categoria = l.Categoria.Nombre,
                    Precio = l.Precio,
                    Disponible = l.Disponible
                })
                .FirstOrDefaultAsync();
        }

        public async Task ComprarLibroAsync(long usuarioId, long libroId)
        {
            var libro = await _context.Libros.FindAsync(libroId); // ya espera un long
            var usuario = await _context.Usuarios.FindAsync(usuarioId);

            if (libro == null || usuario == null) throw new Exception("Datos inválidos.");
            if (!libro.Disponible) throw new Exception("Libro no disponible.");
            if (usuario.Saldo < libro.Precio) throw new Exception("Saldo insuficiente.");

            usuario.Saldo -= libro.Precio;

            var compra = new Compra
            {
                UsuarioId = usuarioId,
                Fecha = DateTime.Now,
                Precio = libro.Precio,
                DetallesCompra = new List<DetalleCompra>
        {
            new DetalleCompra { LibroId = libroId }
        }
            };

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();
        }

        public async Task ReservarLibroAsync(long usuarioId, long libroId)
        {
            var libro = await _context.Libros.FindAsync(libroId);
            if (libro == null || !libro.Disponible) throw new Exception("Libro no disponible.");

            var reserva = new Reserva
            {
                UsuarioId = usuarioId,
                LibroId = libroId,
                FechaReserva = DateTime.Now,
                FechaLimite = DateTime.Now.AddDays(7),
                EstadoReserva = EstadoReservaId.Activo
            };

            libro.Disponible = false;
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
        }


    }
}
