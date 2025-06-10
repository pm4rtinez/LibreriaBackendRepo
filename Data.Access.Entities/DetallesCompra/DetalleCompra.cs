using Data.Access.Entities.Compras;
using Data.Access.Entities.Libros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Access.Entities.DetallesCompra
{

    public class DetalleCompra
    {
        public long Id { get; set; }

        public long CompraId { get; set; }
        public Compra Compra { get; set; }

        public long LibroId { get; set; }
        public Libro Libro { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUd { get; set; }
    }



}
