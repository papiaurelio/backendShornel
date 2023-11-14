using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class ProductoOrdenado
    {
        public ProductoOrdenado()
        {
            
        }
        public ProductoOrdenado(int productoOrdenadoId, string productoNombre, string imagenUrl)
        {
            ProductoOrdenadoId = productoOrdenadoId;
            ProductoNombre = productoNombre;
            ImagenUrl = imagenUrl;
        }

        public int ProductoOrdenadoId { get; set; }
        public string ProductoNombre { get; set; }
        public string ImagenUrl { get; set; }
    }
}
