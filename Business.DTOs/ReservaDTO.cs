using Data.Access.Entities.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ReservaDTO
    {
        public long Id { get; set; }
        public string TituloLibro { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaLimite { get; set; }
        public EstadoReservaId EstadoReserva { get; set; }
    }

}
