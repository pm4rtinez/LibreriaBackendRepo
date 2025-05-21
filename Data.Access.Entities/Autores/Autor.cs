using Data.Access.Entities.Libros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Access.Entities.Autores
{

        public class Autor
        {
            public long Id { get; set; }

            public string Nombre { get; set; }

            public ICollection<Libro> Libros { get; set; }
        }



}
