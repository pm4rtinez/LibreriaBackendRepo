using Data.Access.Entities.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Interfaces.Repositories.Reservas
{
    public interface IReservaRepository
    {
        Task<List<Reserva>> ObtenerPorUsuarioAsync(long usuarioId);
        Task<List<Reserva>> ObtenerPorFechaAsync(long usuarioId, DateTime desde, DateTime hasta);
        Task<Reserva> ObtenerPorIdAsync(long id);
        Task RegistrarAsync(Reserva reserva);
        Task GuardarCambiosAsync();
        Task<Reserva?> GetByIdWithLibroAsync(long id);
        void Update(Reserva reserva);

    }
}
