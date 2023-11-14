using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class OrdenItem : ClaseBase
    {
        public OrdenItem()
        {
            
        }

        public OrdenItem(ProductoOrdenado productoItemOrdenado, decimal precio, int cantidad)
        {
            ProductoOrdenado = productoItemOrdenado;
            Precio = precio;
            this.cantidad = cantidad;
        }

        public ProductoOrdenado ProductoOrdenado { get; set; }
        public decimal Precio { get; set; }
        public int cantidad { get; set; }
    }
}
