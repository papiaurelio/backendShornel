using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class UsuarioSpecification : BaseSpecification<Usuario>
    {
        public UsuarioSpecification(UsuarioSpecificationParams usuarioParams) 
            : base(x=> 
                (string.IsNullOrEmpty(usuarioParams.Search) || x.Nombres.Contains(usuarioParams.Search)) &&
                (string.IsNullOrEmpty(usuarioParams.Nombre) || x.Nombres.Contains(usuarioParams.Nombre)) &&
                (string.IsNullOrEmpty(usuarioParams.Apellido) || x.Apellidos.Contains(usuarioParams.Nombre))
            )
        {
            ApplyPaging(usuarioParams.PageSize * (usuarioParams.PageIndex - 1), usuarioParams.PageSize);

            if (!string.IsNullOrEmpty(usuarioParams.Sort))
            {
                switch (usuarioParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p => p.Nombres);
                        break;
                    case "nombreDesc":
                        AddOrderByDesc(u => u.Nombres);
                        break;
                    case "emailAsc":
                        AddOrderBy(p => p.Email);
                        break;
                    case "emailDesc":
                        AddOrderByDesc(p => p.Email);
                        break;
                    default:
                        AddOrderBy(p => p.Nombres);
                        break;

                }
            }
        }
    }
}
