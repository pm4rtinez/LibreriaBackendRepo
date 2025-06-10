using Data.Access.Entities.Reservas;
using System;

namespace Business.DTOs
{
    public class ReservaDTO
    {
        public long Id { get; set; }
        public string TituloLibro { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaLimite { get; set; }
        public EstadoReservaId EstadoReserva { get; set; }
        public bool Ampliada { get; set; }

        // Nuevo campo requerido para que Angular sepa si mostrar el botón
        public bool TieneComprobante { get; set; }
    }
}
