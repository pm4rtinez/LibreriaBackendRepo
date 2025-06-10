using Data.Access.Interfaces.Repositories.Autores;
using Data.Access.Interfaces.Repositories.Categorias;
using Data.Access.Interfaces.Repositories.Compras;
using Data.Access.Interfaces.Repositories.DetallesCompra;
using Data.Access.Interfaces.Repositories.Libros;
using Data.Access.Interfaces.Repositories.Usuarios;
using Data.Access.Interfaces.Repositories.Reservas;
using Data.Access.Interfaces.Repositories.Comprobantes;
using Data.Access.Entities.Comprobantes;

namespace Data.Access.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        ILibroRepository Libros { get; }
        IAutorRepository Autores { get; }
        ICategoriaRepository Categorias { get; }
        ICompraRepository Compras { get; }
        IDetalleCompraRepository DetallesCompra { get; }

        IReservaRepository Reserva { get; } // ✅ NUEVO
        IComprobanteDevolucionRepository ComprobanteDevolucion { get; } // ✅ NUEVO
        public IDetalleCompraRepository DetalleCompra { get; }

        Task<int> SaveChangesAsync();
    }
}
