using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class HistorialDTO
    {
        public List<CompraDTO> Compras { get; set; }
        public List<ReservaDTO> Reservas { get; set; }
    }

}


//Dto para mostrar el historial del usuario, de libros comprados y reservados, apuntando a los dto correspondientes.