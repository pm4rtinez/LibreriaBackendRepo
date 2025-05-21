using Data.Access.EF.Repositories;
using Data.Access.Interfaces.Repositories.Autores;
using Data.Access.Interfaces.Repositories.Categorias;
using Data.Access.Interfaces.Repositories.Compras;
using Data.Access.Interfaces.Repositories.DetallesCompra;
using Data.Access.Interfaces.Repositories.Libros;
using Data.Access.Interfaces.Repositories.Usuarios;
using Data.Access.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Access.EF.Context;

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
        }

        public IUsuarioRepository Usuarios { get; }
        public ILibroRepository Libros { get; }
        public IAutorRepository Autores { get; }
        public ICategoriaRepository Categorias { get; }
        public ICompraRepository Compras { get; }
        public IDetalleCompraRepository DetallesCompra { get; }

        public IDetalleCompraRepository DetalleCompra => throw new NotImplementedException();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
