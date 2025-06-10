using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Usuarios
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> ObtenerPerfilAsync(int id);
        Task AñadirSaldoAsync(int id, decimal cantidad);
        Task<List<ReservaDTO>> ObtenerReservasActivasAsync(int id);
        Task<HistorialDTO> ObtenerHistorialCompletoAsync(int id);

        UsuarioDTO ObtenerPerfil(long usuarioId);
        void AñadirSaldo(long usuarioId, decimal cantidad);

        List<ReservaDTO> ObtenerReservasActivas(long usuarioId);

        HistorialDTO ObtenerHistorialCompleto(long usuarioId);
        void ActualizarAvatar(long usuarioId, string nuevaUrl);

    }

}
