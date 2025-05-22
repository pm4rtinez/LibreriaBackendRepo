using Data.Access.Entities.Libros;
using Data.Access.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
