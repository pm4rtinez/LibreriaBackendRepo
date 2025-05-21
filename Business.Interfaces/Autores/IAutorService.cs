using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Autores
{
    public interface IAutorService
    {
        Task<List<string>> ObtenerAutoresAsync();
        Task CrearAutorAsync(string nombre);
    }

}
