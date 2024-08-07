﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Imagen { get; set; }
    }
}
