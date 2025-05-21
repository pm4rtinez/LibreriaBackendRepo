using Business.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Libros
{
    public interface ILibroService
    {
        Task<List<LibroDTO>> BuscarLibrosAsync(string titulo, string categoria, decimal? maxPrecio, bool? disponible);
        Task<LibroDTO> ObtenerDetallesAsync(long libroId); 
        Task ComprarLibroAsync(long usuarioId, long libroId); 
        Task ReservarLibroAsync(long usuarioId, long libroId); 
    }

}
