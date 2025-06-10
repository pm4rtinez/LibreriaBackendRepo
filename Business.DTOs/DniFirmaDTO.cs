using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class DniFirmaDTO
    {
        public long ComprobanteId { get; set; }
        public string DNI { get; set; } = string.Empty;
        public string FirmaBase64 { get; set; } = string.Empty; 
    }

}
