using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class UsuarioForCountingSpecification : BaseSpecification<Usuario>
    {
        public UsuarioForCountingSpecification(UsuarioSpecificationParams usuarioParams)
            : base(x =>
                (string.IsNullOrEmpty(usuarioParams.Search) || x.Nombres.Contains(usuarioParams.Search)) &&
                (string.IsNullOrEmpty(usuarioParams.Nombre) || x.Nombres.Contains(usuarioParams.Nombre)) &&
                (string.IsNullOrEmpty(usuarioParams.Apellido) || x.Apellidos.Contains(usuarioParams.Nombre))
            )
        {

        }

    }
}
