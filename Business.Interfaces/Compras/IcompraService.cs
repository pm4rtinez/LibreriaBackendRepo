using Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Compras
{
    public interface ICompraService
    {
        Task<List<CompraDTO>> ObtenerComprasPorUsuario(long usuarioId);
        Task<List<CompraDTO>> ObtenerComprasPorFecha(long usuarioId, DateTime desde, DateTime hasta);
        Task RegistrarCompra(long usuarioId, long libroId);
    }

}
