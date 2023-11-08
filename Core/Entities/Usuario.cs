using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Usuario: IdentityUser
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public Direccion Direccion { get; set; }
    }
}
