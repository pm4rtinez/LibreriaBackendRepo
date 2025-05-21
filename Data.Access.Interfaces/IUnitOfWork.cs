namespace Data.Access.Interfaces
{
    using Data.Access.Interfaces.Repositories.Autores;
    using Data.Access.Interfaces.Repositories.Categorias;
    using Data.Access.Interfaces.Repositories.Compras;
    using Data.Access.Interfaces.Repositories.DetallesCompra;
    using Data.Access.Interfaces.Repositories.Libros;
    using Data.Access.Interfaces.Repositories.Usuarios;


    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        ILibroRepository Libros { get; }
        IAutorRepository Autores { get; }
        ICategoriaRepository Categorias { get; }
        ICompraRepository Compras { get; }
        IDetalleCompraRepository DetalleCompra { get; }

        Task<int> SaveChangesAsync();
    }


}
