using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductoForCoutingSpecification : BaseSpecification<Producto>
    {
        public ProductoForCoutingSpecification (ProductoSpecificationParams productoParams)
          : base(x =>
            (string.IsNullOrEmpty(productoParams.Search) || x.Nombre.Contains(productoParams.Search)) &&
            (!productoParams.Marca.HasValue || x.MarcaId == productoParams.Marca) &&
            (!productoParams.Categoria.HasValue || x.CategoriaId == productoParams.Categoria)
          )
        {

        }
    }
}
