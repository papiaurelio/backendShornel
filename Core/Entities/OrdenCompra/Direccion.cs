using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrdenCompra
{
    public class Direccion
    {
        public Direccion()
        {
            
        }

        public Direccion(int id, string direccionCalle, string ciudad, string departamento)
        {
            DireccionCalle = direccionCalle;
            Ciudad = ciudad;
            Departamento = departamento;
        }

        public string DireccionCalle { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }

    }
}
