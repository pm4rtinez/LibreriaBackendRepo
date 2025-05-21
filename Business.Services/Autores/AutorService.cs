    using Business.Interfaces.Autores;
using Data.Access.EF.Context;
using Data.Access.Entities.Autores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Autores
{
    public class AutorService : IAutorService
    {
        private readonly LibreriaDbContext _context;

        public AutorService(LibreriaDbContext context)
        {
            _context = context;
        }   

        public List<string> ObtenerAutores()
        {
            return _context.Autores.Select(a => a.Nombre).ToList();
        }

        public void CrearAutor(string nombre)
        {
            var autor = new Autor { Nombre = nombre };
            _context.Autores.Add(autor);
            _context.SaveChanges();
        }

        public Task<List<string>> ObtenerAutoresAsync()
        {
            throw new NotImplementedException();
        }

        public Task CrearAutorAsync(string nombre)
        {
            throw new NotImplementedException();
        }
    }

}
