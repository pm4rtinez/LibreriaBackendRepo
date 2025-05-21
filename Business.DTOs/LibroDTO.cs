using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs
{
    public class LibroDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }

        public string Autor { get; set; }
        public string Categoria { get; set; }

        public long AutorId { get; set; }      
        public long CategoriaId { get; set; }
    }


}
