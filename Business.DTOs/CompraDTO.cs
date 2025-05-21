using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{

    public class CompraDTO
    {
        public long Id { get; set; }
        public DateTime FechaCompra { get; set; }
        public decimal ImportePagado { get; set; }
        public string TituloLibro { get; set; }
    }


}
//Dto para mostrar las compras, hay otro para crear las compras(CrearCompraDTO)