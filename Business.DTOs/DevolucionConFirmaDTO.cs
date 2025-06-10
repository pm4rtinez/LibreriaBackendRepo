using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class DevolucionConFirmaDTO
    {
        public string DNI { get; set; } = string.Empty;
        public string FirmaBase64 { get; set; } = string.Empty;
    }
}

