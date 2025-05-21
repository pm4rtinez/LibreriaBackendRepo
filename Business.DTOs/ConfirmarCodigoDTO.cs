using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class ConfirmarCodigoDTO
    {
        public string Correo { get; set; }
        public string Codigo { get; set; }
    }

}

//Dto para el envio de una solicitud de codigo de recuperacion