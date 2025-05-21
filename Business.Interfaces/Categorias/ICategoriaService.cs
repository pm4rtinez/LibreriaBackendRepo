using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Categorias
{
    public interface ICategoriaService
    {
        Task<List<string>> ObtenerCategoriasAsync();
    }

}
