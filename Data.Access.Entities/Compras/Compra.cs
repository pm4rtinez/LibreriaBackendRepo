using Data.Access.Entities.DetallesCompra;
using Data.Access.Entities.Libros;
using Data.Access.Entities.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Entities.Compras
{

        public class Compra
        {
            public long Id { get; set; }

            public DateTime Fecha { get; set; }

            public decimal Precio { get; set; }

            public long UsuarioId { get; set; }
            public Usuario Usuario { get; set; }

            public ICollection<DetalleCompra> DetallesCompra { get; set; }
        }
    



}
