using Data.Access.Entities.Autores;
using Data.Access.Entities.Categorias;
using Data.Access.Entities.Compras;
using Data.Access.Entities.DetallesCompra;
using Data.Access.Entities.Reservas;
using Data.Access.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Entities.Libros
{


        public class Libro
        {
            public long Id { get; set; }

            public string Titulo { get; set; }

            public string Descripcion { get; set; }

            public decimal Precio { get; set; }

            public bool Disponible { get; set; }

            public long AutorId { get; set; }
            public Autor Autor { get; set; }

            public long CategoriaId { get; set; }
            public Categoria Categoria { get; set; }

            public ICollection<DetalleCompra> DetallesCompra { get; set; }

            public ICollection<Reserva>? Reservas { get; set; }
        }
    

    //He creado ambas , los objetos y los id, podria ser redundante pero hay veces que solo me interesa
    //Acceder al id, y no generar el objeto completo.
}
