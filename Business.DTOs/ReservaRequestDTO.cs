using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ReservaRequestDTO
    {
        public long UsuarioId { get; set; }
        public long LibroId { get; set; }
    }

}
