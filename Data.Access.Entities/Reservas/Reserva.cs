using Data.Access.Entities.Comprobantes;
using Data.Access.Entities.Libros;
using Data.Access.Entities.Usuarios;
using System;
using System.Collections.Generic;

namespace Data.Access.Entities.Reservas
{
    public enum EstadoReservaId
    {
        Activo,
        EnReserva,
        Terminado
    }

    public class Reserva
    {
        public long Id { get; set; }

        public DateTime FechaReserva { get; set; }

        public DateTime FechaLimite { get; set; }

        public EstadoReservaId EstadoReserva { get; set; }

        public long UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public long LibroId { get; set; }
        public Libro Libro { get; set; }
        public bool TieneComprobante { get; set; } = false;

        public bool Ampliada { get; set; } = false;

        // Relación con los comprobantes de devolución
        public ICollection<ComprobanteDevolucion> Comprobantes { get; set; } = new List<ComprobanteDevolucion>();
    }
}
