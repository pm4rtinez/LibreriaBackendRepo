﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    namespace Business.DTOs
    {
        public class CrearUsuarioDTO
        {
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string Password { get; set; }
            public decimal Saldo { get; set; }
            public string? AvatarUrl { get; set; }
            public string Direccion { get; set; }
        }
    }

}
