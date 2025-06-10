using Data.Access.Entities.Autores;
using Data.Access.Entities.Categorias;
using Data.Access.Entities.Compras;
using Data.Access.Entities.DetallesCompra;
using Data.Access.Entities.Libros;
using Data.Access.Entities.Usuarios;
using Data.Access.Entities.Reservas;
using Data.Access.Entities.Comprobantes;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Access.EF.Context
{
    public class LibreriaDbContext : DbContext
    {
        public LibreriaDbContext(DbContextOptions<LibreriaDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ComprobanteDevolucion> ComprobantesDevolucion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Categoria)
                .WithMany(c => c.Libros)
                .HasForeignKey(l => l.CategoriaId);

            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Autor)
                .WithMany(a => a.Libros)
                .HasForeignKey(l => l.AutorId);

            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Compras)
                .HasForeignKey(c => c.UsuarioId);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(dc => dc.Compra)
                .WithMany(c => c.DetallesCompra)
                .HasForeignKey(dc => dc.CompraId);

            modelBuilder.Entity<DetalleCompra>()
                .HasOne(dc => dc.Libro)
                .WithMany(l => l.DetallesCompra)
                .HasForeignKey(dc => dc.LibroId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.UsuarioId);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Libro)
                .WithMany(l => l.Reservas)
                .HasForeignKey(r => r.LibroId);

            modelBuilder.Entity<ComprobanteDevolucion>()
                .HasOne(cd => cd.Reserva)
                .WithMany(r => r.Comprobantes)
                .HasForeignKey(cd => cd.ReservaId);
        }
    }
}
