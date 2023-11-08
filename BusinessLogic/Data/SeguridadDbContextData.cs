using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class SeguridadDbContextData
    {
        public static async Task SeedUsersAsync(UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuarios = new List<Usuario> { 
                    new Usuario{                    
                    Nombres = "Byron",
                    Apellidos = "Samayoa",
                    UserName = "PapiAurelio",
                    Email = "byrondereyes@gmail.com",
                    Direccion = new Direccion
                        {
                            direccionCalle = "Calle el calvario",
                            Ciudad = "Masaya",
                            Departamento = "Masaya"
                        } 
                    },
                    new Usuario
                    {
                    Nombres = "Rey",
                    Apellidos = "Eduardo",
                    UserName = "reduhq",
                    Email = "reduhq@gmail.com",
                    Direccion = new Direccion
                        {
                            direccionCalle = "Por la UNI",
                            Ciudad = "Managua",
                            Departamento = "Managua"
                        }

                    }
                };


                await userManager.CreateAsync(usuarios[0], "Aurelio444*");
                await userManager.CreateAsync(usuarios[1], "Reduhq4444*");
            }
        }
    }
}
