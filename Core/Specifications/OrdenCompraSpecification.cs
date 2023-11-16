using Core.Entities.OrdenCompra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class OrdenCompraSpecification : BaseSpecification<OrdenCompras>
    {
        public OrdenCompraSpecification(string email) : base(o => o.CorreoComprador == email)
        {
            AddInclude(o => o.OrderItems);

            AddOrderByDesc(o => o.FechaOrdenCompra);
        }

        public OrdenCompraSpecification(int id, string email) 
            : base(o => o.CorreoComprador == email &&
              o.Id == id
            )
        {
            AddInclude(o => o.OrderItems);

            AddOrderByDesc(o => o.FechaOrdenCompra);
        }
    }
}
