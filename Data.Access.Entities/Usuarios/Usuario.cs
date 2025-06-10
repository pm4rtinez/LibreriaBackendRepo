using Data.Access.Entities.Compras;
using Data.Access.Entities.Reservas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Entities.Usuarios
{

    public class Usuario
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Password { get; set; }

        public decimal Saldo { get; set; }

        public string? AvatarUrl { get; set; }

        public string? Direccion { get; set; }
        public ICollection<Compra>? Compras { get; set; }

        public ICollection<Reserva>? Reservas { get; set; }
    }



}
