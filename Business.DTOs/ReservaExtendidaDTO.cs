using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ReservaExtendidaDTO
    {
        public long ReservaId { get; set; }
        public string TituloLibro { get; set; }
        public DateTime FechaLimiteAnterior { get; set; }
        public DateTime NuevaFechaLimite { get; set; }
        public string Estado { get; set; }
    }
}

