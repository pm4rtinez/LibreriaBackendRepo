using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Reservas
{
    public interface IReservaService
    {
        Task<List<ReservaDTO>> ObtenerReservasPorUsuarioAsync(int usuarioId);
        Task<List<ReservaDTO>> ObtenerReservasPorFechaAsync(int usuarioId, DateTime desde, DateTime hasta);
        Task DevolverLibroAsync(long reservaId); 
        Task<int> DiasRestantesAsync(long reservaId);
        Task RegistrarReservaAsync(int usuarioId, int libroId);
    }


}
