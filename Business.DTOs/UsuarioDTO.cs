using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public decimal Saldo { get; set; }
    }



}
