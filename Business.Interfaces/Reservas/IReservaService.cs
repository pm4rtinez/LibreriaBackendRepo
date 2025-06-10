using Business.DTOs;
using Data.Access.Entities.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Reservas
{
    public interface IReservaService
    {
        Task<List<ReservaDTO>> ObtenerReservasPorUsuarioAsync(long usuarioId);
        Task<List<ReservaDTO>> ObtenerReservasPorFechaAsync(long usuarioId, DateTime desde, DateTime hasta);
        Task DevolverLibroAsync(long reservaId);
        Task<int> DiasRestantesAsync(long reservaId);
        Task RegistrarReservaAsync(long usuarioId, long libroId);
        Task<ReservaExtendidaDTO?> AmpliarReservaAsync(long reservaId, int dias);
    }


}
