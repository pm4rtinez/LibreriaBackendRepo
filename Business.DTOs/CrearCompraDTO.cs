using Data.Access.Entities.DetallesCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    namespace Business.DTOs
    {
        public class CrearCompraDTO
        {
            public DateTime Fecha { get; set; }
            public decimal Precio { get; set; }
            public long UsuarioId { get; set; }
            public List<DetalleCompra> DetallesCompra { get; set; } 
        }
    }

}

