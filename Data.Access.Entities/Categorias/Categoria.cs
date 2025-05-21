using Data.Access.Entities.Libros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Entities.Categorias
{

        public class Categoria
        {
            public long Id { get; set; }

            public string Nombre { get; set; }

            public ICollection<Libro>? Libros { get; set; }
        }


}
