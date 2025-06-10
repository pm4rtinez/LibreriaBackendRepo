using Data.Access.EF.Context;
using Data.Access.EF.Repositories;
using Data.Access.Interfaces;
using Data.Access.Interfaces.Repositories.Autores;
using Data.Access.Interfaces.Repositories.Categorias;
using Data.Access.Interfaces.Repositories.Compras;
using Data.Access.Interfaces.Repositories.DetallesCompra;
using Data.Access.Interfaces.Repositories.Libros;
using Data.Access.Interfaces.Repositories.Usuarios;
using Data.Access.Interfaces.Repositories.Reservas;
using Data.Access.Interfaces.Repositories.Comprobantes;

namespace Data.Access.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibreriaDbContext _context;

        public UnitOfWork(LibreriaDbContext context)
        {
            _context = context;

            Usuarios = new UsuarioRepository(_context);
            Libros = new LibroRepository(_context);
            Autores = new AutorRepository(_context);
            Categorias = new CategoriaRepository(_context);
            Compras = new CompraRepository(_context);
            DetallesCompra = new DetalleCompraRepository(_context);
            Reserva = new ReservaRepository(_context);
            ComprobanteDevolucion = new ComprobanteDevolucionRepository(_context); // ✅ NUEVO
        }

        public IUsuarioRepository Usuarios { get; }
        public ILibroRepository Libros { get; }
        public IAutorRepository Autores { get; }
        public ICategoriaRepository Categorias { get; }
        public ICompraRepository Compras { get; }
        public IDetalleCompraRepository DetallesCompra { get; }

        public IReservaRepository Reserva { get; } // ✅
        public IComprobanteDevolucionRepository ComprobanteDevolucion { get; } // ✅

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context.Dispose();
        public IDetalleCompraRepository DetalleCompra { get; }
    }
}
