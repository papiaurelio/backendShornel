using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class OrdenCompras : ClaseBase
    {
        public OrdenCompras()
        {
            
        }
        public OrdenCompras(string idComprador, string correoComprador, 
            IReadOnlyList<OrdenItem> orderItems, decimal subTotal)
        {
            IdComprador = idComprador;
            CorreoComprador = correoComprador;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string IdComprador { get; set; }
        public string CorreoComprador { get; set; }
        public DateTimeOffset FechaOrdenCompra { get; set; } = DateTimeOffset.Now;

        public Direccion DireccionEnvio { get; set; } = null;

        public bool Envio { get; set; } = false;

        public IReadOnlyList<OrdenItem> OrderItems { get; set; }

        public decimal SubTotal { get; set; }

        public OrdenStatus Status { get; set; } = OrdenStatus.Pendiente;

        public string PagoId { get; set; }

        public decimal PrecioEnvio { get; set; } = 0;

        public decimal TotalFactura()
        {
            return (decimal)(SubTotal + PrecioEnvio) * (1.15m);
        }


    }
}
