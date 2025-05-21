using Business.Interfaces.Categorias;
using Data.Access.EF.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Categorias
{
    public class CategoriaService : ICategoriaService
    {
        private readonly LibreriaDbContext _context;

        public CategoriaService(LibreriaDbContext context)
        {
            _context = context;
        }

        public List<string> ObtenerCategorias()
        {
            return _context.Categorias.Select(c => c.Nombre).ToList();
        }

        public Task<List<string>> ObtenerCategoriasAsync()
        {
            throw new NotImplementedException();
        }
    }

}
