using Data.Access.Entities.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Entities.Comprobantes
{
    public class ComprobanteDevolucion
    {
        public long Id { get; set; }
        public long ReservaId { get; set; }
        public string DNI { get; set; } = string.Empty;
        public byte[] FirmaImagen { get; set; } = Array.Empty<byte>();
        public DateTime FechaGeneracion { get; set; } = DateTime.UtcNow;

        // Relación con la reserva
        public Reserva Reserva { get; set; } = null!;
    }

}
